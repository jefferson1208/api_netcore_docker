using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using App.Docker.Api;
using App.Docker.Api.ViewModels.Products;
using App.Docker.Tests.Fixtures;
using App.Docker.Tests.Integration.Configuration;
using Xunit;

namespace App.Docker.Tests.Integration.Controllers
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ProductControllerTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _integrationTestsFixture;

        public ProductControllerTests(IntegrationTestsFixture<StartupTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }

        [Fact(DisplayName = "Should return an OK code")]
        [Trait(name: "Integration", value: "")]
        public async Task GetAll_ShouldReturnOkCode()
        {
            //ARRANGE
            await _integrationTestsFixture.SignIn();
            _integrationTestsFixture._client.AssignToken(_integrationTestsFixture.UserToken);

            //ACT
            
            var products = await _integrationTestsFixture._client.GetAsync("api/v1/products");

            //ASSERT

            products.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, products.StatusCode);
        }

        [Fact(DisplayName = "Should create successful product")]
        [Trait(name: "Integration", value: "")]
        public async Task CreateProduct_ShouldCreateSuccessfulProduct()
        {
            //ARRANGE
            await _integrationTestsFixture.SignIn();
            _integrationTestsFixture._client.AssignToken(_integrationTestsFixture.UserToken);
            
            var product = new ProductViewModel
            {
                Name = "Televisor",
                Description = "TV Samsung 50 polegadas",
                Price = 3500
            };

            //ACT

            var products = await _integrationTestsFixture._client.PostAsJsonAsync("api/v1/products/create-product", product);

            //ASSERT

            products.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, products.StatusCode);
        }

        [Fact(DisplayName = "Should update successful product")]
        [Trait(name: "Integration", value: "")]
        public async Task UpdateProduct_ShouldUpdateSuccessfulProduct()
        {
            //ARRANGE
            await _integrationTestsFixture.SignIn();
            _integrationTestsFixture._client.AssignToken(_integrationTestsFixture.UserToken);

            var product = new UpdateProductViewModel
            {
                Id = System.Guid.Parse("D9C6142F-EE5D-4EB2-ADDE-02ABF58921AB"), // informar um id de produto existente
                Description = "Iphone 12 Feito em 2022",
                Price = 12500
            };

            //ACT UpdateProductViewModel

            var products = await _integrationTestsFixture._client.PutAsJsonAsync("api/v1/products/update-product", product);

            //ASSERT

            products.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, products.StatusCode);
        }
    }
}
