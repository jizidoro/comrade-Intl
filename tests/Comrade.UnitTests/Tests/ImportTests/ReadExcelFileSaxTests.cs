#region

using System.Threading.Tasks;
using Comrade.Application.Imports.ImportFunctions;
using Comrade.UnitTests.Mocks;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.ImportTests
{
    public class ReadExcelFileSaxTests
    {
        private readonly ObterIFormFileMock _obterIFormFileMock = new();

        [Fact]
        public async Task ReadExcelFileSaxTest()
        {
            var arquivo = await _obterIFormFileMock.Execute();

            var result = ReadExcelFileSax.Execute(arquivo);

            Assert.NotEmpty(result);
            Assert.Equal(10, result.Count);
        }
    }
}