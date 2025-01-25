using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Expenses.Models;
using Expenses.Data;

namespace Expenses.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ExpensesDbContext _context;

    public HomeController(ILogger<HomeController> logger, ExpensesDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Expenses()
    {
        var allExpenses = _context.Expenses.ToList();
        var totalExpenses = allExpenses.Sum(expense => expense.Value);
        ViewBag.Expenses = totalExpenses;
        return View(allExpenses);
    }

    public IActionResult CreateEditExpense()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateEditExpense(int? id)
    {
        if (id is not null)
        {
            Expense? expense = _context.Expenses.Find(id);
            return View(expense);
        }
        return View();
    }
    [HttpPost]
    public IActionResult CreateEditExpense(Expense model)
    {
        if (model.Id == 0)
        {
            // Create
            _context.Expenses.Add(model);
        }
        else
        {
            // Edit
            Expense? expense = _context.Expenses.Find(model.Id);
            if (expense is null)
            {
                return NotFound();
            }
            expense.Value = model.Value;
            expense.Description = model.Description;
        }
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }

    public IActionResult DeleteExpense(int id)
    {
        Expense? expense = _context.Expenses.Find(id);
        if (expense is null)
        {
            return NotFound();
        }
        _context.Expenses.Remove(expense);
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
