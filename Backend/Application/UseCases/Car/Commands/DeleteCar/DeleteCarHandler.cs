
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
                _logger.LogInformation("❶ Starting delete process for CarId: {CarId}", request.CarId);

                _logger.LogInformation("❷ Calling RemoveAsync...");
                bool wasDeleted = await _carRepository.RemoveAsync(request.CarId, cancellationToken);
                _logger.LogInformation("❸ RemoveAsync completed. Result: {WasDeleted}", wasDeleted);

                if (!wasDeleted)
                {
                    _logger.LogWarning("❹ Car with ID {CarId} not found", request.CarId);
                    throw new NotFoundException($"Car with ID {request.CarId} not found");
                }

                _logger.LogInformation("❺ Publishing CarDeleted event...");
                await _domainBus.Publish(new CarDeleted(request.CarId), cancellationToken);

                _logger.LogInformation("❻ Car with ID {CarId} deleted successfully", request.CarId);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❼ ERROR in DeleteCarHandler for CarId: {CarId}. Exception: {ExceptionType} - {ExceptionMessage}",
                    request.CarId, ex.GetType().Name, ex.Message);
                throw new BussinessException(ApplicationConstants.PROCESS_EXECUTION_EXCEPTION, ex);
            }
        }
    }
}