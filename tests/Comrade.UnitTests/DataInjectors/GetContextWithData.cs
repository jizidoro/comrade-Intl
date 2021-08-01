﻿#region

using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;

#endregion

namespace Comrade.UnitTests.DataInjectors
{
    public static class GetContextWithData
    {
        public static ComradeContext Excute(ComradeContext context)
        {
            context.Airplanes.Add(new Airplane
            {
                Id = 70,
                Code = "Test",
                Model = "Test",
                PassengerQuantity = 666,
                RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia()
            });

            context.SaveChanges();

            return context;
        }
    }
}