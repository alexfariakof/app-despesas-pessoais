# Defina o diretório do projeto (onde o arquivo docker-compose.yml está localizado)
$projectDirectory = ".\despesas-backend-api-net-core"

# Comando para realizar o build
$buildCommand = "dotnet build -restore"

# Comando para iniciar a aplicação em segundo plano
$startCommand = "Start-Process 'dotnet' -ArgumentList 'run', '--project', '$projectDirectory' -NoNewWindow -Wait"

# Execute o comando de build
Invoke-Expression $buildCommand

# Verifique se o build foi bem-sucedido
if ($LASTEXITCODE -eq 0) {
    # Se o build for bem-sucedido, execute o comando para iniciar a aplicação em segundo plano
    Start-Process "http://localhost:42535/swagger"
    Invoke-Expression $startCommand
}
else {
    Write-Host "Falha ao construir a aplicação."
}



