using Pokedex.Data.Caching;
using System.Net.Http;

namespace Pokedex.Data
{
    public class BaseApiConnection
    {
        protected readonly HttpClient _client;
        protected readonly ICachingService _cachingService;

        public BaseApiConnection(HttpClient client, ICachingService cachingService)
        {
            _client = client;
            _cachingService = cachingService;
        }
    }
}
