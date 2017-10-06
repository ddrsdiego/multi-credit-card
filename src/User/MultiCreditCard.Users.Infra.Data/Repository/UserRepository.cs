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
                        UserId = user.UserId,
                        UserName = user.UserName,
                        DocumentNumber = user.DocumentNumber,
                        Email = user.Email,
                        Password = user.Password,
                        CreationDate = user.CreationDate
                    };
                    await conn.ExecuteAsync(UserStatements.Create, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar o usuário {user.UserName}. {ex.Message}");
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
                throw new InvalidOperationException($"Erro ao obter o usuário pelo email {email}. {ex.Message}");
            }
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            var userResult = User.DefaultEntity();

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    userResult = await conn.QueryFirstOrDefaultAsync<User>(UserStatements.GetUserByUserId, new { userId = userId });
                }
                return userResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter o usuário pelo id {userId}. {ex.Message}");
            }
        }

        public async Task<User> GetUserFromCredentials(string email, string password)
        {
            var userResult = User.DefaultEntity();

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    var parameters = new { email = email, password = password };
                    userResult = await conn.QueryFirstOrDefaultAsync<User>(UserStatements.GetUserFromCredentials, parameters);
                }
                return userResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter o usuário para autenticação pelo email {email}. {ex.Message}");
            }
        }
    }
}
