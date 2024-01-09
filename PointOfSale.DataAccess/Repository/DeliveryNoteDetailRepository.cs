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
    public class DeliveryNoteDetailRepository : Repository<DeliveryNoteDetail>, IDeliveryNoteDetailRepository
    {
        public ApplicationDbContext _db;

        public DeliveryNoteDetailRepository(ApplicationDbContext db) : base (db)
        {
            
        }
        public void Update(DeliveryNoteDetail deliveryNoteDetail)
        {
            _db.DeliveryNoteDetails.Update(deliveryNoteDetail);
        }
    }
}
