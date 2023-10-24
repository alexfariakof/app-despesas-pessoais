#!/bin/bash

projectDirectory="./"
docker-compose -f $projectDirectory/docker-compose.yml down
docker-compose -f $projectDirectory/docker-compose.database.yml down

if [ "$1" == "-local" ]; then
    
    buildCommand="docker-compose -f $projectDirectory/docker-compose.database.yml build"
    startCommand="docker-compose -f $projectDirectory/docker-compose.database.yml up -d"

    eval "$buildCommand"
    if [ $? -eq 0 ]; then
        eval "$startCommand"
        if [ $? -eq 0 ]; then
            start "http://localhost:42535/swagger"
        else
            echo "Falha ao iniciar os contêineres."
        fi
    else
        echo "Falha ao construir os contêineres."
    fi
else    
    buildCommand="docker-compose -f $projectDirectory/docker-compose.yml build"
    startCommand="docker-compose -f $projectDirectory/docker-compose.yml up -d"

    eval "$buildCommand"
    if [ $? -eq 0 ]; then
        eval "$startCommand"
        if [ $? -eq 0 ]; then
            start "http://localhost:42535/swagger" 
        else
            echo "Falha ao iniciar os contêineres."
        fi
    else
        echo "Falha ao construir os contêineres."
    fi

fi