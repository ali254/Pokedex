using AutoMapper;
using Pokedex.Core.AutoMapper;
using System.Text;
using Xunit;

namespace Pokedex.Core.Tests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Mapping_CorrectlyMapsProperties()
        {
            var entity = new Entity.Pokemon()
            {
                Id = 1,
                Name = "raichu",
                Habitat = new Entity.NamedEntity() { Name = "grass" },
                Descriptions = new Entity.PokemonDescription[] {
                    new Entity.PokemonDescription() { FlavorText = "asdasdqweqweqwe", Language = new Entity.NamedEntity() { Name = "fr" } },
                    new Entity.PokemonDescription() { FlavorText = "lightning\npowers\n", Language = new Entity.NamedEntity() { Name = "en" } },
                }
            };
            var dto = new DTO.Pokemon()
            {
                Name = "raichu",
                Habitat = "grass",
                Description = "lightning powers "
            };

            var result = _mapper.Map<DTO.Pokemon>(entity);

            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Habitat, result.Habitat);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.IsLegendary, result.IsLegendary);
        }

        [Fact]
        public void Mapping_CorrectlyMapsProperties_WhenNoEnglishTranslation()
        {
            var entity = new Entity.Pokemon()
            {
                Id = 1,
                Name = "raichu",
                Habitat = new Entity.NamedEntity() { Name = "grass" },
                Descriptions = new Entity.PokemonDescription[] {
                    new Entity.PokemonDescription() { FlavorText = "asdasdqweqweqwe", Language = new Entity.NamedEntity() { Name = "fr" } },
                }
            };
            var dto = new DTO.Pokemon()
            {
                Name = "raichu",
                Habitat = "grass",
            };

            var result = _mapper.Map<DTO.Pokemon>(entity);

            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Habitat, result.Habitat);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.IsLegendary, result.IsLegendary);
        }
    }
}
