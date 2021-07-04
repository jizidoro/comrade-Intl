﻿#region

using Comrade.Application.Extensions;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToSnakeCaseTests
    {
        [Fact]
        public void StringExtension_ToSnakeCase()
        {
            var testObject = "Last in Line";
            var objetivo = "last_in_line";

            var restult = testObject.ToSnakeCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}