using DAC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{
    public class CartDtos:BaseEntity
    {
        public ICollection<ProductsDto> Products { get; set; }
        public decimal TotalPrice;
        public CartDtos()
        {
            Products = new HashSet<ProductsDto>();
            TotalPrice = 0;
        }
    }
}
