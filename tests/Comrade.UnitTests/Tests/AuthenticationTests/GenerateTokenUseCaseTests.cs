#region

using System.Threading.Tasks;
using Comrade.Application.Dtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Comrade.UnitTests.Tests.AuthenticationTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests
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
        public async Task ValidateLoginUseCase_Test(int expected, AuthenticationDto testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_ValidateLoginUseCase_Test" + testObjectInput.Key)
                .Options;
            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var generateTokenLoginUseCase = _authenticationInjectionUseCase.GetValidateLoginUseCase(context);
            var result = await generateTokenLoginUseCase.Execute(testObjectInput.Key, testObjectInput.Password);
            Assert.Equal(expected, result.Code);
        }
    }
}