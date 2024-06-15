$projectTestPath = Get-Location
$baseDirectory =  (Resolve-Path -Path ..).Path
$projectAngular = (Resolve-Path -Path "$baseDirectory\AngularApp");
$sourceDirs = "$baseDirectory\Despesas.Business;$baseDirectory\Despesas.Domain;$baseDirectory\Despesas.Repository;$baseDirectory\Despesas.WebApi;"
$filefilters = "$baseDirectory\Despesas.DataSeeders\**;-$baseDirectory\Migrations.MySqlServer\**;-$baseDirectory\Migrations.MsSqlServer\**;-$baseDirectory\Despesas.CrossCutting\**;-$baseDirectory\Despesas.Business\HyperMedia\**"
$reportPath = Join-Path -Path (Get-Location) -ChildPath "TestResults"
$coveragePath = Join-Path -Path $reportPath -ChildPath "coveragereport"
$coverageAngularPath = Join-Path -Path $projectAngular -ChildPath "coverage"

function Wait-Angular-TestResults {
    $REPEAT_WHILE = 0
    while (-not (Test-Path $coverageAngularPath)) {
        echo "Agaurdando Coverage Report..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }
} 

# Gera o Relatório de Cobertura do Backend
dotnet test ./XUnit.Tests.csproj --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore --no-build > $null 2>&1
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml  -targetdir:$coveragePath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters > $null 2>&1

# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
	cd $projectAngular
	npm install
	cd $projectTestPath 
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow
Wait-Angular-TestResults