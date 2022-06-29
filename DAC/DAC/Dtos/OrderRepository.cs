using DAC.Entities;
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
                   .Where(c => c.User.Id == ID)
                   .FirstOrDefault();
            // -> rezultat concret
            return result;
        }
    }
}
