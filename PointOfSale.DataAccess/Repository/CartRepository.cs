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
    public class CartRepository  : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)  : base (db)
        {
            _db = db;
        }

        public double UpdatePrice(Cart cart, double price)
        {
            cart.Price = price;
            return cart.Count;
        }

        public double UpdateCount(Cart cart, double count)
        {
            cart.Count = count;
            return cart.Count;
        }
    }
}
