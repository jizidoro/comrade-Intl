#region

using System.Threading.Tasks;
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