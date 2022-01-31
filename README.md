# TrueLayer Assignment: Pokedex
Basic Pokemon REST API that returns Pokemon information

## Third Parties

The following public API's are called by this project:

- https://pokeapi.co/api/v2
- https://funtranslations.com/api/shakespeare
- https://funtranslations.com/api/yoda

## Prerequisites
- .NET 5 https://dotnet.microsoft.com/download/dotnet/5.0

## Running Pokedex 
1) Download and install .NET 5.0
2) Clone repo from GitHub
3) Navigate to project folder (..\Pokedex\Pokedex)
4) Open preferred command prompt
5) Run command ```dotnet run ```

Once the webapp is running, the API is exposed on the following two ports:
- http://localhost:5000
- https://localhost:5001

## Running Unit Tests (Pokedex.UnitTest)
1) Download and install .NET 5.0
2) Clone repo from GitHub
3) Navigate to project folder (..\Pokedex\Pokedex)
4) Open preferred command prompt
5) Run command ```dotnet test ```

## Docker file
A simple docker file is provided for running a containarized version of the API (.\Pokedex\Pokedex\Dockerfile). 

## Future Suggestions / Production improvements / TODO
- Generate an OpenAPI page for easy API interaction
- Add caching for calling the third party APIs - most of the data (both pokemon info and translated descriptions) are static, this is well suited for caching. Something like Redis could be used as a suitable cache.
- Add more info/debug log statements for easier debugging
- Add advanced telemetry - by linking the logger to something like AppInsights, useful metrics and analytics can be captured and can help in spotting performance issues and bugs faster.
- Add more unit tests for higher code coverage
- Introduce API versioning - allows for new "breaking changes" to be introduced without causing issues for existing clients and allowing them to switch over at their own rate.
- Commented code