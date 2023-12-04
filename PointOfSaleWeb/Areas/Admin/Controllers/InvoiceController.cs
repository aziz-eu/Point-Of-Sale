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
    public class InvoiceController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public InvoiceVM InvoiceVM { get; set; }
        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           
            return View();
        }


        public IActionResult Create()
        {

            InvoiceVM invoiceVM = new InvoiceVM()
            {
                Cart = new(),
                ProductList = _unitOfWork.Product.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }),
                ListCart = _unitOfWork.Cart.GetAll(includeProperties: "Product"),

                InvoiceHeader = new(),
                VatRate = _unitOfWork.VatRate.GetFirstOrDefault(u => u.Id == 1)
            };

            foreach(var item in invoiceVM.ListCart)
            { 
                invoiceVM.InvoiceHeader.SubTotal += item.Count*item.Price;
            }
            invoiceVM.VatPercentage = invoiceVM.VatRate.Vat;
            invoiceVM.InvoiceHeader.Vat = _unitOfWork.VatRate.CalculateVat(invoiceVM.InvoiceHeader.SubTotal, invoiceVM.VatRate);

            invoiceVM.InvoiceHeader.Total = invoiceVM.InvoiceHeader.SubTotal + invoiceVM.InvoiceHeader.Vat;

            return View(invoiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(InvoiceVM obj)
        {

            try
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                obj.Cart.ApplicationUserId = claim.Value;
                              

              Cart cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.ProdouctId == obj.Cart.ProdouctId);

                if(cart == null) {
                    _unitOfWork.Cart.Add(obj.Cart);
                    TempData["success"] = "New Item Add Sccessful";
                }

                else
                {
                    _unitOfWork.Cart.UpdateCount(cart, obj.Cart.Count);
                    _unitOfWork.Cart.UpdatePrice(cart, obj.Cart.Price);
                    TempData["success"] = "Item Update Sccessful";

                }

        

                _unitOfWork.Save();
                return RedirectToAction(nameof(Create));
            }

            catch (Exception ex)
            {
                return View(obj);
            }
           

        }

        public IActionResult Remove(int id)
        {
            var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Cart.Remove(cart);
            _unitOfWork.Save();           
            return RedirectToAction(nameof(Create));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Save (InvoiceVM InvoiceVM)
        {

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);


            InvoiceVM.ListCart = _unitOfWork.Cart.GetAll(includeProperties: "Product");
            InvoiceVM.VatRate = _unitOfWork.VatRate.GetFirstOrDefault(u => u.Id == 1);
            InvoiceVM.InvoiceHeader.PaymentSataus = SD.PaymentStatus_Due;
            InvoiceVM.InvoiceHeader.CreatedAt= DateTime.Now;
            InvoiceVM.InvoiceHeader.ApplicationUserId = claim.Value;

            foreach (var item in InvoiceVM.ListCart)
            {
                InvoiceVM.InvoiceHeader.SubTotal += item.Count * item.Price;
            }
            if (InvoiceVM.InvoiceHeader.SubTotal <= 0)
            {
                TempData["success"] = "Please Add Item First";
                return RedirectToAction(nameof(Create));
            }
            InvoiceVM.InvoiceHeader.Vat = _unitOfWork.VatRate.CalculateVat(InvoiceVM.InvoiceHeader.SubTotal, InvoiceVM.VatRate);
            InvoiceVM.InvoiceHeader.Total = InvoiceVM.InvoiceHeader.SubTotal + InvoiceVM.InvoiceHeader.Vat;
            InvoiceVM.InvoiceHeader.UnpaidAmount = InvoiceVM.InvoiceHeader.Total - InvoiceVM.InvoiceHeader.PaidAmount;
            if(InvoiceVM.InvoiceHeader.UnpaidAmount < 1)
            {
                InvoiceVM.InvoiceHeader.UnpaidAmount = 0;
                InvoiceVM.InvoiceHeader.PaymentSataus = SD.PaymentStatus_Paid;
            }

            if (InvoiceVM.InvoiceHeader.PaidAmount> InvoiceVM.InvoiceHeader.Total)
            {
                TempData["success"] = "Ivalid Amount";
                return RedirectToAction(nameof(Create));
            }

            _unitOfWork.InvoiceHeader.Add(InvoiceVM.InvoiceHeader);
            _unitOfWork.Save();

            foreach (var item in InvoiceVM.ListCart)
            {
                InvoiceDetail invoiceDetail = new()
                {
                    
                    InvoiceId = InvoiceVM.InvoiceHeader.Id,
                    ProductId = item.ProdouctId,
                    Count = item.Count,
                    Price = item.Price,
                    
                };
                _unitOfWork.InvoiceDetail.Add(invoiceDetail);
                _unitOfWork.Save();
            }
            _unitOfWork.Cart.RemoveRange(InvoiceVM.ListCart);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<InvoiceHeader> invoiceHeaders;

            invoiceHeaders = _unitOfWork.InvoiceHeader.GetAll(includeProperties: "ApplicationUser");

            return Json(new { data = invoiceHeaders });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
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

        }

        #endregion


    }
}
