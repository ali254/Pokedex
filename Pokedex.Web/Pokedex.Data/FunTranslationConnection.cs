using Newtonsoft.Json;
using Pokedex.Core.Entity;
using Pokedex.Core.Enums;
using Pokedex.Data.Caching;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public class FunTranslationConnection : BaseApiConnection, IFunTranslationConnection
    {
        public FunTranslationConnection(HttpClient client, ICachingService cachingService) : base(client, cachingService)
        {
        }

        public async Task<TranslationResult> GetTranslationAsync(string textToTranslate, FunTranslationEnum translationEnum, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, translationEnum.ToString())
            { Content = JsonContent.Create(new { Text = textToTranslate }) };

            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new TranslationResult() { Success = false };

            var jsonResult = await response.Content.ReadAsStringAsync();

            var translationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonResult);
            translationResult.Success = true;

            return translationResult;
        }
    }
}
