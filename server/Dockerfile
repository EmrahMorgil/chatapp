# server i 5000:80 portunda başlat.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server.WebApi/Server.WebApi.csproj", "Server.WebApi/"]
COPY ["Server.Application/Server.Application.csproj", "Server.Application/"]
COPY ["Server.Domain/Server.Domain.csproj", "Server.Domain/"]
COPY ["Server.Persistence/Server.Persistence.csproj", "Server.Persistence/"]
RUN dotnet restore "Server.WebApi/Server.WebApi.csproj"
COPY . .
WORKDIR "/src/Server.WebApi"
RUN dotnet build "Server.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.WebApi.dll"]