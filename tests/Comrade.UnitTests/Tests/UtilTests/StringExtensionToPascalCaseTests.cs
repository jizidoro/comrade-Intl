#region

using comrade.Application.Extensions;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToPascalCaseTests
    {
        [Fact]
        public void StringExtension_ToPascalCase()
        {
            var testObject = "Last in Line";
            var objetivo = "LastInLine";

            var result = testObject.ToPascalCase();

            Assert.NotEmpty(result);
            Assert.Equal(result, objetivo);
        }
    }
}