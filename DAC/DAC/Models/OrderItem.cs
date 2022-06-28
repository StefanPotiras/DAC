using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class OrderItem
    {
        public int IdOrderItem { get; set; }
        public short Quantity { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
