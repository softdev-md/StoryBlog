#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api/WebApp.Api/WebApp.Api.csproj", "Api/WebApp.Api/"]
COPY ["Api/WebApp.Api.Application/WebApp.Api.Application.csproj", "Api/WebApp.Api.Application/"]
COPY ["Api/WebApp.Api.Domain/WebApp.Api.Domain.csproj", "Api/WebApp.Api.Domain/"]
COPY ["Api/WebApp.Api.Infrastructure/WebApp.Api.Infrastructure.csproj", "Api/WebApp.Api.Infrastructure/"]
COPY ["BuildingBlocks/WebApp.Logging/WebApp.Logging.csproj", "BuildingBlocks/WebApp.Logging/"]
COPY ["Api/WebApp.Api.Persistence/WebApp.Api.Persistence.csproj", "Api/WebApp.Api.Persistence/"]
RUN dotnet restore "Api/WebApp.Api/WebApp.Api.csproj"
COPY . .
WORKDIR "/src/Api/WebApp.Api"
RUN dotnet build "WebApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Api.dll"]