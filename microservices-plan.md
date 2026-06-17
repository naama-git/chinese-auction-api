Proposed Microservices Decomposition Plan

The monolith should be decomposed into bounded services around Identity, Catalog, Commerce, Raffle, and Media, each owning its own data and using a combination of synchronous gateway routing plus asynchronous events for cross-service consistency.

1. Domain Analysis

- Core bounded contexts
  - Identity / User Management: `UserController`, JWT auth, user roles
  - Catalog / Prize Management: `PrizeController`, `CategoryController`, `DonorController`, prize metadata
  - Commerce / Order & Cart: `CartController`, `OrderController`, `PackageController`, cart and order workflows
  - Raffle / Winner Engine: `RaffleController`, `WinnerController`, raffle execution, winner selection
  - Media / Upload: `UploadController` and asset storage

- Business capability categories
  - Identity: authentication, authorization, profile management
  - Data Storage: prize catalog, packages, carts, orders, tickets, winners
  - Processing: ticket purchase, checkout, raffle execution, winner selection
  - Gateway: API routing, auth enforcement, rate limiting

2. Microservices Blueprint

Service 1: Identity Service
- Responsibility: authenticate users, manage user profiles and roles, issue tokens
- Database strategy: private identity database
- Communication patterns:
  - Expose synchronous REST for login/register/profile
  - Use JWT for gateway and service-level auth
  - Publish events like `UserRegistered`, `UserUpdated` for downstream services

Service 2: Catalog Service
- Responsibility: manage prize inventory, donors, categories, package definitions
- Database strategy: private catalog database
- Communication patterns:
  - Expose synchronous REST for catalog queries and administration
  - Publish events such as `PrizeCreated`, `PrizeUpdated`, `PackageUpdated`
  - Subscribe to events for cache invalidation or downstream read-model updates

Service 3: Commerce Service
- Responsibility: manage carts, orders, checkout, and ticket reservation
- Database strategy: private commerce database
- Communication patterns:
  - Expose synchronous REST for cart and order APIs
  - Synchronously validate catalog entries via Catalog API on checkout
  - Publish `OrderCreated`, `PaymentSucceeded`, `TicketReserved` events
  - Subscribe to `PrizeUpdated` or `UserUpdated` when needed for data consistency

Service 4: Raffle Service
- Responsibility: execute raffle draws, select winners, manage winner history
- Database strategy: private raffle database
- Communication patterns:
  - Expose synchronous REST for raffle draw and winner retrieval
  - Subscribe to `TicketIssued`, `OrderCreated`, `PrizeUpdated` to build raffle state
  - Publish `WinnerSelected`, `RaffleCompleted` events

Service 5: Media Service (optional)
- Responsibility: handle image upload/download and media asset storage
- Database strategy: object/blob storage backed by optional metadata store
- Communication patterns:
  - Expose synchronous REST for uploads and signed URL generation
  - Catalog service stores media references, not blobs

3. Technical Implementation & Infrastructure

API Gateway
- Use a gateway such as Ocelot, YARP, Kong, or managed cloud gateway
- Responsibilities:
  - Route external requests to the correct service
  - Enforce authentication and authorization policies
  - Apply rate limiting, CORS, request validation, and API versioning

Inter-service Security
- Use mTLS inside the cluster or service mesh
- Use OAuth2 client credentials or signed JWTs for service-to-service auth
- Validate tokens at each service boundary, not only at the gateway
- Store credentials and secrets in a vault and rotate them regularly

Service Discovery
- In Kubernetes: use built-in DNS service discovery
- On VMs or containers: use Consul or a service mesh like Istio/Linkerd
- Prefer DNS-based discovery with static gateway routing for external ingress

Data Synchronization & Messaging
- Use a message broker such as RabbitMQ, Kafka, or Azure Service Bus
- Use asynchronous events for decoupled state propagation and eventual consistency
- Use private service databases; do not allow cross-service direct DB access
- Use Redis or a cache only for read-side performance layered on top of service data

4. Challenges & Risk Mitigation

Distributed Monolith pitfalls
- Avoid services accessing another service’s database directly
- Avoid shared domain logic libraries that create tight coupling
- Avoid long synchronous chains across many services in a single request
- Keep each service autonomous with independent deployment and schema

Distributed Transactions
- Use Saga Pattern for coordination across multiple services
- Prefer event choreography for simple, decoupled workflows
  - Example: Commerce publishes `OrderCreated`; Payment and Ticketing react
  - If a step fails, emit compensating events like `OrderCancelled`
- If needed, use a saga orchestrator for long-running checkout workflows
- Ensure event handlers are idempotent and saga state is persisted

Key Recommendations
- Use Database-per-Service for autonomy and failure isolation
- Use API gateway for routing, auth, and rate limiting
- Use event-driven choreography for cross-service workflows
- Centralize identity/auth in one service or external IdP
- Add centralized logging, tracing, and observability early

Next Steps
- Build the first extraction phase for Catalog and Identity
- Add an API gateway layer and wire JWT validation
- Introduce a message broker and publish key domain events
- Use phase-based rollout and contract tests to avoid large-scale refactor risk
