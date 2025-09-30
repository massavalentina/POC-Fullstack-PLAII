using Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.Enums;

namespace Application.DomainEvents
{
    internal sealed class CarCreated : DomainEvent
    {
        // Properties that will be shown in the event
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime Year { get; set; }
        public string  MotorNumber { get; set; }
        public string ChassisNumber { get; set; }

    }
}
