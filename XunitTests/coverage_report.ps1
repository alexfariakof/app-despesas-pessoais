$projectTestPath = Get-Location
$baseDirectory =  (Resolve-Path -Path ..).Path
$projectAngular = (Resolve-Path -Path "$baseDirectory\AngularApp");
$sourceDirs = "$baseDirectory\Despesas.Business;$baseDirectory\Despesas.Domain;$baseDirectory\Despesas.Repository;$baseDirectory\Despesas.WebApi;"
$filefilters = "$baseDirectory\Despesas.DataSeeders\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Despesas.CrossCutting\**;-$baseDirectory\Despesas.Business\HyperMedia\**"
$reportPath = Join-Path -Path (Get-Location) -ChildPath "TestResults"
$coveragePath = Join-Path -Path $reportPath -ChildPath "coveragereport"
$coverageAngularPath = Join-Path -Path $projectAngular -ChildPath "coverage"

# Gera o Relatório de Cobertura do Backend
dotnet test ./XUnit.Tests.csproj --configuration Staging --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore > $null 2>&1
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml  -targetdir:$coveragePath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters > $null 2>&1

Exit
# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
    $watchProcess = Start-Process npm -ArgumentList "install" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
    $watchProcess.WaitForExit()	
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
$watchProcess = Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow -PassThru
$watchProcess.WaitForExit()	