# DatingApp WebAPI - AI Coding Agent Instructions

## Project Overview
**DatingApp WebAPI** is an ASP.NET Core 9.0 Web API project for a dating application. It uses Entity Framework Core with SQLite for data persistence. The project is in early stages with basic scaffolding and a sample AppUser entity.

## Architecture & Key Components

### Technology Stack
- **Framework**: ASP.NET Core 9.0 (implicit usings, nullable reference types enabled)
- **ORM**: Entity Framework Core 9.0.5 with SQLite provider
- **Database**: SQLite (local file `dating.db`)
- **Project Type**: Web API (no UI layer)

### Directory Structure
- `Entities/` — Domain models (e.g., AppUser with Id, DisplayName, Email)
- `Data/` — EF Core DbContext (AppDbContext) managing database operations
- `Controllers/` — API endpoints (currently sample WeatherForecastController)
- `Properties/launchSettings.json` — Launch profiles for development

### Data Access Pattern
**AppDbContext** (primary DbContext) manages all database operations:
```csharp
public DbSet<AppUser> Users { get; set; }
```
Configured in `Program.cs` with SQLite connection string from `appsettings.Development.json`.

## Critical Workflows & Commands

### Build & Run
```powershell
# Build project
dotnet build

# Run development server (listens on localhost with HTTPS)
dotnet run

# Restore dependencies
dotnet restore
```

### Database Migrations (EF Core)
```powershell
# Create migration (requires dotnet-ef tool)
dotnet ef migrations add MigrationName --project DatingApp_WebAPI

# Apply migrations to database
dotnet ef database update --project DatingApp_WebAPI

# Drop database (careful!)
dotnet ef database drop --project DatingApp_WebAPI
```
**Note**: `dotnet-ef` tool must be installed globally: `dotnet tool install --global dotnet-ef`

### API Testing
- Use `DatingApp_WebAPI.http` file for REST client requests (built-in VS Code support)
- Sample endpoint: `GET [controller]` for WeatherForecast data

## Project-Specific Patterns & Conventions

### Entity Design
- All entities use **GUID-based Ids** (string type): `Id = Guid.NewGuid().ToString()`
- Use **required properties** for mandatory fields (e.g., `required string DisplayName`)
- Simple domain models without complex inheritance hierarchies

### DbContext Configuration
- DbContext uses **primary constructor pattern** (C# 12):
  ```csharp
  public class AppDbContext(DbContextOptions options) : DbContext(options)
  ```
- DbSet properties map directly to entities (`public DbSet<AppUser> Users`)

### Dependency Injection
- Controllers use constructor injection for `ILogger<T>`
- Services registered in `Program.cs` using `builder.Services`

### Configuration Management
- Environment-specific settings via `appsettings.json` (shared) and `appsettings.Development.json` (local)
- Connection strings stored in appsettings files (no secrets management configured yet)

## Integration Points & Dependencies

### External Dependencies
- **Microsoft.EntityFrameworkCore** — ORM for data access
- **Microsoft.EntityFrameworkCore.Design** — EF Core tooling (migrations)
- **Microsoft.EntityFrameworkCore.Sqlite** — SQLite provider

### Cross-Component Communication
- Controllers → AppDbContext (via constructor injection)
- Program.cs configures DbContext with SQLite connection
- No service layer or repository pattern yet (direct DbContext usage in controllers)

## Known Limitations & Future Considerations
- **No authentication/authorization** currently implemented (middleware exists but not configured)
- **OpenAPI (Swagger)** is commented out - can be enabled if needed
- **Sample controller (WeatherForecast)** should be replaced with domain-specific endpoints
- **No validation framework** (FluentValidation, etc.) — use data annotations for now
- **Database**: SQLite not suitable for production; migration to SQL Server recommended later

## Common Patterns to Follow
1. **Add new entities** in `Entities/` folder, then add DbSet to AppDbContext
2. **Create migrations** after schema changes: `dotnet ef migrations add <Name>`
3. **Create controllers** in `Controllers/` folder, inheriting from ControllerBase
4. **Inject AppDbContext** in controller constructors for data access
5. **Test endpoints** using the `.http` file in the project root
