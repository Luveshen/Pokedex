using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokedex.Services.DataTransferObjects;

namespace Pokedex.Services
{
    internal class PokemonRetrievalService : IPokemonRetrievalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IPokemonRetrievalService> _logger;

        public PokemonRetrievalService(HttpClient httpClient, ILogger<IPokemonRetrievalService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        
        public async Task<PokemonGetResponse> RetrievePokemonInfoAsync(string pokemonName)
        {
            var apiUrl = $"pokemon-species/{pokemonName}";

            var response = await _httpClient.GetAsync(apiUrl);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PokemonGetResponse>(content);
            }
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Pokemon {0} could not be found.", pokemonName);
                return null;
            }
                
            _logger.LogError("An unexpected error occured fetching pokemon {0}. Http code status {1}.", pokemonName, (int)response.StatusCode);
            throw new PokemonRetrievalException(content);
        }
    }
}