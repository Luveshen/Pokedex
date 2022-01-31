using System;

namespace Pokedex.Services
{
    internal class PokemonRetrievalException : Exception
    {
        public PokemonRetrievalException(string message) : base(message)
        {
        }
    }
}