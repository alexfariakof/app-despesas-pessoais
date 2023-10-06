# Executa o teste e coleta o GUID gerado
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"

# Encontra o diretório mais recente na pasta TestResults
$latestDir = Get-ChildItem -Directory -Path .\despesas-backend-api-net-core.XUnit\TestResults | Sort-Object LastWriteTime -Descending | Select-Object -First 1

# Verifica se encontrou um diretório e, em caso afirmativo, obtém o nome do diretório (GUID)
if ($latestDir -ne $null) {
    $guid = $latestDir.Name
  
    # Constrói os caminhos dinamicamente
    $baseDirectory = Join-Path -Path (Get-Location) -ChildPath "despesas-backend-api-net-core.XUnit"
    $coverageXmlPath = Join-Path -Path (Join-Path -Path $baseDirectory -ChildPath "TestResults") -ChildPath $guid

    # Gera o relatório de cobertura usando o GUID capturado
    reportgenerator -reports:$baseDirectory\coverage.cobertura.xml -targetdir:$coverageXmlPath\coveragereport -reporttypes:"Html;lcov;"
}
else {
    Write-Host "Nenhum diretório de resultados encontrado."
} 