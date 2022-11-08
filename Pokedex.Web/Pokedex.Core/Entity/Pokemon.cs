using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.Core.Entity
{
    public class Pokemon : NamedEntity
    {
        public NamedEntity Habitat { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonProperty("flavor_text_entries")]
        public IEnumerable<PokemonDescription> Descriptions { get; set; }
    }
}
