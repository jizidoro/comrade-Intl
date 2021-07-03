#region

using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.Helpers.Models.Validations;
using comrade.Core.UserSystemCore;
using comrade.Domain.Extensions;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.SecurityCore.Validation
{
    public class UserSystemPasswordValidation : EntityValidation<UserSystem>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserSystemRepository _userSystemRepository;

        public UserSystemPasswordValidation(IUserSystemRepository userSystemRepository,
            IPasswordHasher passwordHasher)
            : base(userSystemRepository)
        {
            _userSystemRepository = userSystemRepository;
            _passwordHasher = passwordHasher;
        }

        public ISingleResult<UserSystem> Execute(int key, string password)
        {
            var usuSelecionado = _userSystemRepository.GetById(key).Result;
            var keyValida = usuSelecionado != null;

            if (keyValida)
            {
                var passwordValida = _passwordHasher.Check(usuSelecionado.Password, password);

                if (!passwordValida.Verified)
                {
                    return new SingleResult<UserSystem>(1001, "Usuário ou password informados não são válidos");
                }


                return new SingleResult<UserSystem>(usuSelecionado);
            }


            return new SingleResult<UserSystem>(1001, "Usuário ou password informados não são válidos");
        }
    }
}