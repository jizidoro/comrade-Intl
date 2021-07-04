#region

using System;
using System.Linq;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public static class ComradeContextFake
    {
        public static void AddDataFakeContext(IServiceCollection serviceCollection)
        {
            var context = serviceCollection.BuildServiceProvider()
                .GetService<ComradeContext>();

            var result = context?.Airplanes.Count();


            context?.Airplanes.Add(new Airplane()
            {
                Code = "Test",
                Model = "Test",
                PassengerQuantity = 666,
                RegisterDate = DateTime.UtcNow,
            });


            context?.SaveChanges();
        }
    }
}