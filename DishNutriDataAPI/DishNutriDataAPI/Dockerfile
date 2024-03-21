FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

EXPOSE 5263

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DishNutriDataAPI.csproj", "."]
RUN dotnet restore "./DishNutriDataAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DishNutriDataAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyApi.csproj" -c Release -o /app/publish