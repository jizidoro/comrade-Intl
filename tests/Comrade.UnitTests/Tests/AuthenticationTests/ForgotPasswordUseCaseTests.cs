#region

using System.Threading.Tasks;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests
{
    public sealed class ForgotPasswordUseCaseTests

    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public ForgotPasswordUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ForgotPasswordUseCase_Test()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_ForgotPasswordUseCase_Test")
                .Options;


            var testObject = new SystemUser(1, "111", "777@testObject",
                "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=", "123",
                DateTimeBrasilia.GetDateTimeBrasilia());

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);

            var repository = new SystemUserRepository(context);
            var returnBefore = await repository.GetById(testObject.Id);
            var passwordBefore = returnBefore!.Password;

            var updatePasswordUseCase =
                _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var result = await updatePasswordUseCase.Execute(testObject);
            _output.WriteLine(result.Message);

            var returnAfter = await repository.GetById(testObject.Id);
            var passwordAfter = returnAfter!.Password;

            Assert.NotEqual(passwordBefore, passwordAfter);
        }
    }
}