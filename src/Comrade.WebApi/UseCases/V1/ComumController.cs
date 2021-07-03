#region

using System;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Interfaces;
using comrade.Domain.Models;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1
{
    [FeatureGate(CustomFeature.Comum)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ComumController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserSystemAppService _userSystemAppService;

        public ComumController(IServiceProvider serviceProvider, IUserSystemAppService userSystemAppService)
        {
            _serviceProvider = serviceProvider;
            _userSystemAppService = userSystemAppService;
        }


        [HttpGet]
        [Route("lookup-usuario-sistema")]
        public async Task<IActionResult> GetLookupUserSystem()
        {
            try
            {
                var service = _serviceProvider.GetService<ILookupServiceApp<UserSystem>>();

                var result = await service?.GetLookup()!;

                return Ok(new ListResultDto<LookupDto>(result));
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        [HttpGet]
        [Route("lookup-usuario-sistema-por-name/{name}")]
        public async Task<IActionResult> GetLookupUserSystemByNone(string name)
        {
            try
            {
                var result = await _userSystemAppService.FindByName(name);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UserSystemDto>(e));
            }
        }
    }
}