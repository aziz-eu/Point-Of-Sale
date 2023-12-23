using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area ("Admin")]
    [Authorize]
    public class UnitOfMeasurementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfMeasurementController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            UnitsOfMeasurement unitsOfMeasurement = new();

            if(id == 0 || id ==null)
            {
                // Create Category
                return View(unitsOfMeasurement);
            }
            else
            {
                // Edit 
                unitsOfMeasurement = _unitOfWork.UnitOfMeasurment.GetFirstOrDefault(x => x.Id == id);
                return View(unitsOfMeasurement);
            }
        
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UnitsOfMeasurement unitsOfMeasurement)
        {

           if(ModelState.IsValid)
            {
                if (unitsOfMeasurement.Id == 0)
                {
                    _unitOfWork.UnitOfMeasurment.Add(unitsOfMeasurement);
                    TempData["success"] = "New Unit Add Sccessful";
                }
                else
                {
                    _unitOfWork.UnitOfMeasurment.Update(unitsOfMeasurement);
                    TempData["success"] = "Unit Update Successful";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

           return View(unitsOfMeasurement);


        }



        #region API CALL

        public IActionResult GetAll() {
            var units = _unitOfWork.UnitOfMeasurment.GetAll();

            return Json(new { data = units });
        
        }


        [HttpDelete]
        public IActionResult Delete(int? id) {

            try
            {
                var units = _unitOfWork.UnitOfMeasurment.GetFirstOrDefault(u => u.Id == id);
                if (units == null)
                {
                    return Json(new { success = false, message = "Error while deleteing" });
                }

                _unitOfWork.UnitOfMeasurment.Remove(units);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successful" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "First Delete the Releted Products" });

            }
           
            
        
        
        }

        #endregion


    }
}
