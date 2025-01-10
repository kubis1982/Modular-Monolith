# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add InitialDb --context WriteDbContext --output-dir .\Persistance\WriteModel\Migrations --project .\src\Modules\Orders\Modules.Orders.Infrastructure\Modules.Orders.Infrastructure.csproj --startup-project .\src\Modules\Orders\Modules.Orders.Infrastructure\Modules.Orders.Infrastructure.csproj
## Aktualizacja bazy danych
dotnet ef database update --context WriteDbContext --project .\src\Modules\Orders\Modules.Orders.Infrastructure\Modules.Orders.Infrastructure.csproj
## Usunięcie ostatniej migracji
dotnet ef migrations remove --context WriteDbContext --project .\src\Modules\Orders\Modules.Orders.Infrastructure\Modules.Orders.Infrastructure.csproj --startup-project .\src\Modules\Orders\Modules.Orders.Infrastructure\Modules.Orders.Infrastructure.csproj
