using DAC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DAC.Entities.Order;

namespace DAC.Dtos
{
    public class OrderDtos
    {
        public EOrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid Id { get; set; }
        public ICollection<ProductsDto> Products { get; set; }
        public DateTime? DeletedAt { get; set; }
        public OrderDtos()
        {
            TotalPrice = 0;
           
            Products = new HashSet<ProductsDto>();
        }
    }
}
