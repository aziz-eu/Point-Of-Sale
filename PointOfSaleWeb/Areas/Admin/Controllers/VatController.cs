using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Edit(){

            var vatobj = _unitOfWork.VatRate.GetFirstOrDefault(x => x.Id == 1);
            return View(vatobj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VatRate vatRate) {
            try
            {
                _unitOfWork.VatRate.Update(vatRate);
                TempData["success"] = "Vat Update Successful";
                _unitOfWork.Save();
                return RedirectToAction(nameof(Edit));
            }
            catch (Exception ex)
            {
                TempData["error"] = "Vat Update Failed!";
                return RedirectToAction(nameof(Edit));
            }
          
          
        }
    }
}
