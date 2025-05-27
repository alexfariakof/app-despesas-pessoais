#!/bin/bash

arquivo_base="webapi-cert"
chave_privada="${arquivo_base}.crt"
certificado="${arquivo_base}.pem"
arquivo_pfx="${arquivo_base}.pfx"

echo "Gerando chave privada RSA..."
    openssl req -new -newkey rsa:2048 -days 365 -nodes -x509 -keyout $chave_privada -out $certificado -config openssl.cnf
echo "Chave privada gerada: $chave_privada"

echo "Gera o certificado PFX autoassinado se não existir"
openssl pkcs12 -export -out "$arquivo_pfx" -inkey "$chave_privada" -in "$certificado" 
