using Core.Application;

namespace Application.DomainEvents
{
    internal sealed class CarDeleted : DomainEvent
    {
        public int CarId { get; set; }

        public CarDeleted(int id)
        {
             CarId = id;
        }
    }
}
