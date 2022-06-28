using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public string OrderStatus { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
