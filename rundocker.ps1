# Defina o diretório do projeto (onde o arquivo docker-compose.yml está localizado)
$projectDirectory = ".\"

# Comando para realizar o build dos contêineres
$buildCommand = "docker-compose -f $projectDirectory\docker-compose.yml build"

# Comando para iniciar os contêineres em modo detached (-d)
$startCommand = "docker-compose -f $projectDirectory\docker-compose.yml up -d"

# Execute o comando de build
Invoke-Expression $buildCommand

# Verifique se o build foi bem-sucedido
if ($LASTEXITCODE -eq 0) {
    # Se o build for bem-sucedido, execute o comando para iniciar os contêineres
    Invoke-Expression $startCommand

    # Verifique se o início dos contêineres foi bem-sucedido
    if ($LASTEXITCODE -eq 0) {
        # Se o início for bem-sucedido, abra a página HTML em um navegador padrão        
        Start-Process "http://localhost:42535/swagger"
    } else {
        Write-Host "Falha ao iniciar os contêineres."
    }
} else {
    Write-Host "Falha ao construir os contêineres."
}
