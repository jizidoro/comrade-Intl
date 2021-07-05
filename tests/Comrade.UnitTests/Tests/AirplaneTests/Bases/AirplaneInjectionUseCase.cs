#region

using System.Collections.Generic;
using Comrade.Application.Services;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases
{
    public sealed class AirplaneInjectionUseCase
    {
        public AirplaneCreateUseCase GetAirplaneCreateUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var airplaneRepository = new AirplaneRepository(context);

            var airplaneValidateSameCode = new AirplaneValidateSameCode(airplaneRepository);

            var airplaneCreateValidation = new AirplaneCreateValidation(airplaneRepository, airplaneValidateSameCode);

            return new AirplaneCreateUseCase(airplaneRepository, airplaneCreateValidation, uow);
        }

        public AirplaneEditUseCase GetAirplaneEditUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var airplaneRepository = new AirplaneRepository(context);

            var airplaneValidateSameCode = new AirplaneValidateSameCode(airplaneRepository);

            var airplaneEditValidation = new AirplaneEditValidation(airplaneRepository, airplaneValidateSameCode);

            return new AirplaneEditUseCase(airplaneRepository, airplaneEditValidation, uow);
        }
    }
}