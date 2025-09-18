using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Color { get; set; }
        public DateTime Year { get; set; }
        public int MotorNumber { get; set; }
        public int ChassisNumber { get; set; }
    }
}
