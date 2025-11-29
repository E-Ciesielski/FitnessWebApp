using FitnessWebApp.Data;
using FitnessWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessWebApp.Controllers;

[Authorize]
public class CaloriesController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CaloriesController(AppDbContext context,  UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index([FromQuery()] string? date = null)
    {
        var selectedDate = DateOnly.FromDateTime(DateTime.Now);
        if (DateOnly.TryParseExact(date, "yyyy-MM-dd",out DateOnly dateParsed))
        {
            selectedDate = dateParsed;
        }
        
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        
        var caloriesLog = _context.CaloriesLogs.Where(c => c.Date == selectedDate).Where(c => c.UserId == userId).ToList();
        var totalCalories = caloriesLog.Sum(c => c.Calories);
        return View(new CaloriesIndexViewModel { CaloriesLogs = caloriesLog, SelectedDate = selectedDate,  TotalCalories = totalCalories });
    }

    public IActionResult Create([FromQuery()] string? date = null)
    {
        var selectedDate = DateOnly.FromDateTime(DateTime.Now);
        if (DateOnly.TryParseExact(date, "yyyy-MM-dd",out DateOnly dateParsed))
        {
            selectedDate = dateParsed;
        }

        var caloriesLog = new CaloriesLog()
        {
            Name = "",
            Date = selectedDate,
        };
        return View(caloriesLog);
    }

    [HttpPost]
    public IActionResult Create(CaloriesLog caloriesLog)
    {
        if (!ModelState.IsValid)
        {
            return View(caloriesLog);
        }
        
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        
        caloriesLog.UserId = userId;
        _context.CaloriesLogs.Add(caloriesLog);
        _context.SaveChanges();
        
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }

    [HttpGet("edit/{id}")]
    public IActionResult Edit(int id)
    {
        var caloriesLog = _context.CaloriesLogs.Find(id);
        
        var userId = _userManager.GetUserId(User);
        if (caloriesLog == null)
        {
            return NotFound();
        }
        
        if (userId == null || caloriesLog.UserId != userId)
        {
            return Unauthorized();
        }
        
        return View(caloriesLog);
    }
    
    [HttpPost("edit/{id}")]
    public IActionResult Edit(int id, CaloriesLog caloriesLog)
    {
        if (!ModelState.IsValid)
        {
            return View(caloriesLog);
        }
        
        if (id != caloriesLog.Id)
        {
            return BadRequest();
        }
        
        var userId = _userManager.GetUserId(User);
        var caloriesLogDb = _context.CaloriesLogs.Find(id);
        
        if (caloriesLogDb == null)
        {
            return NotFound();
        }
        
        if (userId == null ||  caloriesLogDb.UserId != userId)
        {
            return Unauthorized();
        }
        
        caloriesLogDb.Name = caloriesLog.Name;
        caloriesLogDb.Calories = caloriesLog.Calories;
        
        _context.SaveChanges();
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var caloriesLog = _context.CaloriesLogs.Find(id);
        if (caloriesLog == null)
        {
            return NotFound();
        }

        if (caloriesLog.UserId != userId)
        {
            return Unauthorized();
        }
        _context.CaloriesLogs.Remove(caloriesLog);
        _context.SaveChanges(); 
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }
}