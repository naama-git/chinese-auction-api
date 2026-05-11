Plan: Microservices Migration

TL;DR — Move the monolith to a small set of microservices in phases to reduce risk: start by extracting Catalog (prizes/categories/donors), then User/Identity, Ticketing, Ordering/Cart (with saga), Raffle/Winner, and finally reporting/observability. Use DB-per-service, an event-driven choreography (message broker), and an API Gateway for auth/routing. Phases keep the monolith running, use contract tests and events for consistency, and add middleware-based centralized logging and preserved rethrows (`throw;`) as documented in the repo docs.

Steps
1. Prep & discovery:
   - Create an ERD from migrations and models and produce a schema document.
   - Inventory external integrations (payments, email/SMS) and authentication flow (Token issuance).
   - Add integration tests and comprehensive unit tests for critical flows: ticket purchase, checkout, prize CRUD.
   - Add CI pipeline and containerization (Dockerfile) stubs for the monolith.

2. Phase 1 — Extract Catalog/Prize Service (read-only initially):
   - Create a new Catalog microservice project that owns Prize, Category, Donor, Package tables and migrations.
   - Move PrizeController endpoints and PrizeService/PrizeRepository (and DTO/Mapping/Validation related to prize) into Catalog service.
   - Keep Catalog read-only for other services initially (other services call Catalog read API rather than DB).
   - Publish OpenAPI for Catalog and create contract tests.
   - Introduce events from Catalog for PrizeUpdated and PrizeDeleted (when applicable).

3. Phase 2 — Extract User/Identity Service:
   - Create User service to own Users table and to handle registration/login (or integrate with chosen IdP).
   - Decide whether to centralize token issuance in this service or use an external IdP.
   - Update frontend and Gateway to request tokens from the new service.
   - Update services to validate JWT via gateway or shared auth middleware.

4. Phase 3 — Extract Ticketing Service and introduce events:
   - Create Ticketing service owning tickets table and ticket purchase APIs.
   - Change TicketService in monolith to call Ticketing service API (or replace with client) for purchases.
   - Publish TicketPurchased events on successful purchase with payload (ticketId, prizeId, userId, qty).
   - Catalog service subscribes to TicketPurchased to decrement available qty or produce PrizeQtyDecremented events.

5. Phase 4 — Extract Ordering/Cart & Payment integration (saga):
   - Create Order/Cart service owning cart and order tables.
   - Implement checkout saga: OrderCreated -> payment -> OrderPaid -> Ticketing: reserve/create tickets; if payment fails trigger compensating actions.
   - Adopt a saga orchestration or choreography pattern (recommend choreography using events to reduce coupling).

6. Phase 5 — Extract Raffle/Winner Service:
   - Create Raffle service that reads tickets and selects winners; owns Winner entity or a winners projection.
   - Raffle service subscribes to TicketCreated/TicketBatch events to build in-memory or persisted participant lists.
   - When draw executed, write winners and emit WinnerSelected event for notifications.

7. Phase 6 — Hardening, observability, and read-models:
   - Migrate logs to centralized system (Seq/ELK) and configure structured logging and correlation across services.
   - Create read-models or reporting service subscribing to events to provide analytics.
   - Introduce API Gateway and centralized rate-limiting, CORS, and auth enforcement.
   - Finalize CI/CD pipelines and rolling deployment patterns (blue/green or canary).

Verification
- Unit tests for services and repositories moved to each microservice project (create Tests/ for each new service).
- Contract tests (Pact or Postman collections) between services (e.g., Catalog <-> Ticketing).
- End-to-end flows executed via Postman/Newman: prize listing, ticket purchase, checkout, raffle draw.
- Integration tests for event flows: producer emits event and consumer processes and reflects state change.
- Run DB migration tests: each microservice has its own migration set and can migrate its DB schema independently.
- Smoke tests via API Gateway to verify auth and routing.

Decisions (recommended)
- DB-per-service: recommended for autonomy; use dual-write or read replicas during cutover.
- Event-driven choreography: prefer choreography (events) over a central orchestrator to reduce coupling; use a message broker (RabbitMQ/Kafka/Azure Service Bus).
- API Gateway: centralize JWT validation and do not duplicate request-level logs across services (use middleware).
- Auth: central Identity service or managed IdP (recommended) to avoid per-service token logic.

Blockers & Questions
- Which message broker and hosting platform will you use? (Kafka/RabbitMQ/Azure Service Bus; Kubernetes or simple container hosts)
- Do you want a dedicated Identity service, or will you use an external IdP?
- Are there external payment/notification providers to integrate (and which ones)?
- Do you want to containerize and deploy to Kubernetes, App Service, or another platform?

Risks & Mitigations
- Data consistency (High): mitigate with event-driven patterns, idempotent handlers, and compensating events.
- Auth fragmentation (High): centralize identity or use a managed IdP; validate tokens at gateway.
- Operational complexity (Medium): phase the rollout (Catalog + User + Ticketing first), build CI/CD and centralized observability.

Next actions
- Produce a detailed per-phase implementation checklist with exact file moves and code-change templates, or
- Implement a ready-to-use `RequestLoggingMiddleware` in `Middlewares/` and wire it in `Program.cs`, and add a `Dockerfile` + basic CI stub.

Choose which next action you want me to take.