# Zeiss API — Product Management Backend

## Project Overview

This is a .NET 10 Web API coding challenge implementing CRUD and inventory-management
endpoints for a Product entity. Candidates were asked to build a RESTful API following
industry-standard design patterns and error handling, using Entity Framework Core with a
code-first approach, database seeding, and a consuming Angular frontend (frontend covered
in a separate README).

**Core requirements:**
- Standard CRUD endpoints for products (`GET`, `POST`, `PUT`, `DELETE`)
- Stock management endpoints (increment/decrement stock by quantity)
- Partial-match product search by name
- Product filtering by stock range
- Product IDs must be unique, auto-generated 6-digit numbers — safe even across multiple
  concurrently running instances
- Validation on product creation/update
- Every product response includes current stock
- Code-first EF Core migrations with seeded initial data

## Tech Stack

- **.NET 10** / ASP.NET Core Web API (Controllers)
- **Entity Framework Core** with **SQLite** (dev database; swappable via EF Core's provider
  model)
- Clean, layered architecture (Controllers → Services → `DbContext`), following
  Domain-Driven Design principles where practical for a project of this size

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- EF Core CLI tools:
  ```
  dotnet tool install --global dotnet-ef
  ```

## Getting Started

```bash
# Restore dependencies
dotnet restore

# Apply database migrations (creates products.db and seeds initial data)
dotnet ef database update

# Run the API
dotnet run
```

By default the API listens on the URLs configured in `Properties/launchSettings.json`
(HTTP and HTTPS profiles). Use:
```
dotnet run --launch-profile https
```
to run with HTTPS enabled locally.

## API Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/api/products` | List all products |
| GET | `/api/products/{id}` | Get a single product by id |
| POST | `/api/products` | Create a new product |
| PUT | `/api/products/{id}` | Update an existing product (partial update) |
| DELETE | `/api/products/{id}` | Delete a product |
| POST | `/api/products/{id}/decrement-stock/{quantity}` | Decrement stock by quantity |
| POST | `/api/products/{id}/add-to-stock/{quantity}` | Increment stock by quantity |
| GET | `/api/products/search?name={name}` | Search products by partial name match |
| GET | `/api/products/stock-level?min={min}&max={max}` | Filter products by stock range |

## Design Decisions & Assumptions

- **Product ID generation**: IDs are random 6-digit numbers, generated in the service layer
  and enforced unique via the database's primary key constraint. On a collision
  (`DbUpdateException`), the service retries generation up to a fixed attempt limit before
  throwing `IdGenerationException`. This delegates the actual uniqueness guarantee to the
  database, making it safe under concurrent/multi-instance writes.
- **No repository layer**: Services interact with `DbContext` directly. EF Core's `DbSet<T>`
  already provides repository-like behavior (querying, tracking, persistence), so an
  additional repository abstraction was judged unnecessary for this project's scope.
- **Category as a related entity**: Products reference a `Category` via a foreign key
  rather than a flat string field, to demonstrate relational modeling. The frontend is
  expected to populate category choices from `GET /api/categories` (a dropdown), so the
  product-creation DTO accepts a `CategoryName` rather than a raw `CategoryId`.
- **Partial updates**: `PUT` accepts a DTO with fully nullable fields; only fields present
  in the request are applied. Stock is intentionally excluded from the general update DTO,
  since stock changes are expected to go through the dedicated increment/decrement
  endpoints.
- **Error handling**: A custom `AppException` hierarchy (each carrying an HTTP status code)
  represents expected business-rule failures (not found, insufficient stock, stock
  overflow, id-generation exhaustion). These are caught centrally by an `IExceptionHandler`
  implementation and returned as standard `ProblemDetails` responses.
- **Database**: SQLite is used for local development for zero-setup simplicity. EF Core's
  provider abstraction means switching to SQL Server later only requires changing the
  provider package and connection string.

## Project Structure

```
zeiss_api/
├── Controllers/     # HTTP endpoints, thin — delegate to services
├── Models/           # EF Core entities (Product, Category)
├── DTOs/             # Request/response shapes
├── Services/         # Business logic (ID generation, stock rules, validation)
├── Exceptions/        # Custom AppException hierarchy + centralized handler
├── Data/              # ApplicationDbContext, seed data configuration
└── Migrations/        # EF Core code-first migrations
```