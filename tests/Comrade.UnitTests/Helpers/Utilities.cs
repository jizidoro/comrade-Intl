#region

using System;
using System.Reflection;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Extensions;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public static class Utilities
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
                    var airplanes = assembly.GetManifestResourceStream($"{JsonPath}.airplane.json");
                    var oto = JsonUtilities.GetListFromJson<Airplane>(airplanes);
                    db.Airplanes.AddRange(oto!);

                    var systemUsers =
                        assembly.GetManifestResourceStream($"{JsonPath}.systemUser.json");
                    var oto2 = JsonUtilities.GetListFromJson<SystemUser>(systemUsers);
                    db.SystemUsers.AddRange(oto2!);
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