#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Filters;
using Comrade.Application.Interfaces;
using Comrade.Application.Queries;
using Comrade.WebApi.Bases;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace Comrade.WebApi.UseCases.V1.AirplaneApi
{
    [Authorize]
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
        [Route("listar")]
        public async Task<IActionResult> Listar([FromQuery] PaginationQuery? paginationQuery)
        {
            try
            {
                PaginationFilter? paginationFilter = null;
                if (paginationQuery != null)
                {
                    paginationFilter = _mapper.Map<PaginationQuery, PaginationFilter>(paginationQuery);
                }

                var result = await _airplaneAppService.Listar(paginationFilter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        /// <summary>
        ///     obter por id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("obter/{id:int}")]
        public async Task<IActionResult> Obter(int id)
        {
            try
            {
                var result = await _airplaneAppService.Obter(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        [Route("incluir")]
        [HttpPost]
        public async Task<IActionResult> Incluir([FromBody] AirplaneIncluirDto dto)
        {
            try
            {
                var result = await _airplaneAppService.Incluir(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] AirplaneEditarDto dto)
        {
            try
            {
                var result = await _airplaneAppService.Editar(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var result = await _airplaneAppService.Excluir(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }
    }
}