# Controllers Folder — Instructions for Contributors & AI Assistants

## Purpose
This document explains conventions, patterns, and best practices for code in the `Controllers/` folder. Controllers handle HTTP requests, validate input, call services, and return responses. They should remain thin and focused on API concerns.

## High-level Patterns
- Controllers should be thin: validate input, call a service method, and map results to DTOs or HTTP responses.
- Avoid business logic in controllers; delegate to services.
- Use appropriate HTTP status codes and response formats.

## Naming & Structure
- Class: `{Entity}Controller` (e.g., `RaffleController`).
- Methods: Use RESTful naming (e.g., `GetAll`, `GetById`, `Create`, `Update`, `Delete`).
- Routes: Use attribute routing with `[Route("api/[controller]")]` and `[HttpGet]`, etc.

## Dependency Injection
- Inject service interfaces via constructor (e.g., `IRaffleService`).
- Do not inject repositories directly; use services.

## Input Validation & DTOs
- Use DTOs for request/response bodies.
- Validate inputs using FluentValidation validators registered in `Program.cs`.
- Return `BadRequest` for validation errors.

## Response Handling
- Return `IActionResult` or specific types like `Ok()`, `NotFound()`, `BadRequest()`.
- Use DTOs for response bodies.
- Handle exceptions via middleware; controllers should not catch general exceptions. Throw errors from services.

## Async Patterns
- Use async methods: `Task<IActionResult>` and `async/await`.

## Logging
- Controllers do not use `ILogger` at all. Logging is handled solely in the exception middleware.

## Example Controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class RaffleController : ControllerBase
{
    private readonly IRaffleService _raffleService;

    public RaffleController(IRaffleService raffleService)
    {
        _raffleService = raffleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var raffles = await _raffleService.GetAllAsync();
        return Ok(raffles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var raffle = await _raffleService.GetByIdAsync(id);
        if (raffle == null) return NotFound();
        return Ok(raffle);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRaffleDto dto)
    {
        var result = await _raffleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
```

## Unit Testing
- Test controllers by mocking services.
- Focus on HTTP behavior, not business logic.

## Security
- Use `[Authorize]` for protected endpoints.
- Validate user permissions in services if needed.