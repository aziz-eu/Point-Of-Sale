using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using PointOfSale.Models.ViewModels;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()          
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }),
                SupplierList = _unitOfWork.Supplier.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Company,
                        Value = u.Id.ToString(),
                    }),
                UnitsList = _unitOfWork.UnitOfMeasurment.GetAll().Select(
                    u=> new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    })
                
            };


            if(id == null || id == 0) {

            //create 
            return View(productVM);

            }
            else
            {
              productVM.Product  =  _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);

            }
           

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "New Product Add Sccessful";
                }

                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["success"] = "Update Product Sccessful";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(obj);

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string? status) {


            var product = _unitOfWork.Product.GetAll(includeProperties : "Category,Supplier,UnitsOfMeasurement");

            switch (status)
            {
                case "outOfStock":
                    product = product.Where(u => u.Quantity == 0);
                    break;
                case "lowStock":
                    product = product.Where(u => u.Quantity < 25  && u.Quantity > 0);
                    break;
                default:
                    break;
            }
            

            return Json(new { data = product });
        }

        [HttpDelete] 
        public IActionResult Delete(int? id)
        {
            try
            {
                var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Error Whiling Delete" });
                }
                else
                {
                    _unitOfWork.Product.Remove(product);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Delete Successful" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = "Error Whiling Delete! Some Invoice Has the item!" });
            }
        
          
        
        }

        #endregion

    }
}
