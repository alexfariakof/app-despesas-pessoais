### MySqlSever Add Migrations 

## Migrations.Application
dotnet ef migrations add Initial -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./despesas-backend-api-net-core -o Migrations.Application
dotnet ef database update -c MySqlServerContext -p ./Migrations.MySqlServer/Migrations.MySqlServer.csproj -s ./despesas-backend-api-net-core


### Return to a state creatred by Mingrations
dotnet ef database update 20231221234827_InitialCreate
dotnet ef database update 20231222054303_Changes-Props_Email_Password_To_Value_Objects

dotnet ef database update 20231222054303_Changes-relationship-between-Card-and-CardBrand

