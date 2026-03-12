using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinanceAccountingSystem.Models;
using FinanceAccountingSystem.Data;

public class InvoiceController : Controller
{
    private readonly FinanceDbContext _context;

    public InvoiceController(FinanceDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var invoices = _context.Invoices.ToList();
        ViewBag.SupplierList = _context.Suppliers.ToList();
        ViewBag.CustomerList = _context.Customers.ToList();
        return View(invoices);
    }

    public IActionResult Create()
    {
        ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name");
        ViewBag.CustomerList = new SelectList(_context.Customers, "CustomerId", "Name");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Invoice model)
    {
        if (ModelState.IsValid)
        {
            _context.Invoices.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var invoice = _context.Invoices.Find(id);
        if (invoice == null)
        {
            return NotFound();
        }

        ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name", invoice.SupplierId);
        ViewBag.CustomerList = new SelectList(_context.Customers, "CustomerId", "Name", invoice.CustomerId);

        return View(invoice);
    }

    [HttpPost]
    public IActionResult Edit(Invoice model)
    {
        if (ModelState.IsValid)
        {
            _context.Invoices.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name", model.SupplierId);
        ViewBag.CustomerList = new SelectList(_context.Customers, "CustomerId", "Name", model.CustomerId);
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var invoice = _context.Invoices.Find(id);
        if (invoice != null)
        {
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}