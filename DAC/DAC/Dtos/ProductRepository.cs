using DAC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {     
        Product GetProductBy(Guid ID);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context) { }

        public Product GetProductBy(Guid ID)
        {
            var result = GetRecords()

                    // FULL JOIN Notifications as n on n.UserId = u.Id


                    // WHERE u.Email = @email 
                    // IQueryable pana aici -> rezultatul nu e concret
                    .Where(u => u.Id == ID)

                    .FirstOrDefault();
            // -> rezultat concret
            return result;
        }

        
     
    }
}
