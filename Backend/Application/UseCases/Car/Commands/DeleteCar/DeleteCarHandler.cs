
using Application.Constants;
using Application.DomainEvents;
using Application.Exceptions;
using Application.Repositories;
using Core.Application;
using MediatR;

namespace Application.UseCases.Car.Commands.DeleteCar
{
    internal sealed class DeleteCarHandler(ICommandQueryBus domainBus, ICarRepository carRepository)
        : IRequestCommandHandler<DeleteCarCommand, Unit>
    {
        private readonly ICommandQueryBus _domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        private readonly ICarRepository _context = carRepository ?? throw new ArgumentNullException(nameof(carRepository));

        public Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _context.Remove(request.CarId);

                _domainBus.Publish(new CarDeleted(request.CarId), cancellationToken);

                return Unit.Task;
            }
            catch (Exception ex)
            {
                throw new BussinessException(ApplicationConstants.PROCESS_EXECUTION_EXCEPTION, ex.InnerException);
            }
        }

    }
}