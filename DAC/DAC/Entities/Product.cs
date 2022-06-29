using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int NumberOfItems { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
       public Product()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }
    }
}
