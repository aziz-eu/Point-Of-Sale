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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            Category category = new();

            if(id == 0 || id ==null)
            {
                // Create Category
                return View(category);
            }
            else
            {
                // Edit 
                category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
                return View(category);
            }
        
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {

           if(ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                    TempData["success"] = "New Category Add Sccessful";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData["success"] = "Category Update Sccessful";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

           return View(category);


        }



        #region API CALL

        public IActionResult GetAll() {
            var category = _unitOfWork.Category.GetAll();

            return Json(new { data =category });
        
        }


        [HttpDelete]
        public IActionResult Delete(int? id) {

            try
            {
                var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Error while deleteing" });
                }

                _unitOfWork.Category.Remove(category);
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
