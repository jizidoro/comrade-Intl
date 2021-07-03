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
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoAppService _autenticacaoAppService;

        public AutenticacaoController(
            IAutenticacaoAppService autenticacaoAppService
        )
        {
            _autenticacaoAppService = autenticacaoAppService;
        }

        [HttpPost]
        [Route("expirar-senha")]
        public async Task<IActionResult> ExpirarSenha([FromBody] AutenticacaoDto dto)
        {
            try
            {
                var result = await _autenticacaoAppService.ExpirarSenha(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AutenticacaoDto>(e));
            }
        }

        [HttpPost]
        [Route("esquecer-senha")]
        public async Task<IActionResult> EsquecerSenha([FromBody] AutenticacaoDto dto)
        {
            try
            {
                var result = await _autenticacaoAppService.EsquecerSenha(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AutenticacaoDto>(e));
            }
        }
    }
}