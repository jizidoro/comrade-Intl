#region

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

#endregion

namespace Comrade.WebApi.Modules.Common
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            var result = Regex.Replace(value?.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();

            return value == null ? null : result;
        }
    }
}