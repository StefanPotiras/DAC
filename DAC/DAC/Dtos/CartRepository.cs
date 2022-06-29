using DAC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Repositories
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Cart GetCartBy(Guid? ID);
    }

    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(ShopContext context) : base(context) { }

        public Cart GetCartBy(Guid? ID)
        {
            var result = GetRecords()

                   // FULL JOIN Notifications as n on n.UserId = u.Id


                   // WHERE u.Email = @email 
                   // IQueryable pana aici -> rezultatul nu e concret
                   .Where(c => c.User.Id == ID)

                   .FirstOrDefault();
            // -> rezultat concret
            return result;
        }
    }
}
