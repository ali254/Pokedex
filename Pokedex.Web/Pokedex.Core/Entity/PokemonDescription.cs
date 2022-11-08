using Newtonsoft.Json;

namespace Pokedex.Core.Entity
{
    public class PokemonDescription
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        public NamedEntity Language { get; set; }
    }
}