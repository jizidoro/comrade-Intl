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
    public class ValidateLoginUseCase : IValidateLoginUseCase
    {
        private readonly SystemUserPasswordValidation _systemUserPasswordValidation;
        private readonly IGenerateTokenUseCase _generateToken;


        public ValidateLoginUseCase(
            SystemUserPasswordValidation systemUserPasswordValidation, IGenerateTokenUseCase generateToken)
        {
            _systemUserPasswordValidation = systemUserPasswordValidation;
            _generateToken = generateToken;
        }

        public async Task<SecurityResult> Execute(string key, string password)
        {
            var success = int.TryParse(key, out var number);
            if (success)
            {
                var result = await Task.Run(() =>
                {
                    var resultPassword = _systemUserPasswordValidation.Execute(number, password);


                    if (resultPassword.Success)
                    {
                        var selectedUser = resultPassword.Data;

                        var profile = "Role";

                        var user = new User
                        {
                            Key = key,
                            Name = selectedUser.Name,
                            Roles = new List<string> {string.IsNullOrEmpty(profile) ? "" : profile}
                        };

                        user.Token = _generateToken.Execute(user);

                        return new SecurityResult(user);
                    }

                    return new SecurityResult(resultPassword.Code, resultPassword.Message);
                });

                return result;
            }

            return new SecurityResult(400, "Error");
        }
    }
}