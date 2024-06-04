### MsSqlSever Add Migrations 

## Migrations.Application
dotnet ef migrations add Initial -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./despesas-backend-api-net-core -o Migrations.Application
dotnet ef database update -c MsSqlServerContext -p ./Migrations.MsSqlServer/Migrations.MsSqlServer.csproj -s ./despesas-backend-api-net-core


# Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects
