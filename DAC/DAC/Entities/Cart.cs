using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Entities
{
    public class Cart : BaseEntity
    {
        //public User Id_user { get; set; }
        public ICollection<Product> Products { get; set; }
        public Cart()
        {
            Products = new HashSet<Product>();
        }
       public User User { get; set; }
    }
}
