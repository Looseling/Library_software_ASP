using BulkyBook.Models;
using BulkyBookDataAccess.Repository;
using BulkyBookModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
              u => new SelectListItem
              {
                  Text = u.Name,
                  Value = u.Id.ToString(),
              }),
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var wwwpath = _hostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwpath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwpath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }


                    using (var fileStream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\Images\Products\" + filename + extension;
                }



                if (obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }


                _unitOfWork.Save();
                TempData["success"] = "Product Added succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var wwwpath = _hostEnvironment.WebRootPath;
            if (obj.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(wwwpath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted succesful" });
        }

        #endregion
    }
}
