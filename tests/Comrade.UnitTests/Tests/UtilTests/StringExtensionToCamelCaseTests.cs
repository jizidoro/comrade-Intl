#region

using Comrade.Application.Extensions;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToCamelCaseTests
    {
        [Fact]
        public void StringExtension_ToCamelCase()
        {
            var testObject = "Last in Line";
            var objetivo = "lastInLine";

            var restult = testObject.ToCamelCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}