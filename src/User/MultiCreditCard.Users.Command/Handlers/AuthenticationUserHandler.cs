using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MultiCreditCard.Shared.Config;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
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
        private readonly IOptions<AuthConfig> _config;

        public AuthenticationUserHandler(IUserRepository userRepository, ILoggerFactory loggerFactory, IOptions<AuthConfig> config)
        {
            _config = config;
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
            _user = _userRepository.GetUserFromCredentials(command.Email, new Password(command.Password).Encoded).Result;
            if (string.IsNullOrEmpty(_user.UserId))
            {
                response.AddError($"Usuário com o email {command.Email} não localizado.");
                return;
            }
        }

        private async Task<JwtSecurityToken> GetJwtSecurityToken(User user)
        {
            return new JwtSecurityToken(issuer: _config.Value.Issuer,
                                        audience: _config.Value.Audience,
                                        claims: GetTokenClaims(user),
                                        expires: DateTime.UtcNow.AddMinutes(15),
                                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.ContentSecret)), SecurityAlgorithms.HmacSha256)
            );
        }

        private static IEnumerable<Claim> GetTokenClaims(User user)
        {
            return new List<Claim>
            {
                new Claim("access_token", user.UserId),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
                ,new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
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