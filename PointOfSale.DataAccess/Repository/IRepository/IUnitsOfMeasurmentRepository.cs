using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface IUnitsOfMeasurmentRepository : IRepository <UnitsOfMeasurement>
    {
        void Update(UnitsOfMeasurement unitsOfMeasurement);
    }
}
