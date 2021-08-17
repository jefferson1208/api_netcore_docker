using App.Docker.Tests.Fixtures;
using Xunit;

namespace App.Docker.Tests.Unity.Commands
{
    [Collection(nameof(ProductCollection))]
    public class ProductCommandTests
    {
        private readonly ProductTestsFixture _productTestsFixture;

        public ProductCommandTests(ProductTestsFixture productTestsFixture)
        {
            _productTestsFixture = productTestsFixture;
        }
    }
}
