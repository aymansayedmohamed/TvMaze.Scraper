#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/TvMaze.Scraper/TvMaze.Scraper.csproj", "src/TvMaze.Scraper/"]
RUN dotnet restore "src/TvMaze.Scraper/TvMaze.Scraper.csproj"
COPY . .
WORKDIR "/src/src/TvMaze.Scraper"
RUN dotnet build "TvMaze.Scraper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TvMaze.Scraper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TvMaze.Scraper.dll"]