using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class AuthenticationUserHandler : IAsyncRequestHandler<AuthenticationUserCommand, AuthenticationUserResponse>
    {
        private User _user = User.DefaultEntity();

        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public AuthenticationUserHandler(IUserRepository userRepository, ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger<AuthenticationUserHandler>();
        }

        public async Task<AuthenticationUserResponse> Handle(AuthenticationUserCommand message)
        {
            var response = message.Response;

            VerifyUser(message, response);
            if (response.HasError)
                return response;

            var token = await GetJwtSecurityToken(_user);

            GetResponse(response, token);
            if (response.HasError)
                return response;

            return response;
        }

        private void VerifyUser(AuthenticationUserCommand command, AuthenticationUserResponse response)
        {
            _user = _userRepository.GetUserFromCredentials(command.Email, command.Password).Result;
            if (string.IsNullOrEmpty(_user.UserId))
            {
                response.AddError($"Usuário com o email {command.Email} não localizado.");
                return;
            }
        }

        private async Task<JwtSecurityToken> GetJwtSecurityToken(User user)
        {
            return new JwtSecurityToken(
                issuer: "http://api.multicreditcard.com.br",
                audience: "http://api.multicreditcard.com.br",
                claims: GetTokenClaims(user),
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fcebf5457e484be1ab50772e236ccd22fcb32d345e41459986cdb973d2d1a34e")), SecurityAlgorithms.HmacSha256)
            );
        }

        private static IEnumerable<Claim> GetTokenClaims(User user)
        {
            return new List<Claim>
            {
                new Claim("access_token", user.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
        }

        private void GetResponse(AuthenticationUserResponse response, JwtSecurityToken token)
        {
            response.UserId = _user.UserId;
            response.Email = _user.Email;
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Expiration = token.ValidTo;
        }
    }
}