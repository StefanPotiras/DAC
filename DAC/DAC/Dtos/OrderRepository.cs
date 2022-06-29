using DAC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Order GetOrderBy(Guid ID);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ShopContext context) : base(context) { }

        public Order GetOrderBy(Guid ID)
        {
            var result = GetRecords()
                   .Include(ind=>ind.Products)
                   .Where(c => c.Id == ID)
                   .FirstOrDefault();
            // -> rezultat concret
            return result;
        }
    }
}
