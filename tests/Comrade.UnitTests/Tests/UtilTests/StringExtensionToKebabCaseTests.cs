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
            var teste = "Last in Line";
            var objetivo = "last-in-line";

            var restult = teste.ToKebabCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}