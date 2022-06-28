using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class UserPayment
    {
        public int IdUserPayment { get; set; }
        public string CardNumber { get; set; }
        public DateTime DateExp { get; set; }
        public string Csv { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
