using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookDataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _db.Product.GetAll(includeProperties:"Category,CoverType");

            return View(productList);
        }


        public IActionResult Details(int id)
        { 

            ShoppingCardVM cardObj = new()
            {
                count = 1,
                product = _db.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType")
            };

            return View(cardObj);
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