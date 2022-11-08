using AutoMapper;
using Moq;
using Pokedex.Core.Entity;
using Pokedex.Core.Enums;
using Pokedex.Data;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DTO = Pokedex.Core.DTO;
using Entity = Pokedex.Core.Entity;

namespace Pokedex.Service.Tests
{
    public class PokemonServiceTests
    {
        private readonly PokemonService _pokemonService;
        private readonly Mock<IPokeApiConnection> _pokeApiConnection;
        private readonly Mock<IFunTranslationConnection> _funTranslationConnection;
        private readonly Mock<IMapper> _mapper;
        public PokemonServiceTests()
        {
            _pokeApiConnection = new Mock<IPokeApiConnection>();
            _funTranslationConnection = new Mock<IFunTranslationConnection>();
            _mapper = new Mock<IMapper>();

            _pokemonService = new PokemonService(_pokeApiConnection.Object, _funTranslationConnection.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_Calls_GetOnApiConnection()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetAsync("pikachu", ct);

            _pokeApiConnection.Verify(x => x.GetAsync("pikachu", ct), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Calls_Mapper()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetAsync("pikachu", ct);

            _mapper.Verify(x => x.Map<DTO.Pokemon>(entity), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Returns_Expected()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            var result = await _pokemonService.GetAsync("pikachu", ct);

            Assert.IsType<DTO.Pokemon>(result);
            Assert.Equal(dto, result);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task GetTranslation_Calls_GetOnPokeApiConnection()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetTranslatedAsync("pikachu", ct);

            _pokeApiConnection.Verify(x => x.GetAsync("pikachu", ct), Times.Once);
        }

        [Fact]
        public async Task GetTranslation_Calls_GetTranslationApiConnection_WhenPokemonIsLegendary_DoYodaTranslation()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu", Description = "aasdas", IsLegendary = true };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetTranslatedAsync("pikachu", ct);

            _funTranslationConnection.Verify(x => x.GetTranslationAsync(dto.Description, FunTranslationEnum.Yoda, ct), Times.Once);
        }

        [Fact]
        public async Task GetTranslation_Calls_GetTranslationApiConnection_WhenPokemonHasCaveHabitat_DoYodaTranslation()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu", Description = "aasdas", Habitat = "cave" };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetTranslatedAsync("pikachu", ct);

            _funTranslationConnection.Verify(x => x.GetTranslationAsync(dto.Description, FunTranslationEnum.Yoda, ct), Times.Once);
        }

        [Fact]
        public async Task GetTranslation_Calls_GetTranslationApiConnection_WhenPokemonIsNotLegendaryAndNotCaveHabitat_DoShakespeareTranslation()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetTranslatedAsync("pikachu", ct);

            _funTranslationConnection.Verify(x => x.GetTranslationAsync(dto.Description, FunTranslationEnum.Shakespeare, ct), Times.Once);
        }

        [Fact]
        public async Task GetTranslation_Calls_Mapper()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu" };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            await _pokemonService.GetTranslatedAsync("pikachu", ct);

            _mapper.Verify(x => x.Map<DTO.Pokemon>(entity), Times.Once);
        }

        [Fact]
        public async Task GetTranslation_Returns_Result_WhenTranslationSuccessful_WithNewTranslation()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu", Description = "hehe" };
            var translationResult = new TranslationResult() { Success = true, Contents = new TranslationContent() { Translated = "oh yeah" } };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            var result = await _pokemonService.GetTranslatedAsync("pikachu", ct);

            Assert.IsType<DTO.Pokemon>(result);
            Assert.Equal(dto, result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(translationResult.Contents.Translated, result.Description);
        }



        [Fact]
        public async Task GetTranslation_Returns_Result_WhenTranslationUnsuccessful_WithOriginalDescription()
        {
            var ct = new CancellationToken();
            var entity = new Entity.Pokemon() { Id = 1, Name = "pikachu" };
            var dto = new DTO.Pokemon() { Name = "pikachu", Description = "hehe" };
            var translationResult = new TranslationResult() { Success = false };
            _pokeApiConnection.Setup(x => x.GetAsync("pikachu", ct)).Returns(Task.FromResult(entity));
            _funTranslationConnection.Setup(x => x.GetTranslationAsync(It.IsAny<string>(), It.IsAny<FunTranslationEnum>(), ct)).Returns(Task.FromResult(translationResult));
            _mapper.Setup(x => x.Map<DTO.Pokemon>(entity)).Returns(dto);

            var result = await _pokemonService.GetTranslatedAsync("pikachu", ct);

            Assert.IsType<DTO.Pokemon>(result);
            Assert.Equal(dto, result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
        }
    }
}
