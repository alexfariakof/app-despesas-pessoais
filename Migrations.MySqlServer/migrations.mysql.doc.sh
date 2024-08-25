### MySqlSever Add Migrations 
## � nesser�ro setar a variavel de ambiente para Migratiosn antes de Usar o Migrations 
## Set-Item -Path Env:DOTNET_ENVIRONMENT -Value "Migrations"

## Migrations.Application
dotnet ef migrations add UpdateDataBase -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./Despesas.WebApi -o Migrations.Application
dotnet ef migrations add Change-Ids-TypeInt-to-UUID -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./Despesas.WebApi -o Migrations.Application
dotnet ef database update -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./Despesas.WebApi


### Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects

dotnet ef database update 20231222054303_Changes-relationship-between-Card-and-CardBrand
dotnet ef migrations add UpdateDataBase -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./Despesas.WebApi -o Migrations.Application
