#region

using System.Linq;
using System.Reflection;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public static class ComradeContextFake
    {
        private const string JsonPath = "Comrade.Infrastructure.SeedData";
        private static readonly object SyncLock = new();

        public static void AddDataFakeContext(IServiceCollection serviceCollection)
        {
            var context = serviceCollection.BuildServiceProvider()
                .GetService<ComradeContext>();

            var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

            if (context != null && context.Airplanes.Any())
            {
                return;
            }

            lock (SyncLock)
            {
                if (context != null && assembly is not null)
                {
                    var airplanes = assembly.GetManifestResourceStream($"{JsonPath}.airplane.json");
                    var oto = JsonUtilities.GetListFromJson<Airplane>(airplanes);
                    context.Airplanes.AddRange(oto!);

                    var systemUsers =
                        assembly.GetManifestResourceStream($"{JsonPath}.systemUser.json");
                    var oto2 = JsonUtilities.GetListFromJson<SystemUser>(systemUsers);
                    context.SystemUsers.AddRange(oto2!);

                    if (context.Airplanes.Any())
                    {
                        return;
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}