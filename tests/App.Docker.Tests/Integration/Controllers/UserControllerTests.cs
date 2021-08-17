using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using App.Docker.Api;
using App.Docker.Tests.Fixtures;
using Xunit;

namespace App.Docker.Tests.Integration.Controllers
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UserControllerTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _integrationTestsFixture;

        public UserControllerTests(IntegrationTestsFixture<StartupTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }

        [Fact(DisplayName = "Should register a user successfully")]
        [Trait(name: "Integration", value: "")]
        public async Task SignUp_ShouldRegisterUserSucessfully()
        {
            //ARRANGE
            var user = _integrationTestsFixture.GenerateSignUpValid();

            //ACT
            var signUp = await _integrationTestsFixture._client.PostAsJsonAsync("api/v1/users/signUp", user);

            //ASSERT
            signUp.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, signUp.StatusCode);

        }
    }
}
