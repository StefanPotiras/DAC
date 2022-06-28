using System;
using System.Collections.Generic;

#nullable disable

namespace DAC.Models
{
    public partial class Cart
    {
        public int IdCart { get; set; }
        public int IdUser { get; set; }
        public int IdItemCart { get; set; }

        public virtual ItemCart IdItemCartNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
