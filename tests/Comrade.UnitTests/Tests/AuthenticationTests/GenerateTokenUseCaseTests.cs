#region

using System.Threading.Tasks;
using comrade.Application.Dtos;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AuthenticationTests.Bases;
using comrade.UnitTests.Tests.AuthenticationTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.UnitTests.Tests.AuthenticationTests
{
    public sealed class GenerateTokenUseCaseTests

    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public GenerateTokenUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AuthenticationDtoTestData))]
        public async Task GenerateTokenLoginUseCase_Test(int expected, AuthenticationDto testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_GenerateTokenLoginUseCase_Test" + testObjectInput.Key)
                .Options;
            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var generateTokenLoginUseCase = _authenticationInjectionUseCase.GetGenerateTokenLoginUseCase(context);
            var result = await generateTokenLoginUseCase.Execute(testObjectInput.Key, testObjectInput.Password);
            Assert.Equal(expected, result.Code);
        }
    }
}