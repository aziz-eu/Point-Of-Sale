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

        public IActionResult Details(int id)
        {
            var ammountInWord = NumberToWords(20020);
            InvoiceVM = new InvoiceVM()
            {
                
                InvoiceHeader = _unitOfWork.InvoiceHeader.GetFirstOrDefault(x => x.Id == id),
                InvoiceDetails = _unitOfWork.InvoiceDetail.GetAll(x => x.InvoiceId == id, includeProperties : "Product")

            };
            return View(InvoiceVM);
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

        public IActionResult Edit(int id)
        {
            InvoiceVM = new InvoiceVM()
            {
                InvoiceHeader = _unitOfWork.InvoiceHeader.GetFirstOrDefault(x => x.Id == id),
                InvoiceDetails = _unitOfWork.InvoiceDetail.GetAll(x => x.InvoiceId == id, includeProperties: "Product")

            };
            return View(InvoiceVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(InvoiceVM InvoiceVM)
        {
            try
            {
               var invoiceHeader =  _unitOfWork.InvoiceHeader.GetFirstOrDefault(u=> u.Id == InvoiceVM.InvoiceHeader.Id);

                if (InvoiceVM.InvoiceHeader.UpdateDue > invoiceHeader.UnpaidAmount  || InvoiceVM.InvoiceHeader.UpdateDue <0)
                {
                    TempData["Success"] = "Please Enter Valid Ammount";
                    return RedirectToAction(nameof(Edit), new {id = invoiceHeader.Id});
                }
                invoiceHeader.PaidAmount = invoiceHeader.PaidAmount + InvoiceVM.InvoiceHeader.UpdateDue;
                invoiceHeader.UnpaidAmount = invoiceHeader.UnpaidAmount - InvoiceVM.InvoiceHeader.UpdateDue;

                if(invoiceHeader.UnpaidAmount< 1)
                {
                    invoiceHeader.PaymentSataus = SD.PaymentStatus_Paid;
                    invoiceHeader.UnpaidAmount = 0;
                }
             

                _unitOfWork.InvoiceHeader.Update(invoiceHeader);
                    _unitOfWork.Save();
                
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
           
            return View(InvoiceVM);

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


        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }

}
