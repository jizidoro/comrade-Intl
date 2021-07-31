#region

using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Validations;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services.AirplaneServices.Commands
{
    public class AirplaneCommand : Service, IAirplaneCommand
    {
        private readonly IAirplaneCreateUseCase _createAirplaneUseCase;
        private readonly IAirplaneDeleteUseCase _deleteAirplaneUseCase;
        private readonly IAirplaneEditUseCase _editAirplaneUseCase;

        public AirplaneCommand(IAirplaneEditUseCase editAirplaneUseCase,
            IAirplaneCreateUseCase createAirplaneUseCase,
            IAirplaneDeleteUseCase deleteAirplaneUseCase,
            IMapper mapper)
            : base(mapper)
        {
            _editAirplaneUseCase = editAirplaneUseCase;
            _createAirplaneUseCase = createAirplaneUseCase;
            _deleteAirplaneUseCase = deleteAirplaneUseCase;
        }

        public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
        {
            var validator = await new AirplaneCreateValidation().ValidateAsync(dto)
                .ConfigureAwait(false);

            if (!validator.IsValid)
            {
                return new SingleResultDto<EntityDto>(validator);
            }

            var mappedObject = Mapper.Map<Airplane>(dto);

            var result = await _createAirplaneUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
        {
            var validator =
                await new AirplaneEditValidation().ValidateAsync(dto).ConfigureAwait(false);

            if (!validator.IsValid)
            {
                return new SingleResultDto<EntityDto>(validator);
            }

            var mappedObject = Mapper.Map<Airplane>(dto);

            var result = await _editAirplaneUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Delete(int id)
        {
            var result = await _deleteAirplaneUseCase.Execute(id).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}