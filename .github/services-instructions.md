**Services Folder — Instructions for Contributors & AI Assistants**

Purpose
- This document explains conventions, patterns, and best practices for code in the `Services/` folder. Services contain business logic called by controllers, coordinating repositories, validation, mapping and transactions.

High-level patterns
- Keep services thin and focused: one service per aggregate or domain concept (e.g., `RaffleService`, `PrizeService`).
- Controllers should be thin: validate input, call a service, and map results to DTOs.
- Services orchestrate calls to repository interfaces (in `Repositories/`), perform mapping between entities and DTOs, and handle transactions when required. Services only use the repositories they consume and do not access the database directly via DbContext.

Naming & structure
- Interface: `I{Name}Service` in `Interface/` (e.g., `IRaffleService`).
- Implementation: `{Name}Service` in `Services/` (e.g., `RaffleService`).
- Place helper/mapping logic in a dedicated helper or mapping class if it gets larger than a few lines.

Dependency injection
- Register services in `Program.cs` with `builder.Services.AddScoped<I{Name}Service, {Name}Service>();`.
- Inject only the interfaces needed (repositories, mappers). Do not inject `ILogger` or `DbContext`.

Async patterns
- Use async methods throughout: `Task<T>` / `Task` and `async/await`.

Transactions & Save semantics
- Prefer unit-of-work via repository `SaveChangesAsync` where required.
- For multi-repository operations that must be atomic, coordinate through repositories (repositories handle DbContext internally).

Error handling & logging
- Services should validate business rules and throw well-defined exceptions when needed (e.g., `ErrorResponse`).
- Logging is concentrated solely in the exception middleware. Services, controllers, and repositories do not use `ILogger` at all. Instead, throw errors that are caught in the exception middleware.

Rethrowing exceptions
- When catching exceptions inside services, prefer to preserve the original stack trace by using `throw;` to rethrow, not `throw ex;`. Only wrap/replace an exception when adding meaningful context, and prefer `throw new MyDomainException("msg", ex)` if you need to preserve the original as an inner exception.

Logging in middleware
- Centralize request and unhandled-exception logging in middleware so every HTTP request and global error is captured consistently.
- Services should not log; throw exceptions for errors.

Mapping & DTOs
- Map Entities ↔ DTOs inside services or via mapping helpers (avoid controller-level mapping complexity).
- Keep DTOs in the `DTO/` folder and return DTOs from services when controllers are thin; otherwise return domain models and map in controller depending on team preference.

Unit testing
- Write unit tests for services by mocking repository interfaces.
- Prefer deterministic behavior: avoid accessing external resources in unit tests; use in-memory or mocked DB only for integration tests.

Example service interface
```csharp
public interface IRaffleService {
    Task<RaffleDto> GetByIdAsync(int id);
    Task<RaffleDto> CreateAsync(CreateRaffleDto dto);
}
```

Example service implementation (skeleton)
```csharp
public class RaffleService : IRaffleService {
    private readonly IRaffleRepo _raffleRepo;
    private readonly IMapper _mapper;

    public RaffleService(IRaffleRepo raffleRepo, IMapper mapper) {
        _raffleRepo = raffleRepo;
        _mapper = mapper;
    }

    public async Task<RaffleDto> GetByIdAsync(int id) {
        var entity = await _raffleRepo.GetByIdAsync(id);
        if (entity == null) {
            throw new ErrorResponse(404, "GetByIdAsync", "Raffle not found", $"No raffle with ID {id}", "GET", "RaffleService");
        }
        return _mapper.Map<RaffleDto>(entity);
    }

    public async Task<RaffleDto> CreateAsync(CreateRaffleDto dto) {
        // Business logic
        var entity = _mapper.Map<Raffle>(dto);
        await _raffleRepo.AddAsync(entity);
        return _mapper.Map<RaffleDto>(entity);
    }
}
```

    public RaffleService(IRaffleRepo raffleRepo, ChineseAuctionDBcontext dbContext, ILogger<RaffleService> logger) {
        _raffleRepo = raffleRepo;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<RaffleDto> GetByIdAsync(int id, CancellationToken cancellationToken) {
        var entity = await _raffleRepo.GetByIdAsync(id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException("Raffle not found");
        return MapToDto(entity);
    }

    public async Task<RaffleDto> CreateAsync(CreateRaffleDto dto, CancellationToken cancellationToken) {
        var entity = new Raffle { /* map fields */ };
        _raffleRepo.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapToDto(entity);
    }

    private RaffleDto MapToDto(Raffle e) => new RaffleDto { /* ... */ };
}
```

Common pitfalls
- Do not perform heavy validation or mapping in controllers; keep controllers as glue.
- Avoid synchronous DB calls, and avoid blocking (e.g., `.Result` or `.Wait()`).
- Avoid returning EF tracked entities from services to callers that may serialize them directly.

When proposing changes (AI assistant guidance)
- Provide minimal focused diffs touching only relevant files (`Services/`, `Interface/`, `Repositories/`, `DTO/`).
- Include migration commands and any `appsettings` changes if needed.
- Provide short, reproducible verification steps (HTTP request examples or `dotnet` commands).

Where to find help
- `Data/ChineseAuctionDBcontext.cs` — DB context
- `Repositories/` — data access patterns
- `Interface/` — available service & repo interfaces
