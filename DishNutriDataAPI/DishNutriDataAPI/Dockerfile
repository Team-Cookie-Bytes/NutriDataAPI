# Use the official ASP.NET Core SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./

# Expose the port your application listens on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "DishNutriDataAPI.dll"]