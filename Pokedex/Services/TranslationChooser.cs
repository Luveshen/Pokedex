using System;
using Pokedex.Shared.DataTransferObjects;

namespace Pokedex.Services
{
    public static class TranslationChooser
    {
        internal static TranslationOption GetTranslationOption(PokemonResponse pokemonResponse)
        {
            if (string.Equals(pokemonResponse.Habitat, "cave", StringComparison.Ordinal))
            {
                return TranslationOption.YODA;
            }

            if (pokemonResponse.IsLegendary)
            {
                return TranslationOption.YODA;
            }

            return TranslationOption.SHAKESPEARE;
        }
    }
}