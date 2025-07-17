# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the csproj and restore dependencies
COPY GameStore.Api/*.csproj ./GameStore.Api/
RUN dotnet restore GameStore.Api/GameStore.Api.csproj

# Copy all source files
COPY . .

# Publish the app
RUN dotnet publish GameStore.Api/GameStore.Api.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
