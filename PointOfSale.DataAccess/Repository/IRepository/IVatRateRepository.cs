using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface IVatRateRepository : IRepository<VatRate>
    {
        void Update(VatRate vatRate);

        double CalculateVat(double number, VatRate vat);
    }
}
