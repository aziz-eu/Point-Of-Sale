using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface ICustomerRepository : IRepository <Customer>
    {
        void Update(Customer customer);

        void UpdateCustomerNameToInvoices(int id, string name);
        void UpdateCustomerNameToDeliveryNote(int id, string name);

    }
}
