#region

using System;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1.LoginApi
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AthenticationController : ControllerBase
    {
        private readonly IAthenticationAppService _authenticationAppService;

        public AthenticationController(
            IAthenticationAppService authenticationAppService
        )
        {
            _authenticationAppService = authenticationAppService;
        }

        [HttpPost]
        [Route("expirar-password")]
        public async Task<IActionResult> ExpirarPassword([FromBody] AthenticationDto dto)
        {
            try
            {
                var result = await _authenticationAppService.ExpirarPassword(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AthenticationDto>(e));
            }
        }

        [HttpPost]
        [Route("esquecer-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] AthenticationDto dto)
        {
            try
            {
                var result = await _authenticationAppService.ForgotPassword(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AthenticationDto>(e));
            }
        }
    }
}