#region

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Interfaces;
using Comrade.Application.Lookups;
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
        private readonly ISystemUserAppService _systemUserAppService;

        public CommonController(IServiceProvider serviceProvider, ISystemUserAppService systemUserAppService)
        {
            _serviceProvider = serviceProvider;
            _systemUserAppService = systemUserAppService;
        }


        [HttpGet]
        [Route("lookup-system-user")]
        public async Task<IActionResult> GetLookupSystemUser()
        {
            try
            {
                var service = _serviceProvider.GetService<ILookupServiceApp<SystemUser>>();

                var result = await service?.GetLookup()!;

                return Ok(new ListResultDto<LookupDto>(result));
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpGet]
        [Route("lookup-predicate-system-user-by-name/{name}")]
        public async Task<IActionResult> GetLookupPredicateSystemUserByName(string name)
        {
            try
            {
                var service = _serviceProvider.GetService<ILookupServiceApp<SystemUser>>();

                Expression<Func<SystemUser, bool>> expression = x => x.Name.Contains(name);
                var result = await service?.GetLookup(expression)!;

                return Ok(new ListResultDto<LookupDto>(result));
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<EntityDto>(e));
            }
        }

        [HttpGet]
        [Route("lookup-system-user-by-name/{name}")]
        public async Task<IActionResult> GetLookupSystemUserByName(string name)
        {
            try
            {
                var result = await _systemUserAppService.FindByName(name);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<EntityDto>(e));
            }
        }
    }
}