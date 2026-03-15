namespace WorkoutLogger.Modules.Workouts.Entities;

public class WorkoutSession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
}