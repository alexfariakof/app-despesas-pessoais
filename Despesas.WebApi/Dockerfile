#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["despesas-backend-api-net-core/despesas-backend-api-net-core.csproj", "despesas-backend-api-net-core/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["DataSeeders/DataSeeders.csproj", "DataSeeders/"]
RUN dotnet restore "./despesas-backend-api-net-core/./despesas-backend-api-net-core.csproj"
COPY . .
WORKDIR "/src/despesas-backend-api-net-core"
RUN dotnet build "./despesas-backend-api-net-core.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./despesas-backend-api-net-core.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "despesas-backend-api-net-core.dll"]


RUN dotnet tool install dotnet-reportgenerator-globaltool --tool-path /tools
RUN dotnet build "./despesas-backend-api-net-core/./despesas-backend-api-net-core.csproj" --restore
RUN dotnet test "./XunitTests/XUnit.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" 
RUN /tools/reportgenerator -reports:**/coverage.cobertura.xml -targetdir:/app/test -reporttypes:Html -filefilters:-$filefilters
