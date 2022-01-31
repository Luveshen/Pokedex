using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokedex.Services.DataTransferObjects;

namespace Pokedex.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ITranslationService> _logger;

        public TranslationService(HttpClient httpClient, ILogger<ITranslationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<TranslationResponse> TranslateTextAsync(TranslationOption translationOption,
            string originalText)
        {
            try
            {
                var translator = translationOption == TranslationOption.SHAKESPEARE ? "shakespeare" : "yoda";
                var translationUrl = $"{translator}.json?text={originalText}";

                var response = await _httpClient.GetAsync(translationUrl);
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TranslationResponse>(content);
                }

                _logger.LogError(
                    $"Failed to translate description using option {nameof(translator)}. Error Message: {content}.");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error occured calling the translation service.");
                return null;
            }
        }
    }
}