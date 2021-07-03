#region

using System.Threading.Tasks;
using comrade.Application.Dtos;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AthenticationTests.Bases;
using comrade.UnitTests.Tests.AthenticationTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.UnitTests.Tests.AthenticationTests
{
    public sealed class GenerateTokenUseCaseTests

    {
        private readonly AthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public GenerateTokenUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AthenticationDtoTestData))]
        public async Task Test_GenerateTokenLoginUseCase(int expected, AthenticationDto testeEntrada)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_token" + testeEntrada.Key)
                .Options;
            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var generateTokenLoginUseCase = _authenticationInjectionUseCase.GetGenerateTokenLoginUseCase(context);
            var result = await generateTokenLoginUseCase.Execute(testeEntrada.Key, testeEntrada.Password);
            Assert.Equal(expected, result.Code);
        }
    }
}