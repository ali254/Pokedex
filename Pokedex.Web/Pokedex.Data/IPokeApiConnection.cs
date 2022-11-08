using Pokedex.Core.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public interface IPokeApiConnection
    {
       public Task<Pokemon> GetAsync(string pokemonName, CancellationToken cancellationToken);
    }
}
