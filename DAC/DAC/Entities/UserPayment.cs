using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Entities
{
    public class UserPayment : BaseEntity
    {
        [MaxLength(16)]
        public string CardNumber { get; set; }

        public DateTime DateExp { get; set; }
        [MaxLength(3)]
        public string Csv { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public User User { get; set; }

        public UserPayment()
        {

        }
    }
}
