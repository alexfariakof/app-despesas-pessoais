#!/bin/bash

# Pasta onde o relatório será gerado
reportPath="./despesas-backend-api-net-core.XUnit/TestResults"

# Exclui todo o conteúdo da pasta TestResults, se existir
if [ -d "$reportPath" ]; then
    rm -r $reportPath/*
fi

# Executa o teste e coleta o GUID gerado
dotnet build -restore
dotnet test ./despesas-backend-api-net-core.XUnit/despesas-backend-api-net-core.XUnit.csproj -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"

# Encontra o diretório mais recente na pasta TestResults
latestDir=$(ls -td ./despesas-backend-api-net-core.XUnit/TestResults/* | head -n 1)

# Verifica se encontrou um diretório e, em caso afirmativo, obtém o nome do diretório (GUID)
if [ -n "$latestDir" ]; then
    guid=$(basename $latestDir)

    baseDirectory=$(pwd)/despesas-backend-api-net-core.XUnit
    coverageXmlPath=$baseDirectory/TestResults/$guid
    sourceDirs=$(pwd)/despesas-backend-api-net-core
    # Gera o relatório de cobertura usando o GUID capturado
    reportgenerator -reports:$baseDirectory/coverage.cobertura.xml -targetdir:$coverageXmlPath/coveragereport -reporttypes:'Html;lcov;' -filefilters:-$sourceDirs/Database-In-Memory/**

    # Abre a página index.html no navegador padrão do sistema operacional (navegador web padrão no Linux)
    start $coverageXmlPath/coveragereport/index.html
else
    echo "Nenhum diretório de resultados encontrado."
fi