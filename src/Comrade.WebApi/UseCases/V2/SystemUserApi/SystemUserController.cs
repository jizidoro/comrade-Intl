#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.SystemUserDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Queries;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace Comrade.WebApi.UseCases.V2.SystemUserApi
{
    // [Authorize]
    [FeatureGate(CustomFeature.SystemUser)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISystemUserAppService _systemUserAppService;

        public SystemUserController(
            ISystemUserAppService systemUserAppService, IMapper mapper)
        {
            _systemUserAppService = systemUserAppService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
        {
            try
            {
                PaginationFilter? paginationFilter = null;
                if (paginationQuery != null)
                {
                    paginationFilter = _mapper.Map<PaginationQuery, PaginationFilter>(paginationQuery);
                }

                var result = await _systemUserAppService.GetAll(paginationFilter).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }


        [HttpGet]
        [Route("get-by-id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _systemUserAppService.GetById(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] SystemUserCreateDto dto)
        {
            try
            {
                var result = await _systemUserAppService.Create(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpPut]
        [Route("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Edit([FromBody] SystemUserEditDto dto)
        {
            try
            {
                var result = await _systemUserAppService.Edit(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _systemUserAppService.Delete(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }
    }
}