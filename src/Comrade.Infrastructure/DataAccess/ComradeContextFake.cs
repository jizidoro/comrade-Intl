#region

using System;
using System.Linq;
using System.Reflection;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Extensions;
using Comrade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public static class ComradeContextFake
    {
        private const string JsonPath = "Comrade.Infrastructure.SeedData";
        private static readonly object _syncLock = new object();

        public static bool AddDataFakeContext(IServiceCollection serviceCollection)
        {
            var context = serviceCollection.BuildServiceProvider()
                .GetService<ComradeContext>();

            var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

            if (context != null && context.Airplanes.Any())
            {
                return true;
            }

            lock (_syncLock)
            {
                if (context != null && assembly is not null)
                {
                    context.Airplanes.AddRange(
                        JsonUtilities.GetListFromJson<Airplane>(
                            assembly.GetManifestResourceStream($"{JsonPath}.airplane.json")));

                    context.SystemUsers.AddRange(
                        JsonUtilities.GetListFromJson<SystemUser>(
                            assembly.GetManifestResourceStream($"{JsonPath}.systemUser.json")));

                    context?.SaveChanges();
                }
            }

            return true;
        }
    }
}