using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.DTO;
using Pokedex.Data;
using Pokedex.Service;
using Pokedex.Web.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [NameValidationAttribute]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// Gets the pokemon for given name
        /// </summary>
        /// <param name="name">Pokemon name to query</param>
        /// <param name="cancellationToken">token for cancellation of request</param>
        /// <returns>pokemon matching the name</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pokemon>> Get([FromRoute]string name, CancellationToken cancellationToken)
        {
            return Ok(await _pokemonService.GetAsync(name, cancellationToken));
        }

        /// <summary>
        /// Gets the pokemon for given name with a specially translated description
        /// </summary>
        /// <param name="name">Pokemon name to query</param>
        /// <param name="cancellationToken">token for cancellation of request</param>
        /// <returns>pokemon matching the name with special description</returns>
        [HttpGet("translated/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pokemon>> GetTranslated([FromRoute]string name, CancellationToken cancellationToken)
        {
            return Ok(await _pokemonService.GetTranslatedAsync(name, cancellationToken));
        }
    }
}
