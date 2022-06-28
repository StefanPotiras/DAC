using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostalCod { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string StreetName { get; set; }
        public byte StreetNr { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
