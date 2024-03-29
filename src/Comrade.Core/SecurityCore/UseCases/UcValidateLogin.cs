﻿#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Comrade.Core.Bases.Results;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Domain.Token;

#endregion

namespace Comrade.Core.SecurityCore.UseCases
{
    public class UcValidateLogin : IUcValidateLogin
    {
        private readonly IUcGenerateToken _generateToken;
        private readonly SystemUserPasswordValidation _systemUserPasswordValidation;


        public UcValidateLogin(
            SystemUserPasswordValidation systemUserPasswordValidation,
            IUcGenerateToken generateToken)
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
                        var selectedUser = resultPassword.Data!;

                        var profile = new List<string> {"Role"};

                        var user = new TokenUser(key, selectedUser.Name, "", profile);

                        user.Token = _generateToken.Execute(user);

                        return new SecurityResult(user);
                    }

                    return new SecurityResult(resultPassword.Code, resultPassword.Message);
                }).ConfigureAwait(false);

                return result;
            }

            return new SecurityResult(400, "Error");
        }
    }
}