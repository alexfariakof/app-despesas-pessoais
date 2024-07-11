#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM alexfariakof/dotnet-sdk-8.0-with-node AS build 
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY . .

WORKDIR /src/Despesas.WebApi
RUN dotnet publish "./Despesas.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Despesas.WebApi.dll"]