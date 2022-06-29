using DAC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{
    public class ProductsDto
    {
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int NumberOfItems { get; set; }   
        public ProductsDto()
        {
           
        }
    }
}
