using DAC.Dtos;
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
        public ICollection<OrderDtos> GetOrderByUser(Guid? Id);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ShopContext context) : base(context) { }

        public Order GetOrderBy(Guid ID)
        {
            var result = GetRecords()
                   .Include(ind => ind.Products)
                   .Where(c => c.Id == ID)
                   .FirstOrDefault();
            return result;
        }

        public ICollection<OrderDtos> GetOrderByUser(Guid? Id)
        {
            var result = GetRecords()
                   .Include(ind => ind.Products)
                   .Where(c => c.User.Id == Id)
                   .ToList().Select(index => new OrderDtos
                   {
                       Products = index.Products.Select(prod => new ProductsDto
                       {
                           Description = prod.Description,
                           Price = prod.Price,
                           Name = prod.Name,
                           NumberOfItems = prod.NumberOfItems,

                       }).ToList(),

                       OrderStatus = index.OrderStatus,
                       Id = index.Id,
                       TotalPrice = index.Products.Sum(item => item.Price),
                       // DeletedAt=index.DeletedAt


                   }).ToList(); ;


            return result;
        }
    }
}
