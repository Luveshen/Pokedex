using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokedex.Services.DataTransferObjects
{
    public class PokemonGetResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("is_legendary")] public bool IsLegendary { get; set; }

        [JsonProperty("flavor_text_entries")] public IList<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonProperty("habitat")] public Habitat Habitat { get; set; }
    }

    public class FlavorTextEntry
    {
        [JsonProperty("flavor_text")] public string FlavorText { get; set; }

        [JsonProperty("language")] public Language Language { get; set; }
    }

    public class Language
    {
        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Habitat
    {
        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}