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
    public class DeliveryNoteHeaderRepository : Repository<DeliveryNoteHeader>, IDeliveryNoteHeaderRepository
    {
        public ApplicationDbContext _db;
        public DeliveryNoteHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DeliveryNoteHeader deliveryNoteHeader)
        {
            _db.DeliveryNoteHeaders.Update(deliveryNoteHeader);
        }

        public int LastNoteId()
        {
           var noteId =  _db.DeliveryNoteHeaders.OrderByDescending(u => u.Id).FirstOrDefault().Id;
            if(noteId == 0)
            {
                return 1;
            }
            return noteId;
        }
    }
}
