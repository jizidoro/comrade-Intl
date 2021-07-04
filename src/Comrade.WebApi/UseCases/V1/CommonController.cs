#region

using System;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Interfaces;
using comrade.Application.Lookups;
using Comrade.Domain.Models;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace Comrade.WebApi.UseCases.V1
{
    [FeatureGate(CustomFeature.Common)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CommonController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserSystemAppService _userSystemAppService;

        public CommonController(IServiceProvider serviceProvider, IUserSystemAppService userSystemAppService)
        {
            _serviceProvider = serviceProvider;
            _userSystemAppService = userSystemAppService;
        }


        [HttpGet]
        [Route("Lookup-user-sistema")]
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
        [Route("lookup-user-sistema-por-name/{name}")]
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