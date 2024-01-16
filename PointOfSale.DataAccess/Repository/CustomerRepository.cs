using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _db;
        public CustomerRepository( ApplicationDbContext db) : base(db )
        {
            _db = db;
        }
        public void Update(Customer customer)
        {
            _db.Customers.Update(customer);
        }

        public void UpdateCustomerNameToDeliveryNote(int id, string name)
        {
            var data = _db.DeliveryNoteHeaders.Where(u => u.CustomerId == id).ToList();

            foreach (var item in data)
            {
                item.Name = name;
                _db.DeliveryNoteHeaders.Update(item);
            }
        }

        public void UpdateCustomerNameToInvoices(int id, string name)
        {
           var data =  _db.InvoiceHeaders.Where(u => u.CustomerId == id).ToList();

            foreach (var item in data)
            {
                item.Name = name;
                _db.InvoiceHeaders.Update(item);
            }
        }
    }
}
