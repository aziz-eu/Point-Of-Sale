using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierController(IUnitOfWork unitOfWork)
        {
                
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            Supplier supplier = new();

            if(id == 0 || id == null)
            {
                //Create

                return View(supplier);
            }
            else
            {
               supplier = _unitOfWork.Supplier.GetFirstOrDefault(x => x.Id == id);
                return View(supplier);
            }
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Supplier supplier)
        {
            if (ModelState.IsValid) {
                if (supplier.Id == 0)  {

                    _unitOfWork.Supplier.Add(supplier);
                    TempData["success"] = "Supplier Added Successfully";

                }
            else
                {
                    _unitOfWork.Supplier.Update(supplier);
                    TempData["success"] = "Supplier Info Update Successfully";


                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);

        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var suppliers = _unitOfWork.Supplier.GetAll();
            return Json(new { data = suppliers });
        }

        [HttpDelete]

        public IActionResult Delete(int? id) { 
        var suppier = _unitOfWork.Supplier.GetFirstOrDefault(u => u.Id == id);
            if (suppier == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.Supplier.Remove(suppier);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        
        }

        #endregion

    }



}
