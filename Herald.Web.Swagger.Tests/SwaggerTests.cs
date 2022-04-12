using Microsoft.AspNetCore.Mvc.Testing;

using System.Threading.Tasks;

using Xunit;

namespace Herald.Web.Swagger.Tests
{
    public class SwaggerTests : IClassFixture<WebApplicationFactory<WebApiStub.Startup>>
    {
        private readonly WebApplicationFactory<WebApiStub.Startup> _factory;

        public SwaggerTests(WebApplicationFactory<WebApiStub.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldSwaggerUIWhenAddSwaggerIsOK()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/swagger");

            // Assert            
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldGetSwaggerJsonWhenUseSwaggerIsOK()
        {
            // Arrange
            var client = _factory.CreateClient();

            //
            var response = await client.GetAsync("1/swagger/v1.0.0.0/swagger.json");

            // Assert            
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
