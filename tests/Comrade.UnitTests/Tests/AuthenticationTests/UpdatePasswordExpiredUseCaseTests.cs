#region

using System.Threading.Tasks;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests
{
    public sealed class UpdatePasswordUseCaseTests
    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public UpdatePasswordUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task UpdatePasswordUseCase_Test()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UpdatePasswordUseCase_Test")
                .Options;


            var testObject = new SystemUser
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

            var repository = new SystemUserRepository(context);
            var returnBefore = await repository.GetById(testObject.Id);
            var passwordBefore = returnBefore.Password;

            var updatePasswordUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordUseCase(context);
            var result = await updatePasswordUseCase.Execute(testObject);
            _output.WriteLine(result.Message);

            var returnAfter = await repository.GetById(testObject.Id);
            var passwordAfter = returnAfter.Password;

            Assert.NotEqual(passwordBefore, passwordAfter);
        }
    }
}