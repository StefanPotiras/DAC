using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Entities
{
    public class UserAddres : BaseEntity
    {
        public string City { get; set; }
        [MaxLength(3)]
        public string PostalCod { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(10)]
        public string MobileNumber { get; set; }
        [MaxLength(50)]
        public string StreetName { get; set; }
        public byte StreetNr { get; set; }

        public User User;
    }
}
