#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM ubuntu:22.04 AS builder

# install the .NET 6 SDK from the Ubuntu archive
# (no need to clean the apt cache as this is an unpublished stage)
RUN apt-get update && apt-get install -y dotnet6 ca-certificates

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5256
EXPOSE 5257

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["despesas-backend-api-net-core.csproj", "."]
RUN dotnet restore "./despesas-backend-api-net-core.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "despesas-backend-api-net-core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "despesas-backend-api-net-core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "despesas-backend-api-net-core.dll"]