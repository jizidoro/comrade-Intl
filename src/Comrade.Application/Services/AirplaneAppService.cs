#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Validations.AirplaneValidations;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services
{
    public class AirplaneAppService : AppService, IAirplaneAppService
    {
        private readonly AirplaneCreateUseCase _createAirplaneUseCase;
        private readonly AirplaneDeleteUseCase _deleteAirplaneUseCase;
        private readonly AirplaneEditUseCase _editAirplaneUseCase;
        private readonly IAirplaneRepository _repository;

        public AirplaneAppService(IAirplaneRepository repository,
            AirplaneEditUseCase editAirplaneUseCase,
            AirplaneCreateUseCase createAirplaneUseCase,
            AirplaneDeleteUseCase deleteAirplaneUseCase,
            IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _editAirplaneUseCase = editAirplaneUseCase;
            _createAirplaneUseCase = createAirplaneUseCase;
            _deleteAirplaneUseCase = deleteAirplaneUseCase;
        }

        public async Task<IPageResultDto<AirplaneDto>> GetAll(PaginationFilter? paginationFilter = null)
        {
            List<AirplaneDto> list;
            if (paginationFilter == null)
            {
                list = await Task.Run(() => _repository.GetAllAsNoTracking()
                    .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                    .ToList()).ConfigureAwait(false);

                return new PageResultDto<AirplaneDto>(list);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<AirplaneDto>(list);
        }

        public async Task<ISingleResultDto<AirplaneDto>> GetById(int id)
        {
            var entity = await _repository.GetById(id).ConfigureAwait(false);
            var dto = Mapper.Map<AirplaneDto>(entity);
            return new SingleResultDto<AirplaneDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
        {
            var validator = new AirplaneCreateValidation();

            var results = await validator.ValidateAsync(dto).ConfigureAwait(false);

            if (!results.IsValid)
            {
                return new SingleResultDto<EntityDto>(results);
            }

            var mappedObject = Mapper.Map<Airplane>(dto);

            var result = await _createAirplaneUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
        {
            var validator = new AirplaneEditValidation();

            var results = await validator.ValidateAsync(dto).ConfigureAwait(false);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
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