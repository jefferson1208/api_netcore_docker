using App.Docker.Tests.Fixtures;
using Xunit;

namespace App.Docker.Tests.Unity.Entities
{
    [Collection(nameof(ProductCollection))]
    public class ProductTests
    {
        private readonly ProductTestsFixture _productTestsFixture;

        public ProductTests(ProductTestsFixture productTestsFixture)
        {
            _productTestsFixture = productTestsFixture;
        }

        [Fact(DisplayName = "Should validate the product entity")]
        [Trait(name: "Unity", value: "Product")]
        public void Products_Validate_ShouldValidateProductEntity()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfValidProducts(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.True(product.ValidationResult.IsValid);
            }
        }

        [Fact(DisplayName = "Should invalidate the product entity")]
        [Trait(name: "Unity", value: "Product")]
        public void Products_Validate_ShouldInvalidateProductEntity()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfInvalidProducts(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.False(product.ValidationResult.IsValid);
            }
        }


        [Fact(DisplayName = "Should validate the command to create a product")]
        [Trait(name: "Unity", value: "CreateProductCommand")]
        public void Products_Validate_ShouldValidateCommandCreateProduct()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfCommandsCreateValid(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.True(product.ValidationResult.IsValid);
            }
        }

        [Fact(DisplayName = "Should invalidate the command to create a product")]
        [Trait(name: "Unity", value: "CreateProductCommand")]
        public void Products_Validate_ShouldInvalidateCommandCreateProduct()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfCommandsCreateInvalid(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.False(product.ValidationResult.IsValid);
            }
        }

        [Fact(DisplayName = "Should validate the command to update a product")]
        [Trait(name: "Unity", value: "UpdateProductCommand")]
        public void Products_Validate_ShouldValidateCommandUpdateProduct()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfCommandsUpdateValid(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.True(product.ValidationResult.IsValid);
            }
        }

        [Fact(DisplayName = "Should invalidate the command to update a product")]
        [Trait(name: "Unity", value: "UpdateProductCommand")]
        public void Products_Validate_ShouldInvalidateCommandUpdateProduct()
        {
            //ARRANGE
            var products = _productTestsFixture.GenerateListOfCommandsUpdateInvalid(100);

            //ACT
            foreach (var product in products)
            {
                product.Validate();


                //ASSERT

                Assert.False(product.ValidationResult.IsValid);
            }
        }
    }
}
