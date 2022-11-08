
using Newtonsoft.Json;
using Pokedex.Core.Entity;
using Pokedex.Core.Exceptions;
using Pokedex.Data.Caching;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public class PokeApiConnection : BaseApiConnection, IPokeApiConnection
    {
        public PokeApiConnection(HttpClient client, ICachingService cachingService) : base(client, cachingService)
        {
        }

        public async Task<Pokemon> GetAsync(string pokemonName, CancellationToken cancellationToken)
        {
            if (_cachingService.ContainsKey(pokemonName))
            {
                return _cachingService.Get<Pokemon>(pokemonName);
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, pokemonName);
            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new ApiException("PokeApi Error: " + response.ReasonPhrase) { StatusCode = (int)response.StatusCode };

            var resultObj = JsonConvert.DeserializeObject<Pokemon>(await response.Content.ReadAsStringAsync());
            _cachingService.Set<Pokemon>(pokemonName, resultObj, 1);

            return resultObj;
        }
    }
}
