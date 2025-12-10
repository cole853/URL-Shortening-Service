# Written by DeepSeek
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/URL-Shortening.Backend/URL-Shortening.Backend.csproj", "backend/URL-Shortening.Backend/"]
COPY ["stylecop.json", "./"]
RUN dotnet restore "backend/URL-Shortening.Backend/URL-Shortening.Backend.csproj"
COPY . .

WORKDIR "/src/backend/URL-Shortening.Backend"
RUN dotnet build "URL-Shortening.Backend.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/backend/URL-Shortening.Backend"
RUN dotnet publish "URL-Shortening.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "URL-Shortening.Backend.dll"]