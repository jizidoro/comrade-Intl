#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Filters;
using comrade.Application.Interfaces;
using comrade.Application.Queries;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1.UserSystemApi
{
    // [Authorize]
    [FeatureGate(CustomFeature.UserSystem)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserSystemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserSystemAppService _userSystemAppService;

        public UserSystemController(
            IUserSystemAppService userSystemAppService, IMapper mapper)
        {
            _userSystemAppService = userSystemAppService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
        {
            try
            {
                PaginationFilter? paginationFilter = null;
                if (paginationQuery != null)
                {
                    paginationFilter = _mapper.Map<PaginationQuery, PaginationFilter>(paginationQuery);
                }

                var result = await _userSystemAppService.GetAll(paginationFilter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }


        [HttpGet]
        [Route("get-by-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _userSystemAppService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSystemCreateDto dto)
        {
            try
            {
                var result = await _userSystemAppService.Create(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody] UserSystemEditDto dto)
        {
            try
            {
                var result = await _userSystemAppService.Edit(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userSystemAppService.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }
    }
}