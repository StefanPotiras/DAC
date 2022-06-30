using DAC.Dtos;
using DAC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Repositories
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Cart GetCartBy(Guid? ID);
        CartDtos GetCartById(Guid? Id);
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
                   .Include(u => u.Products)
                   .Include(u => u.User)                 
                   .Where(c => c.User.Id == ID)

                   .FirstOrDefault();
            // -> rezultat concret
            return result;
        }

        public CartDtos GetCartById(Guid? Id)
        {
            var result = GetRecords()
                   .Include(ind => ind.Products)              
                   .Where(c => c.User.Id == Id)
                   .FirstOrDefault();



            List<ProductsDto> products = new List<ProductsDto>();
            decimal totalPrice = 0;
            foreach (var indexP in result.Products)
            { if (indexP.DeletedAt == null)
                {
                    products.Add(new ProductsDto()
                    {
                        Name = indexP.Name,
                        Description = indexP.Description,
                        Price = indexP.Price,
                        NumberOfItems = indexP.NumberOfItems
                    });
                    totalPrice += indexP.Price;
                }
            }
                     
            CartDtos cart = new CartDtos() {
                Products=products,
                Id=result.Id,
                TotalPrice=totalPrice
            };
              
                       
            return cart;
        }
    }
}
