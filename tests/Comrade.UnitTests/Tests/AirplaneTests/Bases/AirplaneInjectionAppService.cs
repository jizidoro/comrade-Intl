#region

using AutoMapper;
using comrade.Application.Services;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases
{
    public sealed class AirplaneInjectionAppService
    {
        public AirplaneAppService GetAirplaneAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var airplaneRepository = new AirplaneRepository(context);

            var airplaneValidateCodeRepetido = new AirplaneValidateCodeRepetido(airplaneRepository);


            var airplaneEditValidation =
                new AirplaneEditValidation(airplaneRepository, airplaneValidateCodeRepetido);
            var airplaneDeleteValidation = new AirplaneDeleteValidation(airplaneRepository);
            var airplaneCreateValidation =
                new AirplaneCreateValidation(airplaneRepository, airplaneValidateCodeRepetido);
            var airplaneCreateUseCase = new AirplaneCreateUseCase(airplaneRepository, airplaneCreateValidation, uow);
            var airplaneDeleteUseCase = new AirplaneDeleteUseCase(airplaneRepository, airplaneDeleteValidation, uow);
            var airplaneEditUseCase = new AirplaneEditUseCase(airplaneRepository, airplaneEditValidation, uow);

            return new AirplaneAppService(airplaneRepository, airplaneEditUseCase, airplaneCreateUseCase,
                airplaneDeleteUseCase, mapper);
        }
    }
}