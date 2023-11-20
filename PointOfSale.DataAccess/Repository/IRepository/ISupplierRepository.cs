﻿using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface ISupplierRepository : IRepository <Supplier>
    {
        
        
        void Update(Supplier supplier);
    }
}
