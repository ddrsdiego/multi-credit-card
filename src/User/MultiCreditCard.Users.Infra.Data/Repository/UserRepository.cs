using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Infra.Data.Statement;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuracoes;

        public UserRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    var parameters = new
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        DocumentNumber = user.DocumentNumber,
                        Email = user.Email.EletronicAddress,
                        Password = user.Password.Encoded,
                        CreationDate = user.CreationDate
                    };
                    await conn.ExecuteAsync(UserStatements.Create, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar o usuário {user.UserName}. Erro: {ex.Message}");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = User.DefaultEntity();

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    user = await conn.QueryFirstOrDefaultAsync<User>(UserStatements.GetUserByEmail, new { email = email });
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            var user = User.DefaultEntity();

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    user = await conn.QueryFirstOrDefaultAsync<User>(UserStatements.GetUserByUserId, new { userId = userId });
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
