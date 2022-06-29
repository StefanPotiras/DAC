﻿using DAC.Dtos;
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
                   .ToList();


            List<OrderDtos> orderDtos= new List<OrderDtos>();

            var finalTest = result.Select(index => new OrderDtos
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
                TotalPrice = index.Products.Sum(item=>item.Price)

            }).ToList() ; 


            //foreach(var index in result)
            //{
            //   List <ProductsDto> products=new List<ProductsDto>();
            //    decimal totalPrice = 0;
            //    foreach(var indexP in index.Products)
            //    {
            //        products.Add(new ProductsDto()
            //        {
            //            Name=indexP.Name,
            //            Description=indexP.Description,
            //            Price=indexP.Price,
            //            NumberOfItems=indexP.NumberOfItems
            //        });
            //        totalPrice += indexP.Price;
            //    }
            //    orderDtos.Add(new OrderDtos()
            //    {
            //        OrderStatus=index.OrderStatus,
            //        Id=index.Id,
            //        Products= products,
            //        TotalPrice=totalPrice,                             
            //    });
            //}
                          
            return finalTest;
        }
    }
}
