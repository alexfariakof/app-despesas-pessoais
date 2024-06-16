#!/bin/bash

# Diretórios base
projectTestPath=$(pwd)
baseDirectory=$(realpath ..)
projectAngular=$(realpath "$baseDirectory/AngularApp")
sourceDirs="$baseDirectory/Despesas.Business:$baseDirectory/Despesas.Domain:$baseDirectory/Despesas.Repository:$baseDirectory/Despesas.WebApi"
filefilters="$baseDirectory/Despesas.DataSeeders/**;- $baseDirectory/Migrations.MySqlServer/**;- $baseDirectory/Migrations.MsSqlServer/**;- $baseDirectory/Despesas.CrossCutting/**;- $baseDirectory/Despesas.Business/HyperMedia/**"
reportPath="$projectTestPath/TestResults"
coveragePath="$reportPath/coveragereport"
coverageAngularPath="$projectAngular/coverage"

# Gera o Relatório de Cobertura do Backend
dotnet test ./XUnit.Tests.csproj --results-directory "$reportPath" -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore > /dev/null 2>&1
reportgenerator -reports:$projectTestPath/coverage.cobertura.xml -targetdir:$coveragePath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters

# Encontra o diretório mais recente na pasta TestResults
latestDir=$(ls -td $reportPath/*/ | grep -v 'coveragereport' | head -n 1)

# Verifica se encontrou um diretório e, em caso afirmativo, obtém o nome do diretório (GUID)
if [ -n "$latestDir" ]; then
    guid=$(basename $latestDir)
    coverageXmlPath="$projectTestPath/TestResults/$guid"
    cp -r "$coverageXmlPath/"* "$reportPath/"
else
    echo "Nenhum diretório de resultados encontrado."
fi

# Verifica se existe a pasta node_modules, e se não existir executa npm install
if [ ! -d "$projectAngular/node_modules" ]; then
    (cd $projectAngular && npm install)
fi

# Executa Testes Unitários e gera o relatório de cobertura do Frontend
(cd $projectAngular && npm run test:coverage)
