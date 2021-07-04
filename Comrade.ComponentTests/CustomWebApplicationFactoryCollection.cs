using Xunit;

namespace Comrade.ComponentTests
{
    [CollectionDefinition("WebApi Collection")]
    public sealed class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactoryFixture>
    {
    }
}
