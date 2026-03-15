# PRD — Workout Logger

## 1. Project Overview

Workout Logger is a cross-platform application that allows users to log workouts, track exercises, record sets and weights, and visualize training progress over time.

The project is primarily a **learning platform** to practice:

* .NET MAUI
* Modular Monolith architecture
* ASP.NET Core Minimal APIs
* EF Core with PostgreSQL
* .NET Aspire orchestration
* Observability with OpenTelemetry
* Integration testing with TestContainers
* Docker deployment

The system converts raw workout data into meaningful visual progress charts.

Supported platforms:

* Android
* iOS
* Windows Desktop

Single codebase using **.NET MAUI**.

---

# 2. Core Features

## Workout Logging

Users can:

* Start a workout session
* Add exercises to the session
* Record multiple sets per exercise
* Log reps and weights
* Edit or delete sets

Example:

Bench Press
Set 1 — 80kg x 8
Set 2 — 85kg x 6
Set 3 — 90kg x 4

---

## Exercise Catalog

Users can browse or create exercises.

Each exercise contains:

* Name
* Muscle group
* Category

Example categories:

Push
Pull
Legs
Core

Example muscle groups:

Chest
Back
Shoulders
Biceps
Triceps
Quadriceps
Hamstrings

Users may create **custom exercises**.

---

## Workout History

Users can browse historical workouts.

Features:

* View workouts by date
* Filter by exercise
* Filter by muscle group
* Delete workout sessions

---

## Progress Tracking

The system generates charts showing training progress.

Charts must support:

* Smooth rendering for large datasets
* Responsive layouts
* Clear labels and axis values
* Highlight personal records

Types of charts:

Line Chart
Weight progression over time

Bar Chart
Workout volume per session

Example data:

Date | Weight
2026-01-01 | 80
2026-01-08 | 85
2026-01-15 | 90

---

## Personal Records

The system detects personal records per exercise.

Possible calculations:

Max weight lifted

or

Estimated 1RM using formula:

1RM = weight × (1 + reps / 30)

Personal records should appear in:

* Dashboard
* Progress charts
* Exercise detail screen

---

# 3. Technology Stack

Backend:

ASP.NET Core 10
Minimal APIs
Entity Framework Core
FluentValidation

Architecture:

Modular Monolith

Observability:

OpenTelemetry
.NET Aspire dashboard

Testing:

xUnit
TestContainers

Frontend:

.NET MAUI

Charts:

LiveChartsCore.SkiaSharpView.Maui

Database:

PostgreSQL (server)
SQLite (mobile local cache in future versions)

Deployment:

Docker
.NET Aspire generated compose

---

# 4. Repository Structure

Repository follows a **monorepo layout**.

```
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

README.md
PRD.md
```

---

# 5. Architectural Principles

## Modular Monolith

The backend is organized by modules.

Modules:

Exercises
Workouts
Users
Progress

Each module owns:

* Entities
* DTOs
* Endpoints
* Validators
* Services

Modules must **not directly depend on each other**.

Communication occurs through:

* shared database
* services exposed by modules

---

## Project Dependencies

Dependency flow must follow:

Mobile → API → Modules → Infrastructure → Shared

Aspire AppHost orchestrates:

API
Postgres
Observability stack

---

# 6. Module Structure

Each module follows the same internal structure.

Example:

```
WorkoutLogger.Modules.Exercises/

Entities/
Exercise.cs

Dtos/
ExerciseDto.cs
CreateExerciseDto.cs

Endpoints/
ExerciseEndpoints.cs

Validators/
CreateExerciseValidator.cs

Services/
ExerciseService.cs
```

Entities represent database models.

DTOs represent API payloads.

Endpoints contain Minimal API route mappings.

Validators use FluentValidation.

Services contain business logic.

---

# 7. Database Design

Primary tables:

### exercises

id (uuid)
name
muscle_group
category
created_at

---

### workout_sessions

id
user_id
date
notes

---

### workout_sets

id
session_id
exercise_id
set_number
reps
weight

Relationships:

WorkoutSession → WorkoutSets (1:N)

Exercise → WorkoutSets (1:N)

---

# 8. API Design

Minimal API endpoints live inside modules.

Example:

GET /exercises
POST /exercises

GET /workouts
POST /workouts

GET /progress/exercise/{id}

Responses should return DTOs.

Endpoints should remain thin.

Business logic must live in services.

---

# 9. Mobile Application

The mobile client is built with .NET MAUI.

Architecture pattern:

MVVM

Structure:

```
WorkoutLogger.Mobile/

Views/
DashboardPage
LogWorkoutPage
HistoryPage
ExercisesPage

ViewModels/
DashboardViewModel
LogWorkoutViewModel
HistoryViewModel

Services/
ApiClient
WorkoutService
ExerciseService
ProgressService
```

Mobile app communicates with API via HttpClient.

---

# 10. Observability

System uses OpenTelemetry through Aspire.

Metrics:

API latency
Database query duration
Errors

Tracing should capture:

Mobile request → API → database

Aspire dashboard provides visualization.

---

# 11. Integration Testing

Integration tests run against a real Postgres instance using TestContainers.

Example scenarios:

Create exercise
Create workout session
Add sets
Verify persistence

Test project:

WorkoutLogger.IntegrationTests

---

# 12. Deployment

Aspire generates container artifacts.

Command:

```
aspire publish -o docker-compose-artifacts
```

Resulting services:

API container
Postgres container
OpenTelemetry collector
Aspire dashboard

Deployment targets may include:

Railway
Fly.io
Docker VPS

---

# 13. Development Roadmap

## Phase 1

Project setup

* Solution structure
* Aspire configuration
* Postgres container
* EF Core setup

---

## Phase 2

Exercise module

Features:

Create exercise
List exercises

---

## Phase 3

Workout logging

Features:

Create workout session
Add sets
View session

---

## Phase 4

History screen

Browse past workouts

Filters:

Exercise
Muscle group

---

## Phase 5

Progress module

Generate chart data

Endpoints:

Progress per exercise

---

## Phase 6

Mobile UI

Screens:

Dashboard
Log Workout
History
Exercises

---

## Phase 7

Charts

Integrate LiveCharts

Display:

Weight progression
Workout volume

---

## Phase 8

Testing

Integration tests with TestContainers

---

## Phase 9

Deployment

Generate Docker compose with Aspire

Deploy API and Postgres.

---

# 14. Coding Rules

Minimal APIs must live inside modules.

Entities should not depend on API layer.

Business logic must live in services.

DTOs must be used for API communication.

Database access must go through EF Core DbContext.

Avoid logic inside controllers/endpoints.

---

# 15. Future Enhancements

Offline mode using SQLite

Data synchronization

Workout templates

Push notifications

Health platform integrations

AI workout recommendations

---

# 16. Project Goal

The main goal of the project is to build a **production-grade architecture while learning modern .NET ecosystem tools**.

Focus areas:

Mobile development
Backend design
Observability
Infrastructure
Deployment
