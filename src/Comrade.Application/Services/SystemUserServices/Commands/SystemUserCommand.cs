#region

using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Validations;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services.SystemUserServices.Commands
{
    public class SystemUserCommand : Service, ISystemUserCommand
    {
        private readonly ISystemUserCreateUseCase _createSystemUserUseCase;
        private readonly ISystemUserDeleteUseCase _deleteSystemUserUseCase;
        private readonly ISystemUserEditUseCase _editSystemUserUseCase;

        public SystemUserCommand(
            ISystemUserEditUseCase editSystemUserUseCase,
            ISystemUserCreateUseCase createSystemUserUseCase,
            ISystemUserDeleteUseCase deleteSystemUserUseCase,
            IMapper mapper)
            : base(mapper)
        {
            _editSystemUserUseCase = editSystemUserUseCase;
            _createSystemUserUseCase = createSystemUserUseCase;
            _deleteSystemUserUseCase = deleteSystemUserUseCase;
        }

        public async Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto)
        {
            var validator = await new SystemUserCreateValidation().ValidateAsync(dto)
                .ConfigureAwait(false);

            if (!validator.IsValid)
            {
                return new SingleResultDto<EntityDto>(validator);
            }

            var mappedObject = Mapper.Map<SystemUser>(dto);

            var result = await _createSystemUserUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto)
        {
            var validator = await new SystemUserEditValidation().ValidateAsync(dto)
                .ConfigureAwait(false);

            if (!validator.IsValid)
            {
                return new SingleResultDto<EntityDto>(validator);
            }

            var mappedObject = Mapper.Map<SystemUser>(dto);

            var result = await _editSystemUserUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Delete(int id)
        {
            var result = await _deleteSystemUserUseCase.Execute(id).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}