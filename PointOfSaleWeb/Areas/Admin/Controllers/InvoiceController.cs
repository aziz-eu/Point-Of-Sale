using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models.ViewModels;

namespace PointOfSaleWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
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
            };

            return View(invoiceVM);
        }
    }
}
