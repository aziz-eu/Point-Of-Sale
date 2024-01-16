using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using PointOfSale.Models.ViewModels;
using System.Diagnostics;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM HomeVM = new HomeVM();
            HomeVM.CalculateInvoiceAmount = _unitOfWork.Home.CalculateInvoiceAmount();
            HomeVM.CalculateDueInvoiceAmount = _unitOfWork.Home.CalculateDueInvoiceAmount();
            HomeVM.CountProduct = _unitOfWork.Home.CountProduct();
            HomeVM.CountDeliveryNote = _unitOfWork.Home.CountDeliveryNote();
            HomeVM.CountInvoice = _unitOfWork.Home.CountInvoice();
            HomeVM.CountDueInvoice = _unitOfWork.Home.CountDueInvoice();
            
            return View(HomeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}