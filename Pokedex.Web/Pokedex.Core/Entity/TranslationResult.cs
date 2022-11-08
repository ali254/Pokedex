using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Core.Entity
{
    public class TranslationResult
    {
        public TranslationContent Contents { get; set; }


        [JsonIgnore]
        public bool Success { get; set; }
    }
}
