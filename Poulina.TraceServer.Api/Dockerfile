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

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 10000
ENTRYPOINT ["dotnet", "AgileAi.Api.dll"]
