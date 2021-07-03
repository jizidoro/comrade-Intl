#region

using System.Threading.Tasks;
using comrade.Application.Imports.ImportFunctions;
using comrade.UnitTests.Mocks;
using Xunit;

#endregion

namespace comrade.UnitTests.Tests.ImportTests
{
    public class ReadExcelFileSaxTests
    {
        private readonly GetIFormFileMock _getIFormFileMock = new();

        [Fact]
        public async Task ReadExcelFileSaxTest()
        {
            var arquivo = await _getIFormFileMock.Execute();

            var result = ReadExcelFileSax.Execute(arquivo);

            Assert.NotEmpty(result);
            Assert.Equal(10, result.Count);
        }
    }
}