#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Filters;
using comrade.Application.Interfaces;
using comrade.Application.Utils;
using comrade.Application.Validations.AirplaneValidations;
using comrade.Core.AirplaneCore;
using comrade.Core.AirplaneCore.UseCases;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.Services
{
    public class AirplaneAppService : AppService, IAirplaneAppService
    {
        private readonly AirplaneEditUseCase _editAirplaneUseCase;
        private readonly AirplaneDeleteUseCase _deleteAirplaneUseCase;
        private readonly AirplaneCreateUseCase _createAirplaneUseCase;
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

        public async Task<IPageResultDto<AirplaneDto>> GetAll(PaginationFilter paginationFilter = null)
        {
            List<AirplaneDto> list;
            if (paginationFilter == null)
            {
                list = await Task.Run(() => _repository.GetAll()
                    .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                    .ToList());

                return new PageResultDto<AirplaneDto>(list);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            list = await Task.Run(() => _repository.GetAll().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<AirplaneDto>(list);
        }

        public async Task<ISingleResultDto<AirplaneDto>> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            var dto = Mapper.Map<AirplaneDto>(entity);
            return new SingleResultDto<AirplaneDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
        {
            var validator = new AirplaneCreateValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
            }

            var evento = Mapper.Map<Airplane>(dto);

            var result = await _createAirplaneUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
        {
            var validator = new AirplaneEditValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
            }

            var evento = Mapper.Map<Airplane>(dto);

            var result = await _editAirplaneUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Delete(int id)
        {
            var result = await _deleteAirplaneUseCase.Execute(id);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}