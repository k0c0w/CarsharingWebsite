FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

COPY ["Analytics.Microservice/", "/src/Analytics.Microservice/"]
COPY ["SharedLibs/Carsharing.Contracts/", "/src/SharedLibs/Carsharing.Contracts/"]
WORKDIR /src/Analytics.Microservice
RUN dotnet restore "./Api/Analytics.Microservice.csproj"
COPY . .
WORKDIR /src/Analytics.Microservice/Api
RUN dotnet build "./Analytics.Microservice.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Analytics.Microservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Analytics.Microservice.dll"]