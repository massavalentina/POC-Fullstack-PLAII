using Application.DataTransferObjects;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Queries.GetAllCars
{
    internal class GetAllCarsHandler : IRequestHandler<GetAllCarsQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        public async Task<List<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.FindAllAsync(); // Trae todos los autos desde BaseRepository

            // Convertimos a DTO si lo necesitas
            var result = cars.Select(c => new CarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Color = c.Color,
                ModelYear = c.ModelYear,
                MotorNumber = c.MotorNumber,
                ChassisNumber = c.ChassisNumber
            }).ToList();

            return result;
        }
    }
}
