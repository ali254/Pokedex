using Pokedex.Data;
using System;
using System.Threading.Tasks;
using System.Threading;
using Entity = Pokedex.Core.Entity;
using DTO = Pokedex.Core.DTO;
using AutoMapper;
using Pokedex.Core.Enums;

namespace Pokedex.Service
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokeApiConnection _pokeApiConnection;
        private readonly IFunTranslationConnection _funTranslationConnection;
        private readonly IMapper _mapper;

        public PokemonService(IPokeApiConnection pokeApiConnection, IFunTranslationConnection funTranslationConnection, IMapper mapper)
        {
            _pokeApiConnection = pokeApiConnection;
            _funTranslationConnection = funTranslationConnection;
            _mapper = mapper;
        }

        public async Task<DTO.Pokemon> GetAsync(string pokemonName, CancellationToken cancellationToken)
        {
            var result = await _pokeApiConnection.GetAsync(pokemonName, cancellationToken);

            return _mapper.Map<DTO.Pokemon>(result);
        }  
        
        public async Task<DTO.Pokemon> GetTranslatedAsync(string pokemonName, CancellationToken cancellationToken)
        {
            var result = await GetAsync(pokemonName, cancellationToken);

            var translationType = GetTranslationType(result);
            var translationResult = await _funTranslationConnection.GetTranslationAsync(result.Description, translationType, cancellationToken);

            if (translationResult.Success)
                result.Description = translationResult.Contents.Translated;

            return result;
        }

        private static FunTranslationEnum GetTranslationType(DTO.Pokemon pokemon)
        {
            if (pokemon.Habitat?.Equals("cave", StringComparison.OrdinalIgnoreCase) == true || pokemon.IsLegendary)
                return FunTranslationEnum.Yoda;
            else
                return FunTranslationEnum.Shakespeare;
        }
    }
}
