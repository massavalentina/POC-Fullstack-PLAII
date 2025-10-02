using Application.Constants;
using Application.DataTransferObjects;
using Application.Exceptions;
using Application.Repositories;
using Core.Application;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Car.Queries.GetCarByChassisNumber
{
    internal sealed class GetCarByChassisNumberHandler
        : IRequestQueryHandler<GetCarByChassisNumberQuery, CarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<GetCarByChassisNumberHandler> _logger;

        public GetCarByChassisNumberHandler(
            ICarRepository carRepository,
            ILogger<GetCarByChassisNumberHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<CarDto> Handle(GetCarByChassisNumberQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("🔎 Searching car with ChassisNumber: {ChassisNumber}", request.ChassisNumber);

            var car = await _carRepository.GetByChassisNumberAsync(request.ChassisNumber, cancellationToken);

            if (car == null)
            {
                _logger.LogWarning("🚫 Car with ChassisNumber {ChassisNumber} not found", request.ChassisNumber);
                throw new EntityDoesNotExistException(ApplicationConstants.ENTITY_DOESNOT_EXIST_EXCEPTION);
            }

            _logger.LogInformation("✅ Car with ChassisNumber {ChassisNumber} found", request.ChassisNumber);

            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                ModelYear = car.ModelYear,
                MotorNumber = car.MotorNumber,
                ChassisNumber = car.ChassisNumber
            };
        }

    }
}
// Varios autos con un mismo número de chasis

// return cars.Select(car => new CarDto
//{
//    Id = car.Id,
//    Color = car.Color,
//    Year = car.Year,
//    MotorNumber = car.MotorNumber,
//    ChassisNumber = car.ChassisNumber
//}).ToList();
//}