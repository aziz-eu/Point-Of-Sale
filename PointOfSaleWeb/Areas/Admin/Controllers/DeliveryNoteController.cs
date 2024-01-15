using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using PointOfSale.Models.ViewModels;
using PointOfSale.Utility;
using System.Security.Claims;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DeliveryNoteController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeliveryNoteVM DeliveryNoteVM { get; set; }
        public DeliveryNoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Details(int id)
        {
            DeliveryNoteVM = new DeliveryNoteVM()
            {
                DeliveryNoteHeader = _unitOfWork.DeliveryNoteHeader.GetFirstOrDefault(x => x.Id == id),
                DeliveryNoteDetails = _unitOfWork.DeliveryNoteDetail.GetAll(x => x.DeliveryNoteId == id, includeProperties: "Product"),
                Company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == 1),
                UnitsOfMeasurement = _unitOfWork.UnitOfMeasurment.GetAll()                
            };
            
            return View(DeliveryNoteVM);
        }


        public IActionResult Edit(int id)
        {
            DeliveryNoteVM = new DeliveryNoteVM()
            {
                DeliveryNoteHeader = _unitOfWork.DeliveryNoteHeader.GetFirstOrDefault(x => x.Id == id),
                DeliveryNoteDetails = _unitOfWork.DeliveryNoteDetail.GetAll(x => x.DeliveryNoteId == id, includeProperties: "Product")

            };
            return View(DeliveryNoteVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(DeliveryNoteVM DeliveryNoteVM)
        {
            try
            {
                var deliveryNoteHeader = _unitOfWork.DeliveryNoteHeader.GetFirstOrDefault(u => u.Id == DeliveryNoteVM.DeliveryNoteHeader.Id);

               
                if (DeliveryNoteVM.DeliveryNoteHeader.Name == null)
                {
                    deliveryNoteHeader.Name = deliveryNoteHeader.Name;
                }
                else
                {
                    deliveryNoteHeader.Name = DeliveryNoteVM.DeliveryNoteHeader.Name;
                }
                deliveryNoteHeader.CreatedAt = DeliveryNoteVM.DeliveryNoteHeader.CreatedAt;

                deliveryNoteHeader.PhoneNumbar = DeliveryNoteVM.DeliveryNoteHeader.PhoneNumbar;
                deliveryNoteHeader.Address = DeliveryNoteVM.DeliveryNoteHeader.Address;









                _unitOfWork.DeliveryNoteHeader.Update(deliveryNoteHeader);
                _unitOfWork.Save();

                TempData["success"] = "Delivery Note Update Successful";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Want Wrong!";
                return RedirectToAction(nameof(Edit), new { id = DeliveryNoteVM.DeliveryNoteHeader.Id });
            }



        }









        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<DeliveryNoteHeader> deliveryNoteHeaders;

            deliveryNoteHeaders = _unitOfWork.DeliveryNoteHeader.GetAll();

            return Json(new { data = deliveryNoteHeaders });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var deliveryNoteHeader = _unitOfWork.DeliveryNoteHeader.GetFirstOrDefault(u => u.Id == id);
         
            if (deliveryNoteHeader == null)
            {
                return Json(new { success = false, message = "Error Whiling Delete" });
            }
            else
            {
                _unitOfWork.DeliveryNoteHeader.Remove(deliveryNoteHeader);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successful" });
            }

        }
        //public IActionResult DeliveryNote(int id)
        //{
        //    InvoiceVM = new InvoiceVM()
        //    {
        //        InvoiceHeader = _unitOfWork.InvoiceHeader.GetFirstOrDefault(x => x.Id == id),
        //        InvoiceDetails = _unitOfWork.InvoiceDetail.GetAll(x => x.InvoiceId == id, includeProperties: "Product"),
        //        Company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == 1),
        //        UnitsOfMeasurement = _unitOfWork.UnitOfMeasurment.GetAll()
        //    };      
            
        //    return View(InvoiceVM);
        //}
        #endregion


    }

}
