#region

using Comrade.Application.Extensions;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToKebabCaseTests
    {
        [Fact]
        public void StringExtension_ToKebabCase()
        {
            var testObject = "Last in Line";
            var objetivo = "last-in-line";

            var restult = testObject.ToKebabCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}