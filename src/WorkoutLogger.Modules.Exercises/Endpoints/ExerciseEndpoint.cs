using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WorkoutLogger.Infrastructure.Persistence;
using WorkoutLogger.Modules.Exercises.Dtos;
using WorkoutLogger.Modules.Exercises.Entities;

namespace WorkoutLogger.Modules.Exercises.Endpoints;

public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/exercises", async (WorkoutLoggerDbContext db) => await db.Set<Exercise>().ToListAsync());

        app.MapPost("/exercises", async (
            CreateExerciseDto dto,
            WorkoutLoggerDbContext db) =>
        {
            var exercise = new Exercise
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                MuscleGroup = dto.MuscleGroup,
                Category = dto.Category
            };

            db.Set<Exercise>().Add(exercise);

            await db.SaveChangesAsync();

            return Results.Created($"/exercises/{exercise.Id}", exercise);
        });
    }
}