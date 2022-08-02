using BulkyBook.Models;
using BulkyBookDataAccess.Repository;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _unitOfWork.Category.GetAll();
            return View(CategoryList);
        }
        //GET
        public IActionResult Create()
        { 
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DiaplyOrder.ToString())
            {
                ModelState.AddModelError("MatchError", "DisplayOrder and Name Can't be equal");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var dbCategoryChange = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if( dbCategoryChange == null)
            {
                return NotFound();
            }

            return View(dbCategoryChange);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DiaplyOrder.ToString())
            {
                ModelState.AddModelError("MatchError", "DisplayOrder and Name Can't be equal");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category edited succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var dbCategoryChange = _db.Categories.Find(id);
            var dbCategoryChange = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (dbCategoryChange == null)
            {
                return NotFound();
            }

            return View(dbCategoryChange);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
