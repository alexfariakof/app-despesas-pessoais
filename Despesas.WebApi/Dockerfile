#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY . .

RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_21.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g npm@latest >/dev/null 2>&1 && \
    npm install -g @angular/core @angular/cli >/dev/null 2>&1

WORKDIR /src/Despesas.WebApi
RUN dotnet publish "./Despesas.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build appsettings.docker.json ./appsettings.json
COPY --from=build appsettings.docker.json ./appsettings.develpment.json
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Despesas.WebApi.dll"]