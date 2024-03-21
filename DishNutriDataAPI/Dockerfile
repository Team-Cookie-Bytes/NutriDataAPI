FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR .

COPY . .

RUN dotnet build

WORKDIR ./DishNutriDataAPI

CMD ["dotnet", "run"]