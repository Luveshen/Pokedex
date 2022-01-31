using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Pokedex.Controllers;
using Pokedex.Services;
using Pokedex.Shared.DataTransferObjects;
using Xunit;

namespace PokeDexUnitTests
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task RetrievePokemonGetCall_ReturnsValidResponse()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock.Setup(service => service.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(new PokemonResponse
                {
                    Name = "Pikachu",
                    Description = "xxxx",
                    Habitat = "water",
                    IsLegendary = true
                });
            
            var pokemonController = new PokemonController(new Logger<PokemonController>(NullLoggerFactory.Instance), pokemonServiceMock.Object);

            // Act 
            var pokemonResult = await pokemonController.GetSpecificPokemonAsync("Pikachu");

            // Assert
            pokemonServiceMock.Verify(service => service.GetPokemonAsync("Pikachu"), Times.Once());
            Assert.IsType<OkObjectResult>(pokemonResult);
            var okObjectResult = (OkObjectResult)pokemonResult;
            Assert.IsType<PokemonResponse>(okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }
        
        [Fact]
        public async Task RetrievePokemonGetCall_EmptyName_ReturnsBadRequestResponse()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            var pokemonController = new PokemonController(new Logger<PokemonController>(NullLoggerFactory.Instance), pokemonServiceMock.Object);
            
            // Act 
            var pokemonResult = await pokemonController.GetSpecificPokemonAsync("");

            // Assert
            pokemonServiceMock.Verify(service => service.GetPokemonAsync(It.IsAny<string>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(pokemonResult);
            var badRequestResult = (BadRequestObjectResult)pokemonResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
        
        [Fact]
        public async Task RetrievePokemonGetCall_PokemonNotFound_ReturnsNotFoundResponse()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            var pokemonController = new PokemonController(new Logger<PokemonController>(NullLoggerFactory.Instance), pokemonServiceMock.Object);
            pokemonServiceMock.Setup(service => service.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync((PokemonResponse)null);
            
            // Act 
            var pokemonResult = await pokemonController.GetSpecificPokemonAsync("Spongebob SquarePants");

            // Assert
            pokemonServiceMock.Verify(service => service.GetPokemonAsync("Spongebob SquarePants"), Times.Once);
            Assert.IsType<NotFoundResult>(pokemonResult);
            var notFoundResult = (NotFoundResult)pokemonResult;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
        
        [Fact]
        public async Task RetrievePokemonTranslatedGetCall_ReturnsValidResponse()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock.Setup(service => service.GetPokemonWithTranslatedDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(new PokemonResponse
                {
                    Name = "Pikachu",
                    Description = "xxxx",
                    Habitat = "water",
                    IsLegendary = true
                });
            
            var pokemonController = new PokemonController(new Logger<PokemonController>(NullLoggerFactory.Instance), pokemonServiceMock.Object);

            // Act 
            var pokemonResult = await pokemonController.GetSpecificPokemonWithTranslatedDescriptionAsync("Pikachu");

            // Assert
            pokemonServiceMock.Verify(service => service.GetPokemonWithTranslatedDescriptionAsync("Pikachu"), Times.Once);
            Assert.IsType<OkObjectResult>(pokemonResult);
            var okObjectResult = (OkObjectResult)pokemonResult;
            Assert.IsType<PokemonResponse>(okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }

    }
}