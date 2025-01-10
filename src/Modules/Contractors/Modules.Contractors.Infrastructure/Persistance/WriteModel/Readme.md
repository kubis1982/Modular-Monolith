# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add InitialDb --context WriteDbContext --output-dir .\Persistance\WriteModel\Migrations --project .\src\Modules\Contractors\Modules.Contractors.Infrastructure\Modules.Contractors.Infrastructure.csproj --startup-project .\src\Modules\Contractors\Modules.Contractors.Infrastructure\Modules.Contractors.Infrastructure.csproj
## Aktualizacja bazy danych
dotnet ef database update --context WriteDbContext --project .\src\Modules\Contractors\Modules.Contractors.Infrastructure\Modules.Contractors.Infrastructure.csproj
## Usuwanie migracji
dotnet ef migrations remove --context WriteDbContext --project .\src\Modules\Contractors\Modules.Contractors.Infrastructure\Modules.Contractors.Infrastructure.csproj --startup-project .\src\Modules\Contractors\Modules.Contractors.Infrastructure\Modules.Contractors.Infrastructure.csproj
