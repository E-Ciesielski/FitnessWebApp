using FitnessWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessWebApp.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<CaloriesLog> CaloriesLogs { get; set; }

    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}