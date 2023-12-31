using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {

        double UpdateCount(Cart cart, double count);
        double UpdatePrice(Cart cart, double price);

    }
}
