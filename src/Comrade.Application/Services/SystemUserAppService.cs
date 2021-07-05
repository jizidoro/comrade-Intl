#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.SystemUserDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Validations.SystemUserValidations;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services
{
    public class SystemUserAppService : AppService, ISystemUserAppService
    {
        private readonly SystemUserCreateUseCase _createSystemUserUseCase;
        private readonly SystemUserDeleteUseCase _deleteSystemUserUseCase;
        private readonly SystemUserEditUseCase _editSystemUserUseCase;
        private readonly ISystemUserRepository _repository;

        public SystemUserAppService(ISystemUserRepository repository,
            SystemUserEditUseCase editSystemUserUseCase,
            SystemUserCreateUseCase createSystemUserUseCase,
            SystemUserDeleteUseCase deleteSystemUserUseCase,
            IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _editSystemUserUseCase = editSystemUserUseCase;
            _createSystemUserUseCase = createSystemUserUseCase;
            _deleteSystemUserUseCase = deleteSystemUserUseCase;
        }

        public async Task<IPageResultDto<SystemUserDto>> GetAll(PaginationFilter? paginationFilter = null)
        {
            List<SystemUserDto> list;
            if (paginationFilter == null)
            {
                list = await Task.Run(() => _repository.GetAllAsNoTracking()
                    .ProjectTo<SystemUserDto>(Mapper.ConfigurationProvider)
                    .ToList()).ConfigureAwait(false);

                return new PageResultDto<SystemUserDto>(list);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<SystemUserDto>(Mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemUserDto>(paginationFilter, list);
        }

        public async Task<ListResultDto<LookupDto>> FindByName(string name)
        {
            var success = int.TryParse(name, out var number);
            List<LookupDto> list = new();

            if (success)
            {
                var entity = await _repository.GetById(number).ConfigureAwait(false);
                if (entity != null)
                {
                    var dto = Mapper.Map<LookupDto>(new LookupEntity {Key = entity.Id, Value = entity.Name});
                    list = new List<LookupDto> {dto};
                }
            }
            else if (!string.IsNullOrEmpty(name))
            {
                list = await Task.Run(() => _repository.FindByName(name)
                    .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
                    .ToList()).ConfigureAwait(false);
            }

            return new ListResultDto<LookupDto>(list);
        }

        public async Task<ISingleResultDto<SystemUserDto>> GetById(int id)
        {
            var entity = await _repository.GetById(id).ConfigureAwait(false);
            var dto = Mapper.Map<SystemUserDto>(entity);
            return new SingleResultDto<SystemUserDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto)
        {
            var validator = new SystemUserCreateValidation();

            var results = await validator.ValidateAsync(dto).ConfigureAwait(false);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
            }

            var mappedObject = Mapper.Map<SystemUser>(dto);

            var result = await _createSystemUserUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto)
        {
            var validator = new SystemUserEditValidation();

            var results = await validator.ValidateAsync(dto).ConfigureAwait(false);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
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