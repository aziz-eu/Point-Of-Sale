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
            InvoiceVM = new InvoiceVM()
            {
                InvoiceHeader = _unitOfWork.InvoiceHeader.GetFirstOrDefault(x => x.Id == id),
                InvoiceDetails = _unitOfWork.InvoiceDetail.GetAll(x => x.InvoiceId == id, includeProperties: "Product"),
                Company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == 1),
                UnitsOfMeasurement = _unitOfWork.UnitOfMeasurment.GetAll()                
            };
            if (InvoiceVM.InvoiceHeader.CustomerId != null)
            {
                InvoiceVM.TotalDueAmount = _unitOfWork.InvoiceHeader.CalculateDue(InvoiceVM.InvoiceHeader.CustomerId);
            }
            var totalAmmount = Convert.ToInt32(Math.Floor(InvoiceVM.InvoiceHeader.Total));
            InvoiceVM.AmountInWord = NumberToWords(totalAmmount);
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
                UnitsOfMeasurement = _unitOfWork.UnitOfMeasurment.GetAll(),
                InvoiceHeader = new(),
                VatRate = _unitOfWork.VatRate.GetFirstOrDefault(u => u.Id == 1),
                CustomersList = _unitOfWork.Customer.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.Id.ToString(),
                   })
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
       
        public IActionResult Create(InvoiceVM obj)
        {

            try
            {
                                              

              Cart cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.ProdouctId == obj.Cart.ProdouctId);
                var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == obj.Cart.ProdouctId);
                if(product.Quantity< obj.Cart.Count)
                {
                    TempData["error"] = "Please Update Your Product Quantity";
                    return RedirectToAction(nameof(Create));

                }

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
            try
            {

                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

              


                InvoiceVM.ListCart = _unitOfWork.Cart.GetAll(includeProperties: "Product");
                
                InvoiceVM.VatRate = _unitOfWork.VatRate.GetFirstOrDefault(u => u.Id == 1);
                InvoiceVM.InvoiceHeader.CreatedAt = DateTime.Now;


                foreach (var item in InvoiceVM.ListCart)
                {
                    InvoiceVM.InvoiceHeader.SubTotal += item.Count * item.Price;
                }
                if (InvoiceVM.InvoiceHeader.SubTotal <= 0)
                {
                    TempData["error"] = "Please Add Item First";
                    return RedirectToAction(nameof(Create));
                }
                InvoiceVM.InvoiceHeader.Vat = _unitOfWork.VatRate.CalculateVat(InvoiceVM.InvoiceHeader.SubTotal, InvoiceVM.VatRate);
                InvoiceVM.InvoiceHeader.Total = InvoiceVM.InvoiceHeader.SubTotal + InvoiceVM.InvoiceHeader.Vat;
                InvoiceVM.InvoiceHeader.UnpaidAmount = InvoiceVM.InvoiceHeader.Total - InvoiceVM.InvoiceHeader.PaidAmount;
                if (InvoiceVM.InvoiceHeader.UnpaidAmount < 1)
                {
                    InvoiceVM.InvoiceHeader.UnpaidAmount = 0;
                    InvoiceVM.InvoiceHeader.PaymentSataus = SD.PaymentStatus_Paid;
                }
                else
                {
                    InvoiceVM.InvoiceHeader.PaymentSataus = SD.PaymentStatus_Due;
                }

                if (InvoiceVM.InvoiceHeader.PaidAmount > InvoiceVM.InvoiceHeader.Total)
                {
                    TempData["success"] = "Ivalid Amount";
                    return RedirectToAction(nameof(Create));
                }

                if (InvoiceVM.InvoiceHeader.CustomerId != null)
                {
                    var customerInfo = _unitOfWork.Customer.GetFirstOrDefault(u => u.Id == InvoiceVM.InvoiceHeader.CustomerId);

                    InvoiceVM.InvoiceHeader.Name = customerInfo.Name;
                    InvoiceVM.InvoiceHeader.PhoneNumbar = customerInfo.PhoneNumber;
                    InvoiceVM.InvoiceHeader.Email = customerInfo.Email;                    
                   InvoiceVM.InvoiceHeader.CustTrn = customerInfo.CustTrn;
                    InvoiceVM.InvoiceHeader.CustomerId = InvoiceVM.InvoiceHeader.CustomerId;
                    if(InvoiceVM.InvoiceHeader.Loaction == null)
                    {
                        InvoiceVM.InvoiceHeader.Address = customerInfo.Address;
                    }
                    else
                    {
                        InvoiceVM.InvoiceHeader.Address = InvoiceVM.InvoiceHeader.Loaction;
                    }
                    _unitOfWork.InvoiceHeader.Add(InvoiceVM.InvoiceHeader);
                    _unitOfWork.Save();


                }
                else
                {

                    _unitOfWork.InvoiceHeader.Add(InvoiceVM.InvoiceHeader);
                    _unitOfWork.Save();
                }

               

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
                    var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == item.ProdouctId);
                    if (product != null)
                    {
                        product.Quantity = product.Quantity - item.Count;
                        _unitOfWork.Save();

                    }
                }


                ////Delivery Note 
                
                if (InvoiceVM.Cart.isGenrateDeliveryNote)
                {
                    DeliveryNoteHeader deliveryNote = new()
                    {
                        Name = InvoiceVM.InvoiceHeader.Name,
                        PhoneNumbar = InvoiceVM.InvoiceHeader.PhoneNumbar,
                        Email = InvoiceVM.InvoiceHeader.Email,
                        Address = InvoiceVM.InvoiceHeader.Address,
                        CustTrn = InvoiceVM.InvoiceHeader.CustTrn,
                        CreatedAt = DateTime.Now,
                        CustomerId = InvoiceVM.InvoiceHeader.CustomerId



                    };
                    _unitOfWork.DeliveryNoteHeader.Add(deliveryNote);

                    _unitOfWork.Save();
                    

                    foreach (var item in InvoiceVM.ListCart)
                    {

                        DeliveryNoteDetail deliveryNoteDetail = new()
                        {

                            DeliveryNoteId = _unitOfWork.DeliveryNoteHeader.LastNoteId(),
                            ProductId = item.ProdouctId,
                            Count = item.Count,
                            

                            
                        };

                        _unitOfWork.DeliveryNoteDetail.Add(deliveryNoteDetail);
                        _unitOfWork.Save();
                   
                    }
                    _unitOfWork.Cart.RemoveRange(InvoiceVM.ListCart);
                    _unitOfWork.Save();
                    TempData["success"] = "Delivery Note & Invoice Created Successful";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Cart.RemoveRange(InvoiceVM.ListCart);
                    _unitOfWork.Save();
                    TempData["success"] = "Invoice Create Successful";
                    return RedirectToAction("Index");
                }

               

            }
            catch (Exception ex)
            {
                TempData["error"] = "Something want wrong!";
                return RedirectToAction(nameof(Create));
            }

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
                    TempData["error"] = "Please Enter Valid Amount";
                    return RedirectToAction(nameof(Edit), new {id = invoiceHeader.Id});
                }
                invoiceHeader.PaidAmount = invoiceHeader.PaidAmount + InvoiceVM.InvoiceHeader.UpdateDue;
                invoiceHeader.UnpaidAmount = invoiceHeader.UnpaidAmount - InvoiceVM.InvoiceHeader.UpdateDue;

                if(invoiceHeader.UnpaidAmount< 1)
                {
                    invoiceHeader.PaymentSataus = SD.PaymentStatus_Paid;
                    invoiceHeader.UnpaidAmount = 0;
                    
                }
                if (InvoiceVM.InvoiceHeader.Name == null)
                {
                    invoiceHeader.Name = invoiceHeader.Name;
                }
                else
                {
                    invoiceHeader.Name = InvoiceVM.InvoiceHeader.Name;
                }
                invoiceHeader.CreatedAt = InvoiceVM.InvoiceHeader.CreatedAt;
               
                invoiceHeader.PhoneNumbar = InvoiceVM.InvoiceHeader.PhoneNumbar;
                invoiceHeader.Address = InvoiceVM.InvoiceHeader.Address;








               
                _unitOfWork.InvoiceHeader.Update(invoiceHeader);
                _unitOfWork.Save();

                TempData["success"] = "Invoice info Update Successful";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Want Wrong!";
                return RedirectToAction(nameof(Edit),  new { id = InvoiceVM.InvoiceHeader.Id });
            }
           
           

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

            var inovceHeader = _unitOfWork.InvoiceHeader.GetFirstOrDefault(u => u.Id == id);
         
            if (inovceHeader == null)
            {
                return Json(new { success = false, message = "Error Whiling Delete" });
            }

            if(inovceHeader.UnpaidAmount != 0)
            {
                return Json(new { success = false, message = "Sorry! The invoice due isn't paid." });
            }
            else
            {
                _unitOfWork.InvoiceHeader.Remove(inovceHeader);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successful" });
            }

        }
 
        #endregion


        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }
    }

}
