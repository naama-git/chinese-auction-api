# ChineseAuctionAPI Instructions

## Project Overview & Goals
ChineseAuctionAPI is an ASP.NET Core Web API designed to manage raffles, prizes, packages, tickets, donors, and winners for a charity-style auction system. The primary goal is to provide a robust, scalable backend for auction operations, ensuring data integrity, security, and efficient business logic execution.

## Technical Stack
- **Language:** C#
- **Framework:** ASP.NET Core Web API (.NET 8.0)
- **ORM:** Entity Framework Core 8.0.22
- **Database:** SQL Server
- **Authentication:** JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer 8.0.22)
- **Caching:** Redis (Microsoft.Extensions.Caching.StackExchangeRedis 10.0.6)
- **Logging:** Serilog (Serilog.AspNetCore 10.0.0, with enrichers and sinks)
- **Validation:** FluentValidation (FluentValidation.DependencyInjectionExtensions 12.1.1)
- **Mapping:** AutoMapper (AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1)
- **Security:** BCrypt.Net-Next 4.0.3 for password hashing
- **Environment:** DotNetEnv 3.1.1 for environment variables
- **Documentation:** Swagger (Swashbuckle.AspNetCore 6.5.0, Swashbuckle.AspNetCore.Filters 8.0.2)
- **Other:** AutoFilterer 3.2.1 for filtering, Microsoft.OpenApi 1.6.14

## Architectural Rules
The project follows a Layered Architecture with Repository Pattern:
- **Controllers:** Handle HTTP requests and responses. Located in `Controllers/`.
- **Services:** Contain business logic, orchestrate operations using repositories. Services only use the repositories they consume and do not access the database directly via DbContext. Located in `Services/`. Interfaces in `Interface/`.
- **Repositories:** Handle data access via EF Core. Located in `Repositories/`. Interfaces in `Interface/`.
- **Models:** Entity classes. Located in `Models/`.
- **DTOs:** Data Transfer Objects. Located in `DTO/`.
- **Data:** EF Core DbContext. Located in `Data/`.
- **Mappings:** AutoMapper profiles or helpers. Located in `Mapping/`.
- **Validations:** FluentValidation validators. Located in `Validations/`.
- **Middlewares:** Custom middleware. Located in `Middlewares/`.

Flow: Controllers → Services → Repositories → DbContext.

Use dependency injection via constructor injection. Register services in `Program.cs`.

## Coding Standards
- **Naming Conventions:**
  - Classes: PascalCase (e.g., `RaffleService`).
  - Methods: PascalCase (e.g., `GetAllAsync`).
  - Variables/Properties: camelCase (e.g., `raffleId`).
  - Interfaces: I + PascalCase (e.g., `IRaffleService`).
  - Files: Match class names.
- **File Structure:** Follow existing folder conventions. Keep files small and focused.
- **Typing:** Use strong typing. Enable nullable reference types (`<Nullable>enable</Nullable>`).
- **Async/Await:** Use async methods throughout.
- **Error Handling and Logging:** Services, controllers, and repositories do not use `ILogger` at all. Instead, they throw custom exceptions (e.g., `ErrorResponse`), which are caught and logged solely in the exception middleware (`ExceptionHandlingMiddleware`). Preserve stack traces with `throw;`.
- **Security:** Hash passwords with BCrypt. Use JWT for authentication.
- **Migrations:** Create EF migrations for model changes. Use descriptive names.

## Self-Correction Clause
Before providing any code suggestions, implementations, or modifications, verify all proposals against this instructions file to ensure compliance with the project's architecture, coding standards, and technical stack. If any suggestion deviates, revise it to align or explain the deviation clearly.

## Modular Documentation
For detailed guidelines on specific components:
- **Controllers:** See [controllers-instructions.md](controllers-instructions.md) for API design, routing, validation, and response handling.
- **Services:** See [services-instructions.md](services-instructions.md) for business logic, dependency injection, transactions, and error handling.

## Common Tasks & Commands
- **Build and Run:** `dotnet build` then `dotnet run` from the project root.
- **Migrations:** Add with `dotnet ef migrations add <Name>`, apply with `dotnet ef database update`.
- **Tests:** Run `dotnet test` (no tests currently exist).
- **Configuration:** Use `appsettings.json` for settings. Avoid committing secrets; use environment variables or `dotnet user-secrets`.

## Pull Requests
- Title: Short description (e.g., "Add raffle creation endpoint").
- Body: Changes, rationale, testing instructions (include `dotnet` commands, expected results), DB/migration steps.

## Files of Interest
- `ChineseAuctionAPI.csproj`: Project configuration and dependencies.
- `Program.cs`: App startup, DI, middleware.
- `Data/ChineseAuctionDBcontext.cs`: EF Core context and DbSets.