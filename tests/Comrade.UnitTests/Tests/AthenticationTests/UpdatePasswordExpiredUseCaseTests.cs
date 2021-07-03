#region

using System.Threading.Tasks;
using comrade.Domain.Extensions;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.UnitTests.Tests.AthenticationTests
{
    public sealed class UpdatePasswordExpiredUseCaseTests
    {
        private readonly AthenticationInjectionUseCase _authenticationInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public UpdatePasswordExpiredUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Test_UpdatePasswordExpiredUseCase()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_update_password_expired_usecase")
                .Options;


            var teste = new UserSystem
            {
                Id = 1,
                Name = "111",
                Email = "777@teste",
                Password = "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=",
                Situacao = true,
                Registration = "123",
                RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia()
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var repository = new UserSystemRepository(context);
            var retornoAntes = await repository.GetById(teste.Id);
            var passwordAntes = retornoAntes.Password;

            var updatePasswordExpiredUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordExpiredUseCase(context);
            var result = await updatePasswordExpiredUseCase.Execute(teste);
            _output.WriteLine(result.Message);

            var retornoDepois = await repository.GetById(teste.Id);
            var passwordDepois = retornoDepois.Password;

            Assert.NotEqual(passwordAntes, passwordDepois);
        }
    }
}