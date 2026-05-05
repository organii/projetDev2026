# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy full solution
COPY . .

# Restore using solution
RUN dotnet restore AgileAI.sln

# Publish API project
RUN dotnet publish Poulina.TraceServer.Api/Poulina.TraceServer.Api.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Poulina.TraceServer.Api.dll"]
