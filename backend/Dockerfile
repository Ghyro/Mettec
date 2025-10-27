# ----------- Build Stage -----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY MettecService.API/*.csproj MettecService.API/
COPY MettecService.Core/*.csproj MettecService.Core/
COPY MettecService.DataAccess/*.csproj ./MettecService.DataAccess/

# Publish the application
RUN dotnet publish ./MettecService.API/MettecService.API.csproj -c Release -o /app/publish

# ----------- Runtime Stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Expose port 9000
EXPOSE 9000

# Bind Kestrel to port 9000
ENV ASPNETCORE_URLS=http://+:9000

# Install curl for debugging and upgrade openssl to fix vulnerability
RUN apt-get update && apt-get install -y curl --only-upgrade openssl

# Copy published app
COPY --from=build /app/publish .

# Start the app
ENTRYPOINT ["dotnet", "MettecService.API.dll"]