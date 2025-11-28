using System.ComponentModel.DataAnnotations;

namespace FitnessWebApp.Models;

public class CaloriesLog
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(0, 10_000)]
    public int Calories { get; set; }
    
    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public required string Name { get; set; }
}