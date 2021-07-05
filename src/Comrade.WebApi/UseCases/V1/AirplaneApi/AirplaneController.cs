#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Queries;
using Comrade.WebApi.Bases;
using Comrade.WebApi.Modules.Common;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace Comrade.WebApi.UseCases.V1.AirplaneApi
{
    // [Authorize]
    [FeatureGate(CustomFeature.Airplane)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AirplaneController : ComradeController
    {
        private readonly IAirplaneAppService _airplaneAppService;
        private readonly ILogger<AirplaneController> _logger;
        private readonly IMapper _mapper;

        public AirplaneController(
            IAirplaneAppService airplaneAppService, IMapper mapper, ILogger<AirplaneController> logger)
        {
            _airplaneAppService = airplaneAppService;
            _mapper = mapper;
            _logger = logger;
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

                var result = await _airplaneAppService.GetAll(paginationFilter).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }

        /// <summary>
        ///     get por id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("get-by-id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _airplaneAppService.GetById(id).ConfigureAwait(false);
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
        public async Task<IActionResult> Create([FromBody] AirplaneCreateDto dto)
        {
            try
            {
                var result = await _airplaneAppService.Create(dto).ConfigureAwait(false);
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
        public async Task<IActionResult> Edit([FromBody] AirplaneEditDto dto)
        {
            try
            {
                var result = await _airplaneAppService.Edit(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ISingleResultDto<EntityDto>))]
        [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _airplaneAppService.Delete(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
            }
        }
    }
}