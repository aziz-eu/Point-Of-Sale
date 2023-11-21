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
        void Save();
    }
}
