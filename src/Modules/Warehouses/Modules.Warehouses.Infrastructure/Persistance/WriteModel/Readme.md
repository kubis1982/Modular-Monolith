# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add InitialDb --context WriteDbContext --output-dir .\Persistance\WriteModel\Migrations --project .\src\Modules\Warehouses\Modules.Warehouses.Infrastructure\Modules.Warehouses.Infrastructure.csproj --startup-project .\src\Modules\Warehouses\Modules.Warehouses.Infrastructure\Modules.Warehouses.Infrastructure.csproj
## Aktualizacja bazy danych
dotnet ef database update --context WriteDbContext --project .\src\Modules\Warehouses\Modules.Warehouses.Infrastructure\Modules.Warehouses.Infrastructure.csproj

