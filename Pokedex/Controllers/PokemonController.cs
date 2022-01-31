using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Services;
using Pokedex.Shared.DataTransferObjects;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        [HttpGet("{pokemonName}")]
        [ProducesResponseType(typeof(PokemonResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(UserErrorMessage), 400)]
        [ProducesResponseType(typeof(UserErrorMessage), 500)]
        public async Task<IActionResult> GetSpecificPokemonAsync(string pokemonName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pokemonName))
                {
                    _logger.LogWarning("Invalid pokemon name specified in request.");
                    return BadRequest("Please specify a valid pokemon name.");
                }

                var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);
                if (pokemon is null)
                {
                    return NotFound();
                }

                return Ok(pokemon);
            }
            catch (PokemonRetrievalException e)
            {
                var errorDetail = new UserErrorMessage
                {
                    ErrorMessage = $"Failed to retrieve pokemon {pokemonName}.",
                    ErrorDetail = e.Message
                };
                return BadRequest(errorDetail);
            }
            catch (Exception e)
            {
                var logMessage = $"Unexpected error encountered fetching {pokemonName}.";
                _logger.LogError(e, logMessage);
                var errorDetail = new UserErrorMessage
                {
                    ErrorMessage = logMessage,
                    ErrorDetail = e.Message
                };
                var result = StatusCode(StatusCodes.Status500InternalServerError, errorDetail);
                return result;
            }
        }


        [HttpGet("translated/{pokemonName}")]
        [ProducesResponseType(typeof(PokemonResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(UserErrorMessage), 400)]
        [ProducesResponseType(typeof(UserErrorMessage), 500)]
        public async Task<IActionResult> GetSpecificPokemonWithTranslatedDescriptionAsync(string pokemonName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pokemonName))
                {
                    _logger.LogWarning("Invalid pokemon name specified in request.");
                    return BadRequest("Please specify a valid pokemon name.");
                }

                var pokemon = await _pokemonService.GetPokemonWithTranslatedDescriptionAsync(pokemonName);
                if (pokemon is null)
                {
                    return NotFound();
                }

                return Ok(pokemon);
            }
            catch (PokemonRetrievalException e)
            {
                var errorDetail = new UserErrorMessage
                {
                    ErrorMessage = $"Failed to retrieve pokemon {pokemonName}.",
                    ErrorDetail = e.Message
                };
                return BadRequest(errorDetail);
            }
            catch (Exception e)
            {
                var logMessage = $"Unexpected error encountered fetching {pokemonName} with translated description.";
                _logger.LogError(e, logMessage);
                var errorDetail = new UserErrorMessage
                {
                    ErrorMessage = logMessage,
                    ErrorDetail = e.Message
                };
                var result = StatusCode(StatusCodes.Status500InternalServerError, errorDetail);
                return result;
            }
        }
    }
}