using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pokedex.Core.DTO;
using Pokedex.Service;
using Pokedex.Web.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Web.Tests
{
    public class PokemonControllerTests
    {
        private readonly PokemonController _controller;
        private readonly Mock<IPokemonService> _pokemonService;

        public PokemonControllerTests()
        {
            _pokemonService = new Mock<IPokemonService>();

            _controller = new PokemonController(_pokemonService.Object);
        }

        [Fact]
        public async Task Get_CallsPokemonService_WithCorrectParameter()
        {
            var ct = new CancellationToken();

            await _controller.Get("pokemon", ct);

            _pokemonService.Verify(x => x.GetAsync("pokemon", ct), Times.Once);
        }

        [Fact]
        public async Task Get_Returns200OkResult()
        {
            var ct = new CancellationToken();
            var expectedResult = new Pokemon() { Name = "pokemon" };
            _pokemonService.Setup(x => x.GetAsync("pokemon", ct)).Returns(Task.FromResult(expectedResult));

            var result = await _controller.Get("pokemon", ct);

            var objResult = ((ObjectResult)result.Result);
            Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
            Assert.Equal(expectedResult, objResult.Value);
            Assert.Equal(expectedResult.Name, ((Pokemon)objResult.Value).Name);
        }

        [Fact]
        public async Task GetTranslated_CallsPokemonService_WithCorrectParameter()
        {
            var ct = new CancellationToken();

            await _controller.GetTranslated("pokemon", ct);

            _pokemonService.Verify(x => x.GetTranslatedAsync("pokemon", ct), Times.Once);
        }

        [Fact]
        public async Task GetTranslated_Returns200OkResult()
        {
            var ct = new CancellationToken();
            var expectedResult = new Pokemon() { Name = "pokemon" };
            _pokemonService.Setup(x => x.GetTranslatedAsync("pokemon", ct)).Returns(Task.FromResult(expectedResult));

            var result = await _controller.GetTranslated("pokemon", ct);

            var objResult = ((ObjectResult)result.Result);
            Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
            Assert.Equal(expectedResult, objResult.Value);
            Assert.Equal(expectedResult.Name, ((Pokemon)objResult.Value).Name);
        }
    }
}
