using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DAC.Entities.Order;

namespace DAC.Dtos
{
    public class StatusDtos
    {
        public EOrderStatus OrderStatus { get; set; }
    }
}
