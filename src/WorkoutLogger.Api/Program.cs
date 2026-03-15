using Microsoft.EntityFrameworkCore;
using WorkoutLogger.Infrastructure.Persistence;
using WorkoutLogger.Modules.Exercises.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("workoutlogger");

builder.Services.AddDbContext<WorkoutLoggerDbContext>(
    options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapExerciseEndpoints();

app.Run();
