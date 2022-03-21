#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Security/WebApp.Identity/WebApp.Identity.csproj", "Security/WebApp.Identity/"]
COPY ["Security/WebApp.Identity.Persistence/WebApp.Identity.Persistence.csproj", "Security/WebApp.Identity.Persistence/"]
RUN dotnet restore "Security/WebApp.Identity/WebApp.Identity.csproj"
COPY . .
WORKDIR "/src/Security/WebApp.Identity"
RUN dotnet build "WebApp.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Identity.dll"]