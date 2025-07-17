# Use the official .NET SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/out ./

# Required by Render to expose the port
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
