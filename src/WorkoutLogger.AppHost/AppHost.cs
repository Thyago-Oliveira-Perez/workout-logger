var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var database = postgres.AddDatabase("workoutlogger");

builder
    .AddProject<Projects.WorkoutLogger_Api>("api")
    .WithReference(database);

builder.Build().Run();