#region

using System.Threading.Tasks;
using comrade.Domain.Extensions;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.UnitTests.Tests.AuthenticationTests
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


            var testObject = new UserSystem
            {
                Id = 1,
                Name = "111",
                Email = "777@testObject",
                Password = "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=",
                Situacao = true,
                Registration = "123",
                RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia()
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var repository = new UserSystemRepository(context);
            var returnBefore = await repository.GetById(testObject.Id);
            var passwordBefore = returnBefore.Password;

            var updatePasswordUseCase = _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var result = await updatePasswordUseCase.Execute(testObject);
            _output.WriteLine(result.Message);

            var returnAfter = await repository.GetById(testObject.Id);
            var passwordAfter = returnAfter.Password;

            Assert.NotEqual(passwordBefore, passwordAfter);
        }
    }
}