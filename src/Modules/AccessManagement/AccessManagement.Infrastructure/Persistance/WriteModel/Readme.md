# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add InitialDb --context WriteDbContext --output-dir .\Persistance\WriteModel\Migrations --project .\src\Modules\AccessManagement\AccessManagement.Infrastructure\AccessManagement.Infrastructure.csproj --startup-project .\src\Modules\AccessManagement\AccessManagement.Infrastructure\AccessManagement.Infrastructure.csproj
## Aktualizacja bazy danych
dotnet ef database update --context WriteDbContext --project .\src\Modules\AccessManagement\AccessManagement.Infrastructure\AccessManagement.Infrastructure.csproj
## Usunięcie ostatniej migracji
dotnet ef migrations remove --context WriteDbContext --project .\src\Modules\AccessManagement\AccessManagement.Infrastructure\AccessManagement.Infrastructure.csproj --startup-project .\src\Modules\AccessManagement\AccessManagement.Infrastructure\AccessManagement.Infrastructure.csproj
