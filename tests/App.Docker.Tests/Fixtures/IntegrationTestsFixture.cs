using Bogus;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using App.Docker.Api;
using App.Docker.Infra.CrossCutting.Identity.Model;
using App.Docker.Tests.Integration.Configuration;
using Xunit;

namespace App.Docker.Tests.Fixtures
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection: ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string UserToken;
        public HttpClient _client;
        private readonly AppFactory<TStartup> _factory;
        public IntegrationTestsFixture()
        {
            _factory = new AppFactory<TStartup>();
            _client = _factory.CreateClient();
        }

        public async Task SignIn()
        {
            var user = new SignInViewModel
            {
                UserName = "jefferson1208",
                Password = "Teste@123"
            };

            var response = await _client.PostAsJsonAsync("api/v1/users/signIn", user);
            response.EnsureSuccessStatusCode();
            var token = JObject.Parse(await response.Content.ReadAsStringAsync());

            UserToken = token.SelectToken("data").Value<string>();
        }

        public SignUpViewModel GenerateSignUpValid()
        {
            var faker = new Faker("pt_BR");
            var name = faker.Person.FirstName;
            var lastName = faker.Person.LastName;
            var passWord = "Teste@123";

            var signUp = new SignUpViewModel
            {
                UserName = faker.Internet.UserName(name, lastName),
                FullName = $"{name} {lastName}",
                Email = faker.Internet.Email(name, lastName),
                Password = passWord,
                PasswordConfirmation = passWord
            };

            return signUp;
        }
        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
