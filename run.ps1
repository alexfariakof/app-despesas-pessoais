

# Função para matar processos com base no nome do processo
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

# Encerra qualquer processo em segundo plano relacionado ao comando npm run test:watch
Stop-ProcessesByName

dotnet clean

# Defina o diretório do projeto (onde o arquivo docker-compose.yml está localizado)
$projectDirectory = ".\despesas-backend-api-net-core"

Invoke-Expression "dotnet clean"

# Comando para realizar o build
$buildCommand = "dotnet build -restore"

# Verifique se o parâmetro -w foi passado
if ($args -contains "-w") {
    # Se o parâmetro -w foi passado, configure o comando para usar 'dotnet watch run'
    $startCommand = "dotnet watch run --project $projectDirectory"
}
else {
    # Caso contrário, use o comando padrão 'dotnet run'
    $startCommand = "dotnet run --project $projectDirectory"
}

# Execute o comando de build
Invoke-Expression $buildCommand

# Verifique se o build foi bem-sucedido
if ($LASTEXITCODE -eq 0) {
    # Se o build for bem-sucedido, execute o comando para iniciar a aplicação em segundo plano
    Start-Process "http://localhost:42535/swagger"; Invoke-Expression $startCommand
    
}
else {
    Write-Host "Falha ao construir a aplicação."
}
