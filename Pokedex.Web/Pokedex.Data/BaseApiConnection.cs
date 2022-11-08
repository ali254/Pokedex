using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pokedex.Data
{
    public class BaseApiConnection
    {
        protected readonly HttpClient _client;

        public BaseApiConnection(HttpClient client)
        {
            _client = client;
        }
    }
}
