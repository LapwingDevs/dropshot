FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src

COPY ["./DropShot.API", "./src/DropShot.API"]
COPY ["./DropShot.Application", "./src/DropShot.Application"]
COPY ["./DropShot.Infrastructure", "./src/DropShot.Infrastructure"]
COPY ["./DropShot.Domain", "./src/DropShot.Domain"]

WORKDIR /src
RUN dotnet build "src/DropShot.API/DropShot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/DropShot.API/DropShot.API.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DropShot.API.dll"]