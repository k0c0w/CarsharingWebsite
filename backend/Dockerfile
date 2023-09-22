FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN dotnet tool install --global dotnet-ef
COPY ["Carsharing/Carsharing.csproj", "Carsharing/"]
COPY ["Core/Contracts/Contracts.csproj", "Core/Contracts/"]
COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Core/Services.Abstractions/Services.Abstractions.csproj", "Core/Services.Abstractions/"]
COPY ["Core/Services/Services.csproj", "Core/Services/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
RUN dotnet restore "Carsharing/Carsharing.csproj"
COPY . .
WORKDIR "/src/Carsharing"
RUN dotnet build "Carsharing.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/build

FROM build AS publish
RUN dotnet publish "Carsharing.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carsharing.dll"]


