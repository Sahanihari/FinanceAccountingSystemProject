using Microsoft.AspNetCore.Mvc;
using FinanceAccountingSystem.Models;
using System.ComponentModel.DataAnnotations;
using FinanceAccountingSystem.Data;

public class CustomerController : Controller
{
    private readonly FinanceDbContext _context;

    public CustomerController(FinanceDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var customers = _context.Customers.ToList();
        ViewBag.AddressList = _context.Addresses.ToList();
        return View(customers);
    }

    [HttpPost]
    public IActionResult Create(Customer model)
    {
        if (ModelState.IsValid)
        {
            _context.Customers.Add(model);
            _context.SaveChanges();
        }

        return Json(true);

    }

    [HttpGet]
    public IActionResult GetCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        return Json(customer);
    }

    [HttpPost]
    public IActionResult Update(Customer model)
    {
        _context.Customers.Update(model);
        _context.SaveChanges();

        return Json(true);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}

