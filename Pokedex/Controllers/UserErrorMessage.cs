using Newtonsoft.Json;

namespace Pokedex.Controllers
{
    class UserErrorMessage
    {
        [JsonProperty("error_message")] public string ErrorMessage { get; set; }

        [JsonProperty("error_detail")] public string ErrorDetail { get; set; }
    }
}