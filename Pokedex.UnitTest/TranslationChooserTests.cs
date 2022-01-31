using Pokedex.Services;
using Pokedex.Shared.DataTransferObjects;
using Xunit;

namespace PokeDexUnitTests
{
    public class TranslationChooserTests
    {
        [Fact]
        public void CaveHabitatCheckTest()
        {
            var cavePokemon = CreatePokemonProtoType();
            cavePokemon.Habitat = "cave";

            var caveResult = TranslationChooser.GetTranslationOption(cavePokemon);
            Assert.Equal(TranslationOption.YODA, caveResult);

            var waterPokemon = CreatePokemonProtoType();
            waterPokemon.Habitat = "water";

            var waterResult = TranslationChooser.GetTranslationOption(waterPokemon);
            Assert.Equal(TranslationOption.SHAKESPEARE, waterResult);
        }

        [Fact]
        public void IsLegendaryCheckTest()
        {
            var legendaryPokemon = CreatePokemonProtoType();
            legendaryPokemon.IsLegendary = true;

            var legendaryResult = TranslationChooser.GetTranslationOption(legendaryPokemon);
            Assert.Equal(TranslationOption.YODA, legendaryResult);

            var normalPokemon = CreatePokemonProtoType();
            normalPokemon.IsLegendary = false;

            var normalResult = TranslationChooser.GetTranslationOption(normalPokemon);
            Assert.Equal(TranslationOption.SHAKESPEARE, normalResult);
        }

        private static PokemonResponse CreatePokemonProtoType()
        {
            return new PokemonResponse
            {
                Description = "Description",
                Habitat = "sand",
                IsLegendary = false,
                Name = "Pikachu"
            };
        }
    }
}