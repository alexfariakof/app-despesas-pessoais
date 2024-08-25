### MsSqlSever Add Migrations 
## É nesseráro setar a variavel de ambiente para Migratiosn antes de Usar o Migrations 
## Set-Item -Path Env:DOTNET_ENVIRONMENT -Value "Migrations"

## Migrations.Application
dotnet ef migrations add Initial -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./Despesas.WebApi -o Migrations.Application
dotnet ef migrations add Change-Ids-TypeInt-to-UUID -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./Despesas.WebApi -o Migrations.Application

dotnet ef database update -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./Despesas.WebApi


# Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects
