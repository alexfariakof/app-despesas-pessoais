$baseDirectory = ($PWD)
$projectTestPath = Join-Path -Path ($baseDirectory) -ChildPath "XunitTests"
$projectAngular = (Resolve-Path -Path "$baseDirectory\AngularApp");
$sourceDirs = "$baseDirectory\Despesas.Business;$baseDirectory\Despesas.Domain;$baseDirectory\Despesas.Repository;$baseDirectory\Despesas.WebApi;$baseDirectory\AngularApp;"
$filefilters = "$baseDirectory\Despesas.DataSeeders\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Despesas.CrossCutting\**;-$baseDirectory\Despesas.Business\HyperMedia\**"
$reportPath = Join-Path -Path ($projectTestPath) -ChildPath "TestResults"
$coveragePath = Join-Path -Path $reportPath -ChildPath "coveragereport"
$coverageAngularPath = Join-Path -Path $projectAngular -ChildPath "coverage"

function Wait-TestResults {
    $REPEAT_WHILE = 0
    while (-not (Test-Path $reportPath)) {
        echo "Agaurdando TestResults..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }
 } 

# Gera o Relatório de Cobertura de Código 
dotnet test  $projectTestPath/XUnit.Tests.csproj --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
Wait-TestResults
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml  -targetdir:$coveragePath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters

# Encontra o diretório como os resultados do teste mais recente na pasta TestResults 
$latestDir = Get-ChildItem -Directory -Path $reportPath | Where-Object { $_.Name -ne 'coveragereport' } | Sort-Object LastWriteTime -Descending | Select-Object -First 1

# Verifica se encontrou um diretório e, em caso afirmativo, obtém o nome do diretório (GUID)
if ($latestDir) {
    $guid = $latestDir.Name    
    $coverageXmlPath = "$projectTestPath\TestResults\$guid"
    Copy-Item -Path "$coverageXmlPath\*" -Destination $reportPath -Recurse -Force
} else {
    Write-Output "Nenhum diretório de resultados encontrado."
}


# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
    $watchProcess = Start-Process npm -ArgumentList "install" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
    $watchProcess.WaitForExit()	
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
$watchProcess = Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
$watchProcess.WaitForExit()