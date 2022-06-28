using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class Product
    {
        public Product()
        {
            ItemCarts = new HashSet<ItemCart>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ItemCart> ItemCarts { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
