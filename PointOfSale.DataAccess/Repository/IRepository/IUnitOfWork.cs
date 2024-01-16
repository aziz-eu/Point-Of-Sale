using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        IUnitsOfMeasurmentRepository UnitOfMeasurment { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IVatRateRepository VatRate { get; }
        IInvoiceDetailRepository InvoiceDetail { get; }
        IInvoiceHeaderRepository InvoiceHeader { get; }
        IDeliveryNoteHeaderRepository DeliveryNoteHeader { get; }
        IDeliveryNoteDetailRepository DeliveryNoteDetail { get; }
        ICompanyRepository Company { get; }
        ICustomerRepository Customer { get; }
        IHomeRepository Home {  get; }
        void Save();
    }
}
