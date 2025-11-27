using FitnessWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessWebApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<CaloriesLog> CaloriesLogs { get; set; }

    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}