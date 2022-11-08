using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Entity
{
    public class Pokemon : NamedEntity
    {
        public NamedEntity Habitat { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<PokemonDescription> Descriptions { get; set; }
    }
}
