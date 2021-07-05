﻿#region

using Comrade.Application.Extensions;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToInt32Tests
    {
        [Fact]
        public void StringExtension_ToInt32()
        {
            var testObject = "55";
            int goal = 55;

            var result = testObject.ToInt32();
            
            Assert.Equal(goal, result);
        }
    }
}