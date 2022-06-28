using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class ItemCart
    {
        public ItemCart()
        {
            Carts = new HashSet<Cart>();
        }

        public int IdCartItem { get; set; }
        public short Quantity { get; set; }
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
