#region

using System;
using System.Reflection;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Extensions;

#endregion

namespace Comrade.UnitTests.DataInjectors
{
    public static class InjectDataOnContextBase
    {
        private const string JsonPath = "Comrade.Infrastructure.SeedData";

        #region DadosIniciais

        public static void InitializeDbForTests(ComradeContext db)
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

                if (assembly is not null)
                {
                    var airplaneJson = assembly.GetManifestResourceStream($"{JsonPath}.airplane.json");
                    var airplanes = JsonUtilities.GetListFromJson<Airplane>(airplaneJson);
                    db.Airplanes.AddRange(airplanes!);

                    var systemUserJson =
                        assembly.GetManifestResourceStream($"{JsonPath}.systemUser.json");
                    var systemUsers = JsonUtilities.GetListFromJson<SystemUser>(systemUserJson);
                    db.SystemUsers.AddRange(systemUsers!);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}