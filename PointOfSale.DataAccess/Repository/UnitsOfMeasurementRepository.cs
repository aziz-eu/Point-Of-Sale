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
    public class UnitsOfMeasurementRepository : Repository<UnitsOfMeasurement>, IUnitsOfMeasurmentRepository
    {
        private ApplicationDbContext _db;

        public UnitsOfMeasurementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UnitsOfMeasurement unitsOfMeasurement)
        {
            _db.UnitsOfMeasurement.Update(unitsOfMeasurement);
        }
    }
}
