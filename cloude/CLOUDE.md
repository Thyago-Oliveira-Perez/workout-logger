# CLAUDE.md

## Purpose

This file provides guidance for AI coding assistants working in the **Workout Logger** repository.

The goal is to ensure that generated code respects the architecture, conventions, and structure of the project.

This project follows a **Modular Monolith architecture with ASP.NET Core and .NET MAUI**.

AI assistants should prioritize **clean architecture, clear module boundaries, and maintainability**.

---

# Project Overview

Workout Logger is a cross-platform application that allows users to:

* Log workout sessions
* Record exercises, sets, reps, and weights
* Track personal records
* Visualize progress with charts

The system consists of:

* **ASP.NET Core backend**
* **Modular monolith architecture**
* **PostgreSQL database**
* **.NET MAUI mobile application**
* **.NET Aspire orchestration**
* **OpenTelemetry observability**

---

# Repository Structure

All backend code lives inside `src`.

```text
workout-logger/

src/

    WorkoutLogger.Api
    WorkoutLogger.Infrastructure

    WorkoutLogger.Modules.Exercises
    WorkoutLogger.Modules.Workouts
    WorkoutLogger.Modules.Users
    WorkoutLogger.Modules.Progress

    WorkoutLogger.Shared

    WorkoutLogger.AppHost
    WorkoutLogger.ServiceDefaults

apps/

    WorkoutLogger.Mobile

tests/

    WorkoutLogger.IntegrationTests

docs/
utils/

PRD.md
CLAUDE.md
README.md
```

---

# Architecture

## Modular Monolith

The backend is split into independent modules.

Modules:

* Exercises
* Workouts
* Users
* Progress

Each module owns:

* entities
* DTOs
* endpoints
* validators
* services

Modules should **not directly depend on each other**.

Shared logic belongs in:

```
WorkoutLogger.Shared
```

Database infrastructure belongs in:

```
WorkoutLogger.Infrastructure
```

---

# Dependency Flow

Dependencies must follow this direction:

```
Mobile
   ↓
API
   ↓
Modules
   ↓
Infrastructure
   ↓
Shared
```

Never reverse this dependency order.

Examples of forbidden dependencies:

* Infrastructure referencing Modules
* Modules referencing API
* Shared referencing Infrastructure

---

# Backend Design Rules

## Minimal APIs

Endpoints should be implemented using **Minimal APIs**.

Endpoints should remain thin.

Example responsibilities of endpoints:

* receive request
* validate request
* call service
* return result

Do not place business logic inside endpoints.

---

## Business Logic

Business logic must live inside **services inside modules**.

Example:

```
Modules.Workouts/
    Services/
        WorkoutService.cs
```

Services should handle:

* domain logic
* calculations
* orchestration of database operations

---

## Entities

Entities represent database models.

Entities live inside module folders:

```
Modules.Workouts/Entities/
Modules.Exercises/Entities/
```

Entities should not contain infrastructure code.

---

## DTOs

DTOs represent API payloads.

DTOs must be used when returning data from endpoints.

Never return EF entities directly.

Example location:

```
Modules.Exercises/Dtos/
```

---

## Validation

Validation should use **FluentValidation**.

Validators belong inside modules.

Example:

```
Modules.Exercises/Validators/CreateExerciseValidator.cs
```

---

# Database

Database engine:

PostgreSQL

ORM:

Entity Framework Core

Database access should go through:

```
WorkoutLoggerDbContext
```

DbContext lives in:

```
WorkoutLogger.Infrastructure/Persistence/
```

Modules should access DbContext via dependency injection.

---

# Aspire Orchestration

Aspire manages the development environment.

Services managed by Aspire:

* API
* PostgreSQL
* OpenTelemetry collector
* Aspire dashboard

AppHost project:

```
WorkoutLogger.AppHost
```

Example responsibilities:

* configure containers
* configure environment
* run services locally

---

# Observability

The system uses **OpenTelemetry**.

Metrics and traces should include:

* API request latency
* database query time
* errors

Observability is visualized through the Aspire dashboard.

---

# Mobile Application

The mobile application is built with **.NET MAUI**.

Location:

```
apps/WorkoutLogger.Mobile
```

Architecture:

MVVM

Structure:

```
Views/
ViewModels/
Services/
```

Mobile communicates with backend using HttpClient.

---

# Charts

Charts are implemented with:

```
LiveChartsCore.SkiaSharpView.Maui
```

Charts used in the application:

Line charts → weight progression

Bar charts → workout volume

---

# Integration Tests

Integration tests live in:

```
tests/WorkoutLogger.IntegrationTests
```

Testing framework:

```
xUnit
```

Infrastructure:

```
TestContainers
```

Tests should use real PostgreSQL containers.

---

# Coding Conventions

Follow standard .NET naming conventions.

Rules:

* PascalCase for classes
* camelCase for variables
* Async methods must end with `Async`
* Use dependency injection
* Avoid static classes unless necessary

Prefer small, focused classes.

---

# Guidelines for AI Assistants

When generating code:

1. Respect module boundaries.
2. Do not introduce cross-module dependencies.
3. Place files in the correct module folder.
4. Avoid placing logic inside endpoints.
5. Prefer service classes for domain logic.
6. Use DTOs for API responses.
7. Use FluentValidation for request validation.
8. Use EF Core for database access.

When unsure where to place code, follow the existing module structure.

---

# Typical Feature Workflow

When implementing a new feature, follow this order:

1. Create entity (if needed)
2. Create DTOs
3. Create validator
4. Implement service logic
5. Implement API endpoint
6. Add integration tests
7. Connect mobile UI

---

# Long-Term Goals

The system may eventually include:

* offline mode with SQLite
* synchronization engine
* workout templates
* push notifications
* AI-generated training suggestions

These features should maintain the same architectural principles.

---

# Summary

Key architectural goals:

* clean modular monolith
* clear separation of concerns
* testable services
* thin API endpoints
* observable infrastructure
* cross-platform mobile client

AI assistants should prioritize **clarity, maintainability, and modular design** when modifying this repository.
