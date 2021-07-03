#region

using System.Threading.Tasks;
using Comrade.Application.Dtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AutenticacaoTests.Bases;
using Comrade.UnitTests.Tests.AutenticacaoTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AutenticacaoTests
{
    public sealed class GerarTokenUsecaseTests

    {
        private readonly AutenticacaoInjectionUseCase _autenticacaoInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public GerarTokenUsecaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AutenticacaoDtoTestData))]
        public async Task Test_GerarTokenLoginUsecase(int expected, AutenticacaoDto testeEntrada)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_token" + testeEntrada.Chave)
                .Options;
            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var gerarTokenLoginUsecase = _autenticacaoInjectionUseCase.ObterGerarTokenLoginUsecase(context);
            var result = await gerarTokenLoginUsecase.Execute(testeEntrada.Chave, testeEntrada.Senha);
            Assert.Equal(expected, result.Code);
        }
    }
}