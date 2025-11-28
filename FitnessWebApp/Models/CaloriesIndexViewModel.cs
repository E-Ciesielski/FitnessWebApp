namespace FitnessWebApp.Models;

public class CaloriesIndexViewModel
{
    public DateOnly SelectedDate { get; set; }
    public required List<CaloriesLog> CaloriesLogs { get; set; }
    public int TotalCalories { get; set; }
}