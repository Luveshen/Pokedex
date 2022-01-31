using System.Threading.Tasks;
using Pokedex.Services.DataTransferObjects;

namespace Pokedex.Services
{
    public interface IPokemonRetrievalService
    {
        Task<PokemonGetResponse> RetrievePokemonInfoAsync(string pokemonName);
    }
}