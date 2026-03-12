using System.Diagnostics;
using FinanceAccountingSystem.Data;
using FinanceAccountingSystem.Models;
using FinanceAccountingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAccountingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinanceDbContext _context;

        public HomeController(ILogger<HomeController> logger, FinanceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CustomerCount = _context.Customers.Count();
            ViewBag.SupplierCount = _context.Suppliers.Count();
            ViewBag.InvoiceCount = _context.Invoices.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
