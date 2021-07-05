#region

using System;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Comrade.WebApi.UseCases.V1.LoginApi
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
                var result = await _authenticationAppService.UpdatePassword(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] AuthenticationDto dto)
        {
            try
            {
                var result = await _authenticationAppService.ForgotPassword(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<EntityDto>(e));
            }
        }
    }
}