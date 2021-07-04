#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.Validations
{
    public class SystemUserDeleteValidation : EntityValidation<SystemUser>
    {
        private readonly ISystemUserRepository _repository;

        public SystemUserDeleteValidation(ISystemUserRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<SystemUser>> Execute(int id)
        {
            var recordExists = await RecordExists(id);
            if (!recordExists.Success)
            {
                return recordExists;
            }

            return recordExists;
        }
    }
}