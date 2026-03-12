using FinanceAccountingSystem.Data;
using FinanceAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class SupplierController : Controller
{
    private readonly FinanceDbContext _context;

    public SupplierController(FinanceDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var suppliers = _context.Suppliers.ToList();
        ViewBag.AddressList = _context.Addresses.ToList();
        return View(suppliers);
    }

    [HttpPost]
    public IActionResult Create(Supplier model)
    {
        if (ModelState.IsValid)
        {
            _context.Suppliers.Add(model);
            _context.SaveChanges();
        }

        return Json(true);
    }

    [HttpGet]
    public IActionResult GetSupplier(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        return Json(supplier);
    }

    [HttpPost]
    public IActionResult Update(Supplier model)
    {
        _context.Suppliers.Update(model);
        _context.SaveChanges();
        return Json(true);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}