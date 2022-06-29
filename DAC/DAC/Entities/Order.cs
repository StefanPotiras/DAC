using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Entities
{
    public class Order : BaseEntity
    {
        public enum EOrderStatus { None, Processing, Shipped, Delivered, Transit }
        public EOrderStatus OrderStatus { get; set; }

        public User User { get; set; }
        public ICollection<Product> Products { get; set; }
        Order()
        {
            User = new User();
            Products = new HashSet<Product>();
        }
    }
}
