using Core.Application;

namespace Application.DomainEvents
{
    internal sealed class CarDeleted : DomainEvent
    {
        public Guid CarId { get; set; }

        public CarDeleted(Guid carId)
        {
            CarId = carId;
        }
    }
}
