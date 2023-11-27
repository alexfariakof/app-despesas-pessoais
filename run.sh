#!/bin/bash

# Defina o diretório do projeto (onde o arquivo docker-compose.yml está localizado)
projectDirectory="./despesas-backend-api-net-core"

# Comando para realizar o build
buildCommand="dotnet build -restore"

# Verifique se o parâmetro -w foi passado
if [ "$1" == "-w" ]; then
    # Comando de observação (watch)
    watchCommand="dotnet watch run --project $projectDirectory"
    eval "$watchCommand"
else
    # Execute o comando de build
    eval "$buildCommand"

    # Verifique se o build foi bem-sucedido
    if [ $? -eq 0 ]; then
        nohup dotnet run --project "$projectDirectory" > /dev/null 2>&1 &
        sleep 5
        start "http://localhost:42535/swagger"
    else
        echo "Falha ao construir a aplicação."
    fi
fi
