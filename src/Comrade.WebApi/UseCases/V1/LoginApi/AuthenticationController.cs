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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public AuthenticationController(
            IAuthenticationAppService authenticationAppService
        )
        {
            _authenticationAppService = authenticationAppService;
        }

        [HttpPost]
        [Route("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] AuthenticationDto dto)
        {
            try
            {
                var result = await _authenticationAppService.UpdatePassword(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AuthenticationDto>(e));
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] AuthenticationDto dto)
        {
            try
            {
                var result = await _authenticationAppService.ForgotPassword(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AuthenticationDto>(e));
            }
        }
    }
}