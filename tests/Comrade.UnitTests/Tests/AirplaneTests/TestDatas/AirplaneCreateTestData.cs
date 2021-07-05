#region

using System.Collections;
using System.Collections.Generic;
using Comrade.Domain.Models;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.TestDatas
{
    internal class AirplaneCreateTestData : IEnumerable<object[]>
    {
        #region TestData

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                200, new Airplane
                {
                    Code = "123",
                    Model = "234",
                    PassengerQuantity = 456
                }
            };
            yield return new object[]
            {
                400, new Airplane
                {
                    Code = "123",
                    PassengerQuantity = 456
                }
            };
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}