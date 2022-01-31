using System.Threading.Tasks;
using Pokedex.Shared.DataTransferObjects;

namespace Pokedex.Services
{
    public interface IPokemonService
    {
        Task<PokemonResponse> GetPokemonAsync(string pokemonName);

        Task<PokemonResponse> GetPokemonWithTranslatedDescriptionAsync(string pokemonName);
    }
}