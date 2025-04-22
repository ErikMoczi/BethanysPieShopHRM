# Database Migrations

## EntityFramework Migrations

All migrations are located inside `BethanysPieShopHRM.Api/Migrations`

### Requirements:
- `dotnet tool install --global dotnet-ef`

### New migrations

Adding new migration via CLI:
- `dotnet ef migrations add Initial --context AppDbContext --output-dir Migrations`

#### Manual updates

- `dotnet ef database update --context AppDbContext`

#### Rollback changes

Revert migration
- `dotnet ef migrations remove --context AppDbContext`

Rollback database
- `dotnet ef database update Initial --context AppDbContext`
