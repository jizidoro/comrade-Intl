#region

using System;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public static class ComradeContextFake
    {
        public static void AddDataFakeContext(IServiceCollection serviceCollection) {

            var context = serviceCollection.BuildServiceProvider()
                .GetService<ComradeContext>();

            context?.Airplanes.Add(new Airplane()
            {
                Id = 70,
                Code = "Test",
                Model = "Test",
                PassengerQuantity = 666,
                RegisterDate = DateTime.UtcNow,
            });

            context?.SaveChanges();
        }
    }
}