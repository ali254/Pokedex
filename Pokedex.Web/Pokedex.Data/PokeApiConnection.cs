
using Newtonsoft.Json;
using Pokedex.Core.Entity;
using Pokedex.Core.Exceptions;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public class PokeApiConnection : BaseApiConnection, IPokeApiConnection
    {
        public PokeApiConnection(HttpClient client) : base(client)
        {
        }

        public async Task<Pokemon> GetAsync(string pokemonName, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, pokemonName);
            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new ApiException("PokeApi Error: " + response.ReasonPhrase) { StatusCode = (int)response.StatusCode };

            return JsonConvert.DeserializeObject<Pokemon>(await response.Content.ReadAsStringAsync());
        }
    }
}
