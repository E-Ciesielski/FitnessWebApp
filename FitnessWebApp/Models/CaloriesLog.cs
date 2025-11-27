namespace FitnessWebApp.Models;

public class CaloriesLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Calories { get; set; }
    public required string Name { get; set; }
}