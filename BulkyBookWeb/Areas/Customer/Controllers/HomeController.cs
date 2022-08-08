using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookDataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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


        public IActionResult Details(int productid)
        { 

            ShoppingCardVM cardObj = new()
            {
                count = 1,
                ProductId = productid,
                product = _db.Product.GetFirstOrDefault(u => u.Id == productid, includeProperties: "Category,CoverType")
            };

            return View(cardObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCardVM shoppingCard)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimExt = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCard.ApplicationIdentityUserId = claimExt.Value;

            var productInDb = _db.ShoppingCard.GetFirstOrDefault(u => u.ProductId == shoppingCard.ProductId && u.ApplicationIdentityUserId == claimExt.Value);

            if (productInDb == null)
            {
                _db.ShoppingCard.Add(shoppingCard);
            }
            else
            {
                _db.ShoppingCard.IncrementCount(productInDb,shoppingCard.count);
            }
            _db.Save();






            return RedirectToAction(nameof(Index));
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