using Pokedex.Core.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Service
{
    public interface IPokemonService
    {
        public Task<Pokemon> GetAsync(string pokemonName, CancellationToken cancellationToken);

        public Task<Pokemon> GetTranslatedAsync(string pokemonName, CancellationToken cancellationToken);
    }
}
