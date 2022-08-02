using BulkyBookDataAccess.Repository;
using BulkyBookModels;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;

        public CompanyController(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            Company company = new();
            if (Id == null || Id == 0)
            {
                return View(company);
            }
            else
            {
                company = _uniOfWork.Company.GetFirstOrDefault(x => x.Id == Id);
                return View(company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (obj.Id == 0)
            {
                _uniOfWork.Company.Add(obj);
                TempData["success"] = "Company Created";
            }
            else
            {
                _uniOfWork.Company.Update(obj);
                TempData["success"] = "Company Updated";
            }
            _uniOfWork.Save();
                return RedirectToAction("Index");
        }

        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _uniOfWork.Company.GetAll();
            return Json(new { data = CompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _uniOfWork.Company.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _uniOfWork.Company.Remove(obj);
            _uniOfWork.Save();
            return Json(new { success = true, message = "Deleted succesful" });
        }

        #endregion
    }
}
