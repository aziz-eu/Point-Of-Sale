using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Edit()
        {

            var companyObj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == 1);
            return View(companyObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            try
            {
                _unitOfWork.Company.Update(company);
                TempData["success"] = "Company Info Update Successful";
                _unitOfWork.Save();
                return RedirectToAction(nameof(Edit));
            }
            catch (Exception ex)
            {
                TempData["error"] = "Company Info Update Failed!";
                return RedirectToAction(nameof(Edit));
            }


        }

    }
}
