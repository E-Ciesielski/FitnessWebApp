using FitnessWebApp.Data;
using FitnessWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessWebApp.Controllers;

public class CaloriesController : Controller
{
    private readonly AppDbContext _context;

    public CaloriesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index([FromQuery()] string? date = null)
    {
        var selectedDate = DateOnly.FromDateTime(DateTime.Now);
        if (DateOnly.TryParseExact(date, "yyyy-MM-dd",out DateOnly dateParsed))
        {
            selectedDate = dateParsed;
        }
        var caloriesLog = _context.CaloriesLogs.Where(c => c.Date == selectedDate).ToList();
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

        _context.CaloriesLogs.Add(caloriesLog);
        _context.SaveChanges();
        
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }

    [HttpGet("edit/{id}")]
    public IActionResult Edit(int id)
    {
        var caloriesLog = _context.CaloriesLogs.Find(id);
        return View(caloriesLog);
    }
    
    [HttpPost("edit/{id}")]
    public IActionResult Edit(int id, CaloriesLog caloriesLog)
    {
        if (!ModelState.IsValid)
        {
            return View(caloriesLog);
        }
        _context.CaloriesLogs.Update(caloriesLog);
        _context.SaveChanges();
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var caloriesLog = _context.CaloriesLogs.Find(id);
        if (caloriesLog == null)
        {
            return NotFound();
        }
        _context.CaloriesLogs.Remove(caloriesLog);
        _context.SaveChanges(); 
        return RedirectToAction("Index", "Calories", new { date = caloriesLog.Date.ToString("yyyy-MM-dd") });
    }
}