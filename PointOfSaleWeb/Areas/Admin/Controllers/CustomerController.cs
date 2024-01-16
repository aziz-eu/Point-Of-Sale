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
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            Customer customer = new();

            if(id == 0 || id ==null)
            {
                // Create Category
                return View(customer);
            }
            else
            {
                // Edit 
                customer = _unitOfWork.Customer.GetFirstOrDefault(x => x.Id == id);
                return View(customer);
            }
        
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.Id == 0)
                    {
                        _unitOfWork.Customer.Add(customer);
                        TempData["success"] = "New Customer Add Sccessful";
                    }
                    else
                    {
                        _unitOfWork.Customer.Update(customer);
                        TempData["success"] = "Customer Update Sccessful";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }

                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Sorry! Try Again";
                return RedirectToAction(nameof(Index));

            }

           


        }



        #region API CALL

        public IActionResult GetAll() {
            var category = _unitOfWork.Customer.GetAll();

            return Json(new { data =category });
        
        }


        [HttpDelete]
        public IActionResult Delete(int? id) {

            try
            {
                var customer = _unitOfWork.Customer.GetFirstOrDefault(u => u.Id == id);
                if (customer == null)
                {
                    return Json(new { success = false, message = "Error while deleteing" });
                }

                _unitOfWork.Customer.Remove(customer);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successful" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error! First, delete the customer invoice and delivery note." });
            }

           
            
        
        
        }

        #endregion


    }
}
