using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Entity
{
    public class PokemonDescription
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        public NamedEntity Language { get; set; }
    }
}