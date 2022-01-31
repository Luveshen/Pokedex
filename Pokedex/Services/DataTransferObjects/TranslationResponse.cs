using Newtonsoft.Json;

namespace Pokedex.Services.DataTransferObjects
{
    public class TranslationResponse
    {
        [JsonProperty("success")] public Success Success { get; set; }

        [JsonProperty("contents")] public Content Contents { get; set; }
    }

    public class Success
    {
        [JsonProperty("total")] public int Total { get; set; }
    }

    public class Content
    {
        [JsonProperty("translated")] public string Translated { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("translation")] public string Translation { get; set; }
    }
}