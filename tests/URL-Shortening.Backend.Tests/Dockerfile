# Written by DeepSeek
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file if it exists
COPY ["URL-Shortening.sln", "."]
# Copy project files with their relative paths
COPY ["backend/URL-Shortening.Backend/URL-Shortening.Backend.csproj", "backend/URL-Shortening.Backend/"]
COPY ["tests/URL-Shortening.Backend.Tests/URL-Shortening.Backend.Tests.csproj", "tests/URL-Shortening.Backend.Tests/"]

# Restore using solution file (if available) or individual projects
RUN dotnet restore "URL-Shortening.sln" || \
    (dotnet restore "backend/URL-Shortening.Backend/URL-Shortening.Backend.csproj" && \
     dotnet restore "tests/URL-Shortening.Backend.Tests/URL-Shortening.Backend.Tests.csproj")

# Copy everything
COPY . .

# Build and run tests
WORKDIR "/src/tests/URL-Shortening.Backend.Tests"
RUN dotnet build "URL-Shortening.Backend.Tests.csproj" -c Release --no-restore
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory", "/test-results"]