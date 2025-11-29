using System.ComponentModel.DataAnnotations;
using FitnessWebApp.Data;
using Microsoft.AspNetCore.Identity;

namespace FitnessWebApp.Models;

public class CaloriesLog
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(0, 10_0000)]
    public int Calories { get; set; }
    
    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public required string Name { get; set; }
    
    public string? UserId { get; set; }
    
    public IdentityUser? User { get; set; }
    
    public MeasurementUnits? Unit { get; set; }
    
    [Range(0, 10_0000)]
    public int? CaloriesPerUnit { get; set; }
    
    [Range(0, 10_0000)]
    public float? Amount { get; set; }
}