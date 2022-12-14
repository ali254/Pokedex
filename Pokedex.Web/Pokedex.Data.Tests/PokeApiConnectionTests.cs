using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Pokedex.Core.Entity;
using Pokedex.Core.Exceptions;
using Pokedex.Data.Caching;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Data.Tests
{
    public class PokeApiConnectionTests
    {
        private readonly PokeApiConnection _pokeApiConnection;
        private readonly HttpClient _httpclient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly Mock<ICachingService> _cachingService;

        public PokeApiConnectionTests()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _cachingService = new Mock<ICachingService>();
            _httpclient = new HttpClient(_httpMessageHandler.Object);
            _httpclient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/");

            _pokeApiConnection = new PokeApiConnection(_httpclient, _cachingService.Object);
        }

        [Fact]
        public async Task Get_SendsHttpRequest()
        {
            var pokeName = "mewtwo";
            var ct = new CancellationToken();
            var jsonString = JsonConvert.SerializeObject(new Pokemon() { Id = 1, Name = "mewtwo" });

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });

            await _pokeApiConnection.GetAsync(pokeName, ct);

            _httpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(x => x.Method == HttpMethod.Get && x.RequestUri.Segments.Last() == pokeName),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task Get_ThrowsApiException_WhenFailedResponse()
        {
            var pokeName = "mewtwo";
            var ct = new CancellationToken();
            var jsonString = JsonConvert.SerializeObject(new Pokemon() { Id = 1, Name = "mewtwo" });

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(jsonString)
                });

            var exception = await Assert.ThrowsAsync<ApiException>(async () => await _pokeApiConnection.GetAsync(pokeName, ct));

            Assert.Equal((int)HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task Get_Returns_ExpectedResponse()
        {
            var pokeName = "mewtwo";
            var ct = new CancellationToken();
            var expected = new Pokemon() { Id = 1, Name = "mewtwo" };
            var jsonString = JsonConvert.SerializeObject(expected);

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });

            var result = await _pokeApiConnection.GetAsync(pokeName, ct);

            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
        }
    }
}
