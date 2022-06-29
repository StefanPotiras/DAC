using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAC.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
          
            Orders = new HashSet<Order>();
            UserPayments = new HashSet<UserPayment>();
            UserAddresses = new HashSet<UserAddres>();
        }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<UserAddres> UserAddresses { get; set; }
        public ICollection<UserPayment> UserPayments { get; set; }
        public Cart Cart { get; set; }

    }
}
