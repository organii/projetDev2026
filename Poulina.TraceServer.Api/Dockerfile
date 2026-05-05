# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Expose port (Render uses PORT env anyway)
EXPOSE 8080

# Start app
ENTRYPOINT ["dotnet", "Poulina.TraceServer.Api.dll"]
