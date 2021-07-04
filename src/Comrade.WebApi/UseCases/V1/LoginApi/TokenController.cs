#region

using System.Threading.Tasks;
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
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public TokenController(
            IAuthenticationAppService authenticationAppService
        )
        {
            _authenticationAppService = authenticationAppService;
        }


        [HttpPost]
        [Route("generate-token")]
        public async Task<ActionResult> GenerateToken([FromBody] AuthenticationDto dto)
        {
            var result = await _authenticationAppService.GenerateToken(dto);

            return Ok(result);
        }
    }
}