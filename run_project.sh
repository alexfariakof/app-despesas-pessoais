#!/bin/bash

# Defina o diretório do projeto (onde o arquivo docker-compose.yml está localizado)
projectDirectory="./"

# Comando para realizar o build dos contêineres
buildCommand="docker-compose -f $projectDirectory/docker-compose.yml build"

# Comando para iniciar os contêineres em modo detached (-d)
startCommand="docker-compose -f $projectDirectory/docker-compose.yml up -d"

# Execute o comando de build
eval "$buildCommand"

# Verifique se o build foi bem-sucedido
if [ $? -eq 0 ]; then
    # Se o build for bem-sucedido, execute o comando para iniciar os contêineres
    eval "$startCommand"

    # Verifique se o início dos contêineres foi bem-sucedido
    if [ $? -eq 0 ]; then
        # Se o início for bem-sucedido, abra a página HTML em um navegador padrão
        start "http://localhost:42535/swagger"  # Isso depende do ambiente de desktop Linux; pode variar
    else
        echo "Falha ao iniciar os contêineres."
    fi
else
    echo "Falha ao construir os contêineres."
fi