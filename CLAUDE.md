# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

HubVision.API is an ASP.NET Core 8.0 web API built with Clean Architecture principles, designed for managing advertising campaigns across multiple platforms. The solution implements Domain-Driven Design (DDD) patterns with a multi-tenant architecture where agencies are the primary tenant entity.

## Solution Structure

The solution follows Clean Architecture with four main projects:

- **HubVision.Domain**: Core business entities, aggregates, and interfaces. Contains no external dependencies.
- **HubVision.Application**: Application logic and use cases (currently empty, reserved for CQRS/MediatR patterns).
- **HubVision.Infrastructure**: Data access implementation with Entity Framework Core, including DbContext, repositories, and EF configurations.
- **HubVision.API**: Web API entry point with controllers, dependency injection setup, and API configuration.

## Architecture Patterns

### Domain-Driven Design (DDD)

The domain layer is organized around aggregates in `HubVision.Domain/AggregatesModel/`:

- **AgencyAggregate**: The tenant root - represents advertising agencies
- **ClientAggregate**: Clients that belong to agencies (multi-tenant via TenantEntity)
- **TrafficManagerAggregate**: Users who manage campaigns within agencies
- **AdAccountAggregate**: Advertising accounts with platform credentials
- **PlatformAccountAggregate**: Platform-specific account connections
- **CampaignAggregate**: Campaign entities
- **AdSetAggregate**: Ad set groupings
- **AdAggregate**: Individual advertisements

All aggregate roots implement `IAggregateRoot` and inherit from `Entity<TKey>` or `TenantEntity<TKey>`.

### Multi-Tenancy

The system uses Agency-based multi-tenancy:
- `TenantEntity<TKey>` base class adds `AgencyId` and `Agency` navigation property
- All tenant-scoped entities inherit from `TenantEntity<Guid>`
- Repository implementations should filter by AgencyId when working with tenant entities

### Repository Pattern

- Generic repository interface: `IRepository<T, TKey>` in `HubVision.Domain.Core.Data`
- Implementation: `Repository<T, TKey>` in `HubVision.Infrastructure.Data`
- Repositories work only with aggregate roots (entities implementing `IAggregateRoot`)
- Each repository exposes its `UnitOfWork` for transaction coordination

### Unit of Work Pattern

- Interface: `IUnitOfWork` in `HubVision.Domain.Core.Data`
- Implementation: `UnitOfWork` in `HubVision.Infrastructure.Data`
- Provides transaction management: `BeginTransactionAsync()`, `CommitTransactionAsync()`, `RollbackTransactionAsync()`
- Use `SaveChangesAsync()` for non-transactional saves

## Database

- **Provider**: PostgreSQL via Npgsql.EntityFrameworkCore.PostgreSQL
- **ORM**: Entity Framework Core 9.0.4
- **Schema**: All tables are created in the "HV" schema (see `ApplicationDbContext.DEFAULT_SCHEMA`)
- **Naming Convention**: Snake case (via `EFCore.NamingConventions`)
- **Connection String**: Configured in `appsettings.json` under `ConnectionStrings:DefaultConnection`

### Entity Framework Configuration

- All entity configurations are in `HubVision.Infrastructure/EntityConfigurations/`
- Configurations use Fluent API via `IEntityTypeConfiguration<T>`
- Applied automatically via `modelBuilder.ApplyConfigurationsFromAssembly()` in `ApplicationDbContext`

### Migrations

Migrations are stored in `HubVision.Infrastructure/Migrations/` and managed through the Infrastructure project.

**Create a new migration**:
```bash
dotnet ef migrations add MigrationName --project HubVision.Infrastructure --startup-project HubVision.API
```

**Update database**:
```bash
dotnet ef database update --project HubVision.Infrastructure --startup-project HubVision.API
```

**Remove last migration** (if not applied):
```bash
dotnet ef migrations remove --project HubVision.Infrastructure --startup-project HubVision.API
```

## Building and Running

**Build the solution**:
```bash
dotnet build
```

**Run the API** (from solution root):
```bash
dotnet run --project HubVision.API
```

**Restore dependencies**:
```bash
dotnet restore
```

## Dependency Injection Setup

The DI configuration in `Program.cs` registers:

- `ApplicationDbContext` with PostgreSQL and snake_case naming
- `IUnitOfWork` → `UnitOfWork` (scoped)
- `IRepository<,>` → `Repository<,>` (scoped)

When adding new repositories or services, register them in `Program.cs` following this pattern.

## Key Design Decisions

1. **Guid Primary Keys**: All entities use `Guid` as their primary key type
2. **Factory Methods**: Aggregates use static `Create()` methods for instantiation with private constructors
3. **No Tracking by Default**: Repository `GetAll()` and `GetAllAsync()` methods use `AsNoTracking()` for read operations
4. **Schema Separation**: All tables use the "HV" schema to separate from default "public" schema
5. **Enum Storage**: Enums are stored as strings in the database (see `SubscriptionPlan` in `AgencyConfiguration`)

## Target Framework

- **.NET**: 8.0
- **Language Features**: C# 12 with nullable reference types enabled
- **Implicit Usings**: Enabled across all projects
