using Newtonsoft.Json;

namespace Pokedex.Core.Entity
{
    public class TranslationResult
    {
        public TranslationContent Contents { get; set; }


        [JsonIgnore]
        public bool Success { get; set; }
    }
}
