using Pokedex.Core.Entity;
using Pokedex.Core.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public interface IFunTranslationConnection
    {
        public Task<TranslationResult> GetTranslationAsync(string textToTranslate, FunTranslationEnum translationEnum, CancellationToken cancellationToken);
    }
}
