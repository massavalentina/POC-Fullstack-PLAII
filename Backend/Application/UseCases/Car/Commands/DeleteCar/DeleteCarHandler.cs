
using Application.Constants;
using Application.DomainEvents;
using Application.Exceptions;
using Application.Repositories;
using Application.UseCases.Car.Commands.DeleteCar;
using Core.Application;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Car.Commands.DeleteCar
{
    internal sealed class DeleteCarHandler : IRequestCommandHandler<DeleteCarCommand, Unit>
    {
        private readonly ICommandQueryBus _domainBus;
        private readonly ICarRepository _carRepository;
        private readonly ILogger<DeleteCarHandler> _logger;

        public DeleteCarHandler(
            ICommandQueryBus domainBus,
            ICarRepository carRepository,
            ILogger<DeleteCarHandler> logger)
        {
            _domainBus = domainBus;
            _carRepository = carRepository;
            _logger = logger;
        }


        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting car with ID: {CarId}", request.CarId);

                bool wasDeleted = await _carRepository.RemoveAsync(request.CarId, cancellationToken);

                if (!wasDeleted)
                {
                    _logger.LogWarning("Car with ID {CarId} not found", request.CarId);
                    throw new EntityDoesNotExistException(ApplicationConstants.ENTITY_DOESNOT_EXIST_EXCEPTION);
                }

                await _domainBus.Publish(new CarDeleted(request.CarId), cancellationToken);
                _logger.LogInformation("Car with ID {CarId} deleted successfully", request.CarId);

                return Unit.Value;
            }
            catch (EntityDoesNotExistException) 
            {
                throw; 
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error deleting car with ID: {CarId}", request.CarId);
                throw new BussinessException(ApplicationConstants.PROCESS_EXECUTION_EXCEPTION, ex);
            }
        }
    }
}