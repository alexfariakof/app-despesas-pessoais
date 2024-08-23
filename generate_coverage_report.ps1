cls 

# Pasta onde o relatário será gerado
$baseDirectory = Get-Location
$projectTestPath = Join-Path -Path (Get-Location) -ChildPath "XunitTests"
$sourceDirs = "$baseDirectory\Despesas.Business;$baseDirectory\Despesas.Domain;$baseDirectory\Despesas.Repository;$baseDirectory\Despesas.WebApi;"
$filefilters = "$baseDirectory\Despesas.DataSeeders\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Despesas.CrossCutting\**;-$baseDirectory\Despesas.Business\HyperMedia\**"
$reportPath = Join-Path -Path (Get-Location) -ChildPath "XunitTests\TestResults"
$coverageXmlPath = Join-Path -Path $reportPath -ChildPath "coveragereport"

# Função para matar processos com base no nome do processo
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

# Função para Excluir todo o conteúdo da pasta TestResults, se existir
function Remove-TestPath-Results {
    if (Test-Path $reportPath) {
        Remove-Item -Recurse -Force $reportPath
    }
}

function Remove-TestResults {    
    if (Test-Path $reportPath) {
        Remove-Item -Recurse -Force $reportPath
    }
}

 function Wait-TestResults {
    $REPEAT_WHILE = 0
    while (-not (Test-Path $reportPath)) {
        echo "Agaurdando TestResults..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }

    $REPEAT_WHILE = 0
    while (-not (Test-Path $coverageXmlPath)) {
        echo "Agaurdando Coverage Report..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }   

 } 

# Encerra qualquer processo em segundo plano relacionado ao comando npm run test:watch
Stop-ProcessesByName


# Pasta onde o relatório será gerado
$reportPath = ".\XunitTests\TestResults"

# Exclui todo o conteúdo da pasta TestResults, se existir
Remove-TestPath-Results

# Executa o teste e coleta o GUID gerado
dotnet clean > $null 2>&1
dotnet test ./XunitTests/XUnit.Tests.csproj --configuration Test --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
Wait-TestResults 
Invoke-Item $coverageXmlPath\index.html
