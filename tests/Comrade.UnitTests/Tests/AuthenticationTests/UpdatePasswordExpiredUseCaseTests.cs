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
    public sealed class UpdatePasswordUseCaseTests
    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public UpdatePasswordUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Test_UpdatePasswordUseCase()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_update_password_expired_usecase")
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
            var retornoAntes = await repository.GetById(testObject.Id);
            var passwordAntes = retornoAntes.Password;

            var updatePasswordUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordUseCase(context);
            var result = await updatePasswordUseCase.Execute(testObject);
            _output.WriteLine(result.Message);

            var retornoDepois = await repository.GetById(testObject.Id);
            var passwordDepois = retornoDepois.Password;

            Assert.NotEqual(passwordAntes, passwordDepois);
        }
    }
}