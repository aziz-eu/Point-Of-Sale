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
    public class DeliveryNoteRepository : Repository<DeliveryNote>, IDelivaryNoteRepository
    {
        public ApplicationDbContext _db;

        public DeliveryNoteRepository(ApplicationDbContext db) : base (db)
        {
            
        }
       

        public void Update(DeliveryNote deliveryNote)
        {
            _db.deliveryNotes.Update(deliveryNote);
        }
    }
}
