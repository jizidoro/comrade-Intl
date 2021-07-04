#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.Validation
{
    public class SystemUserPasswordValidation : EntityValidation<SystemUser>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISystemUserRepository _systemUserRepository;

        public SystemUserPasswordValidation(ISystemUserRepository systemUserRepository,
            IPasswordHasher passwordHasher)
            : base(systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
            _passwordHasher = passwordHasher;
        }

        public ISingleResult<SystemUser> Execute(int key, string password)
        {
            var usuSelecionado = _systemUserRepository.GetById(key).Result;
            var keyValida = usuSelecionado != null;

            if (keyValida)
            {
                var passwordValida = _passwordHasher.Check(usuSelecionado.Password, password);

                if (!passwordValida.Verified)
                {
                    return new SingleResult<SystemUser>(1001, "Usuário ou password informados não são válidos");
                }


                return new SingleResult<SystemUser>(usuSelecionado);
            }


            return new SingleResult<SystemUser>(1001, "Usuário ou password informados não são válidos");
        }
    }
}