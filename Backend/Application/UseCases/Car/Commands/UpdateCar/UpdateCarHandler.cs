using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Microsoft.Extensions.Logging;



namespace Application.UseCases.Car.Commands.UpdateCar
{
    public class UpdateCarHandler : IRequestCommandHandler<UpdateCarCommand, CarDto>
    {
        private readonly ICommandQueryBus _domainBus;
        private readonly ICarRepository _carRepository;
        private readonly ILogger<UpdateCarHandler> _logger;

        public UpdateCarHandler(
            ICommandQueryBus domainBus,
            ICarRepository carRepository,
            ILogger<UpdateCarHandler> logger)
        {
            _domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("❶ Starting update process for CarId: {CarId}", request.Id);

                // Usamos FindOneAsync del repositorio, que ya maneja la búsqueda por Id
                var car = await _carRepository.FindOneAsync(request.Id, cancellationToken);

                if (car == null)
                {
                    _logger.LogWarning("❷ Car not found: {CarId}", request.Id);
                    return null;
                }

                _logger.LogInformation("❸ Car found: {CarId}", car.Id);

                // Actualizamos propiedades
                car.Color = request.Color;
                car.MotorNumber = request.MotorNumber;
                _logger.LogInformation("❹ Updated car properties: Color={Color}, MotorNumber={MotorNumber}", car.Color, car.MotorNumber);

                // Guardamos cambios en la DB usando tu SaveChangesAsync
                await _carRepository.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("❺ SaveChangesAsync completed for CarId: {CarId}", car.Id);

                // Devolver DTO
                var result = new CarDto
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    Color = car.Color,
                    MotorNumber = car.MotorNumber,
                    ChassisNumber = car.ChassisNumber
                };

                _logger.LogInformation("❻ Returning updated CarDto for CarId: {CarId}", car.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❼ ERROR updating CarId: {CarId}. Exception: {ExceptionType} - {ExceptionMessage}",
                    request.Id, ex.GetType().Name, ex.Message);
                throw;
            }
        }
    }


}
