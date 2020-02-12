using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.Database;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CustomerAccessMethods CustomerAccessMethods;
        private InvoiceAccessMethods InvoiceAccessMethods;

        public HomeController(ILogger<HomeController> logger)
        {            
            CustomerAccessMethods = new CustomerAccessMethods(new DatabaseContext());
            InvoiceAccessMethods = new InvoiceAccessMethods(new DatabaseContext());
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                TotalPaidInvoiceCount = InvoiceAccessMethods.GetSumOfInvoicesHeld(true),
                PaidInvoiceTotal = InvoiceAccessMethods.GetTotalFundsInvoiced(),
                Customers = CustomerAccessMethods.GetCustomers()
            };

            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
