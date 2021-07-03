#region

using System.Text.Json;
using System.Text.Json.Serialization;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public class DescerializeJsonObject<TEntity>
    {
        public TEntity Excute(string entradaJson)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<TEntity>(entradaJson, options);
        }
    }
}