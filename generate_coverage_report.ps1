# Função para matar processos com base no nome do processo
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

# Encerra qualquer processo em segundo plano relacionado ao comando npm run test:watch
Stop-ProcessesByName


# Pasta onde o relatório será gerado
$reportPath = ".\despesas-backend-api-net-core.XUnit\TestResults"

# Exclui todo o conteúdo da pasta TestResults, se existir
if (Test-Path $reportPath) {
    Remove-Item -Recurse -Force $reportPath
}

# Executa o teste e coleta o GUID gerado
dotnet clean
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"

# Encontra o diretório mais recente na pasta TestResults
$latestDir = Get-ChildItem -Directory -Path .\despesas-backend-api-net-core.XUnit\TestResults | Sort-Object LastWriteTime -Descending | Select-Object -First 1
$sourceDirs = Join-Path -Path (Get-Location) -ChildPath "despesas-backend-api-net-core"

# Verifica se encontrou um diretório e, em caso afirmativo, obtém o nome do diretório (GUID)
if ($latestDir -ne $null) {
    $guid = $latestDir.Name
  
    # Constrói os caminhos dinamicamente
    $baseDirectory = Join-Path -Path (Get-Location) -ChildPath "despesas-backend-api-net-core.XUnit"
    $coverageXmlPath = Join-Path -Path (Join-Path -Path $baseDirectory -ChildPath "TestResults") -ChildPath $guid

    # Gera o relatório de cobertura usando o GUID capturado
    reportgenerator -reports:$baseDirectory\coverage.cobertura.xml -targetdir:$coverageXmlPath\coveragereport -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$sourceDirs\Database-In-Memory\**
    

    # Abre a página index.html no navegador padrão do sistema operacional
    Invoke-Item $coverageXmlPath\coveragereport\index.html
}
else {
    Write-Host "Nenhum diretório de resultados encontrado."
} 