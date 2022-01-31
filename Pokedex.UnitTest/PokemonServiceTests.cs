using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Pokedex;
using Pokedex.Services;
using Pokedex.Services.DataTransferObjects;
using Xunit;

namespace PokeDexUnitTests
{
    public class PokemonServiceTests
    {
        [Fact]
        public async Task RetrievePokemonGetCall_ReturnsValidResponse()
        {
            // Arrange
            var pokemonSvcLogger = new Logger<IPokemonService>(NullLoggerFactory.Instance);
            
            var mockRetrievalService = new Mock<IPokemonRetrievalService>();
            var mockTranslationService = new Mock<ITranslationService>();
            var pokemonService = new PokemonService(pokemonSvcLogger, mockRetrievalService.Object, mockTranslationService.Object);

            var habitatName = "fire";
            var description = "Breathes fire Raaawr.";
            var charmanderName = "Charmander";
            var isLegendary = true;
            
            mockRetrievalService.Setup(svc => svc.RetrievePokemonInfoAsync(charmanderName)).ReturnsAsync(
                new PokemonGetResponse
                {
                    Id = 0,
                    Habitat = new Habitat { Name = habitatName },
                    FlavorTextEntries = new List<FlavorTextEntry>
                    {
                        new()
                        {
                            FlavorText = description,
                            Language = new Language { Name = "en" }
                        }
                    },
                    IsLegendary = isLegendary,
                    Name = "Charmander"
                });

            // Act
            var charmander = await pokemonService.GetPokemonAsync("Charmander");

            // Assert
            mockRetrievalService.Verify(service => service.RetrievePokemonInfoAsync(charmanderName), Times.Once);
            mockTranslationService.Verify(service => service.TranslateTextAsync(It.IsAny<TranslationOption>(), It.IsAny<string>()), Times.Never);
            
            Assert.Equal(charmanderName, charmander.Name);
            Assert.Equal(habitatName, charmander.Habitat);
            Assert.Equal(isLegendary, charmander.IsLegendary);
            Assert.Equal(description, charmander.Description);
        }
        
        [Fact]
        public async Task RetrievePokemonGetCall_ReturnsValidResponse1()
        {
            // Arrange
            var pokemonSvcLogger = new Logger<IPokemonService>(NullLoggerFactory.Instance);
            
            var mockRetrievalService = new Mock<IPokemonRetrievalService>();
            var mockTranslationService = new Mock<ITranslationService>();
            var pokemonService = new PokemonService(pokemonSvcLogger, mockRetrievalService.Object, mockTranslationService.Object);

            var habitatName = "fire";
            var description = "Breathes fire Raaawr.";
            var charmanderName = "Charmander";
            var isLegendary = true;
            
            mockRetrievalService.Setup(svc => svc.RetrievePokemonInfoAsync(charmanderName)).ReturnsAsync(
                new PokemonGetResponse
                {
                    Id = 0,
                    Habitat = new Habitat { Name = habitatName },
                    FlavorTextEntries = new List<FlavorTextEntry>
                    {
                        new()
                        {
                            FlavorText = description,
                            Language = new Language { Name = "en" }
                        }
                    },
                    IsLegendary = isLegendary,
                    Name = "Charmander"
                });

            var translatedDescription = "This is a translated description.";
            mockTranslationService.Setup(svc => svc.TranslateTextAsync(TranslationOption.YODA, description))
                .ReturnsAsync(
                    new TranslationResponse
                    {
                        Success = new Success { Total = 1 },
                        Contents = new Content
                        {
                            Translated = translatedDescription
                        }
                    });

            // Act
            var charmander = await pokemonService.GetPokemonWithTranslatedDescriptionAsync("Charmander");

            // Assert
            mockRetrievalService.Verify(service => service.RetrievePokemonInfoAsync(charmanderName), Times.Once);
            mockTranslationService.Verify(service => service.TranslateTextAsync(TranslationOption.YODA, description), Times.Once);
            
            Assert.Equal(charmanderName, charmander.Name);
            Assert.Equal(habitatName, charmander.Habitat);
            Assert.Equal(isLegendary, charmander.IsLegendary);
            Assert.Equal(translatedDescription, charmander.Description);
        }
    }
}