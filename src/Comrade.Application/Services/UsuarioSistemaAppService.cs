﻿#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.UserSystemDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Utils;
using Comrade.Application.Validations.UserSystemValidations;
using Comrade.Core.UserSystemCore;
using Comrade.Core.UserSystemCore.UseCases;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services
{
    public class UserSystemAppService : AppService, IUserSystemAppService
    {
        private readonly UserSystemCreateUseCase _createUserSystemUseCase;
        private readonly UserSystemDeleteUseCase _deleteUserSystemUseCase;
        private readonly UserSystemEditUseCase _editUserSystemUseCase;
        private readonly IUserSystemRepository _repository;

        public UserSystemAppService(IUserSystemRepository repository,
            UserSystemEditUseCase editUserSystemUseCase,
            UserSystemCreateUseCase createUserSystemUseCase,
            UserSystemDeleteUseCase deleteUserSystemUseCase,
            IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _editUserSystemUseCase = editUserSystemUseCase;
            _createUserSystemUseCase = createUserSystemUseCase;
            _deleteUserSystemUseCase = deleteUserSystemUseCase;
        }

        public async Task<IPageResultDto<UserSystemDto>> GetAll(PaginationFilter paginationFilter = null)
        {
            List<UserSystemDto> list;
            if (paginationFilter == null)
            {
                list = await Task.Run(() => _repository.GetAll()
                    .ProjectTo<UserSystemDto>(Mapper.ConfigurationProvider)
                    .ToList());

                return new PageResultDto<UserSystemDto>(list);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            list = await Task.Run(() => _repository.GetAll().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<UserSystemDto>(Mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<UserSystemDto>(paginationFilter, list);
        }

        public async Task<ListResultDto<LookupDto>> FindByName(string name)
        {
            var success = int.TryParse(name, out var number);
            List<LookupDto> list = new();

            if (success)
            {
                var entity = await _repository.GetById(number);
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
                    .ToList());
            }

            return new ListResultDto<LookupDto>(list);
        }

        public async Task<ISingleResultDto<UserSystemDto>> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            var dto = Mapper.Map<UserSystemDto>(entity);
            return new SingleResultDto<UserSystemDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Create(UserSystemCreateDto dto)
        {
            var validator = new UserSystemCreateValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
            }

            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _createUserSystemUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Edit(UserSystemEditDto dto)
        {
            var validator = new UserSystemEditValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listErrors = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listErrors);
            }

            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _editUserSystemUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Delete(int id)
        {
            var result = await _deleteUserSystemUseCase.Execute(id);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}