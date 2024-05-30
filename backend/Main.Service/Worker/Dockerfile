FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
RUN dotnet tool install --global dotnet-ef --version 7.0.12

COPY ["Main.Service/Core", "Main.Service/Core"]
COPY ["Main.Service/Infrastructure", "Main.Service/Infrastructure"]
COPY ["Main.Service/ChatConsumers", "Main.Service/ChatConsumers"]

COPY ["SharedLibs/", "SharedLibs/"]
COPY ["Balance.Microservice/protos/", "Balance.Microservice/protos/"]

WORKDIR "/src/Main.Service/Worker"

RUN dotnet restore "Main.Service.Worker.csproj"
RUN dotnet build "Main.Service.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Main.Service.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Main.Service.Worker.dll"]


