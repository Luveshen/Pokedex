using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.Services.DataTransferObjects;
using Pokedex.Shared.DataTransferObjects;

namespace Pokedex.Services
{
    internal class PokemonService : IPokemonService
    {
        private readonly ILogger<IPokemonService> _logger;
        private readonly IPokemonRetrievalService _pokemonRetrievalService;
        private readonly ITranslationService _translationService;

        public PokemonService(ILogger<IPokemonService> logger,
            IPokemonRetrievalService pokemonRetrievalService,
            ITranslationService translationService)
        {
            _logger = logger;
            _pokemonRetrievalService = pokemonRetrievalService;
            _translationService = translationService;
        }

        public async Task<PokemonResponse> GetPokemonAsync(string pokemonName)
        {
            try
            {
                return await GetPokemonResponseAsync(pokemonName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured getting pokemon {0}.", pokemonName);
                throw;
            }
        }

        public async Task<PokemonResponse> GetPokemonWithTranslatedDescriptionAsync(string pokemonName)
        {
            try
            {
                var pokemon = await GetPokemonResponseAsync(pokemonName);
                var translatedDescription = await GetTranslatedDescription(pokemon);

                if (!string.IsNullOrEmpty(translatedDescription))
                {
                    pokemon.Description = translatedDescription;
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve translation for {0}, using original description.", pokemonName);
                }

                return pokemon;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured getting pokemon {0} with translated description.", pokemonName);
                throw;
            }
        }

        private async Task<PokemonResponse> GetPokemonResponseAsync(string pokemonName)
        {
            pokemonName = pokemonName.ToLowerInvariant(); // pokemon names must be lower case 
            var pokemonGetResponse = await _pokemonRetrievalService.RetrievePokemonInfoAsync(pokemonName);
            if (pokemonGetResponse is null)
            {
                return null;
            }

            return MapToResponse(pokemonGetResponse);
        }

        private async Task<string> GetTranslatedDescription(PokemonResponse pokemon)
        {
            try
            {
                var translationOption = TranslationChooser.GetTranslationOption(pokemon);
                var translationResult = await _translationService.TranslateTextAsync(translationOption, pokemon.Description);

                return translationResult.Success.Total <= 0 ? null : translationResult.Contents.Translated;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error occured in the translation process.");
                return null;
            }
        }

        private PokemonResponse MapToResponse(PokemonGetResponse getResponse)
        {
            var rawDescription = getResponse.FlavorTextEntries
                ?.FirstOrDefault(x => x.Language?.Name?.ToLower() == SupportedLanguages.English)?.FlavorText;

            var description = HandleEscapeSequences(rawDescription);
            return new PokemonResponse
            {
                Name = getResponse.Name,
                Habitat = getResponse.Habitat?.Name,
                IsLegendary = getResponse.IsLegendary,
                Description = description
            };
        }

        private string HandleEscapeSequences(string text)
        {
            if (text is null)
            {
                return null;
            }
            return Regex.Replace(text, @"\r|\n|\t|\f", " ");
        }

        private class SupportedLanguages
        {
            public const string English = "en";
        }
    }
}