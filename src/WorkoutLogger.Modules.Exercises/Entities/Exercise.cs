namespace WorkoutLogger.Modules.Exercises.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup  { get; set; }
    public string Category { get; set; }
}