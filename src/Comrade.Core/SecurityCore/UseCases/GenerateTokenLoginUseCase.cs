#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Comrade.Core.SecurityCore.UseCases
{
    public class GenerateTokenLoginUseCase : IGenerateTokenLoginUseCase
    {
        private readonly IConfiguration _configuration;
        private readonly UserSystemPasswordValidation _userSystemPasswordValidation;


        public GenerateTokenLoginUseCase(
            IConfiguration configuration,
            UserSystemPasswordValidation userSystemPasswordValidation
        )
        {
            _configuration = configuration;
            _userSystemPasswordValidation = userSystemPasswordValidation;
        }

        public async Task<SecurityResult> Execute(string key, string password)
        {
            var success = int.TryParse(key, out var number);
            if (success)
            {
                var result = await Task.Run(() =>
                {
                    var resultPassword = _userSystemPasswordValidation.Execute(number, password);


                    if (resultPassword.Success)
                    {
                        var usuSelecionado = resultPassword.Data;

                        var perfil = "Role";

                        var user = new User
                        {
                            Key = key,
                            Name = usuSelecionado.Name,
                            Papeis = new List<string> {string.IsNullOrEmpty(perfil) ? "" : perfil}
                        };

                        user.Token = GenerateToken(user);

                        return new SecurityResult(user);
                    }

                    return new SecurityResult(resultPassword.Code, resultPassword.Message);
                });

                return result;
            }

            return new SecurityResult(400, "Error");
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Key", user.Key),
                new(ClaimTypes.Name, user.Name)
            };

            foreach (var role in user.Papeis)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}