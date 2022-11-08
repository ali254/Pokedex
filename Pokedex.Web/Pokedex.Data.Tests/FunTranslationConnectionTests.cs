using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Pokedex.Core.Entity;
using Pokedex.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Data.Tests
{
    public class FunTranslationConnectionTests
    {
        private readonly FunTranslationConnection _funTranslationConnection;
        private HttpClient _httpclient;
        private Mock<HttpMessageHandler> _httpMessageHandler;

        public FunTranslationConnectionTests()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpclient = new HttpClient(_httpMessageHandler.Object);
            _httpclient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/");

            _funTranslationConnection = new FunTranslationConnection(_httpclient);
        }

        [Fact]
        public async Task GetTranslation_SendsHttpRequest()
        {
            var textToTranslate = "textToTranslate";
            var ct = new CancellationToken();
            var jsonString = JsonConvert.SerializeObject(new TranslationResult() { Contents = new TranslationContent() { Text = "hehe", Translated = "asdasdas", Translation = "yoda" } });

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });

            await _funTranslationConnection.GetTranslationAsync(textToTranslate, FunTranslationEnum.Shakespeare, ct);

            _httpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(x => x.Method == HttpMethod.Post && x.RequestUri.Segments.Last() == FunTranslationEnum.Shakespeare.ToString()),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetTranslation_ReturnsExpectedResult_WhenFailedResponse()
        {
            var textToTranslate = "textToTranslate";
            var ct = new CancellationToken();
            var jsonString = JsonConvert.SerializeObject(new TranslationResult() { Contents = new TranslationContent() { Text = "hehe", Translated = "asdasdas", Translation = "yoda" } });

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(jsonString)
                });

            var result = await _funTranslationConnection.GetTranslationAsync(textToTranslate, FunTranslationEnum.Shakespeare, ct);

            Assert.IsType<TranslationResult>(result);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetTranslation_ReturnsExpectedResult_WhenSuccessResponse()
        {
            var textToTranslate = "textToTranslate";
            var ct = new CancellationToken();
            var expected = new TranslationResult() { Contents = new TranslationContent() { Text = "hehe", Translated = "asdasdas", Translation = "yoda" } };
            var jsonString = JsonConvert.SerializeObject(expected);

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });

            var result = await _funTranslationConnection.GetTranslationAsync(textToTranslate, FunTranslationEnum.Shakespeare, ct);

            Assert.IsType<TranslationResult>(result);
            Assert.True(result.Success);
            Assert.Equal(expected.Contents.Text, result.Contents.Text);
            Assert.Equal(expected.Contents.Translated, result.Contents.Translated);
            Assert.Equal(expected.Contents.Translation, result.Contents.Translation);
        }
    }
}
