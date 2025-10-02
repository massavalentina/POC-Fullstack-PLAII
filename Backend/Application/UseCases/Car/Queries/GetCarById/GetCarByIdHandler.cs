using Application.Constants;
using Application.DataTransferObjects;
using Application.Exceptions;
using Application.Repositories;
using Core.Application;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Car.Queries.GetCarById
{
    internal sealed class GetCarByIdHandler
        : IRequestQueryHandler<GetCarByIdQuery, CarDto>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {

            var car = await _carRepository.GetByIdAsync(request.Id, cancellationToken);

            if (car == null)
            {
                throw new EntityDoesNotExistException(ApplicationConstants.ENTITY_DOESNOT_EXIST_EXCEPTION);
            }

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
