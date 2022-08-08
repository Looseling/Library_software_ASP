using BulkyBookDataAccess.Repository;
using BulkyBookModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCardVM2 shoppingCartVM;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimExt = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCardVM2
            {
                ListCart = _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationIdentityUserId == claimExt.Value,includeProperties: "product")
            };

            
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = getPrice(cart.count, cart.product.Price, cart.product.Price50, cart.product.Price100);

                shoppingCartVM.CartTotal += (cart.Price * cart.count);
            }

            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claimExt = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //shoppingCartVM = new ShoppingCardVM2
            //{
            //    ListCart = _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationIdentityUserId == claimExt.Value, includeProperties: "product")
            //};


            //foreach (var cart in shoppingCartVM.ListCart)
            //{
            //    cart.Price = getPrice(cart.count, cart.product.Price, cart.product.Price50, cart.product.Price100);

            //    shoppingCartVM.CartTotal += (cart.Price * cart.count);
            //}

            //return View(shoppingCartVM);
            return View();
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == cartId);

            _unitOfWork.ShoppingCard.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            
            var cart = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == cartId);


            if (cart.count <= 1)
            {
                _unitOfWork.ShoppingCard.Remove(cart);
            }
            else
            { 
            _unitOfWork.ShoppingCard.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCard.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private double getPrice(int quantity, double Price, double Price50, double Price100)
        {
            if (quantity < 50)
            {
                return Price;
            }
            else if(quantity < 100)
            {
                return Price50;
            }
            else
            {
                return Price100;
            }



        }


    }
}
