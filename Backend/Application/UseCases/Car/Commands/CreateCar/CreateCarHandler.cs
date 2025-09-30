using Application.ApplicationServices;
using Application.Constants;
using Application.DomainEvents;
using Application.Exceptions;
using Application.Repositories;
using Application.UseCases.DummyEntity.Commands.CreateDummyEntity;
using Core.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.CreateCar
{
    internal sealed class CreateCarHandler(ICommandQueryBus domainBus, ICarRepository carRepository, ICarApplicationService carApplicationService) : IRequestCommandHandler<CreateCarCommand, string>
    {
        // Domain bus for publishing domain events
        private readonly ICommandQueryBus _domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        // Concrete repository for Car entity operations
        private readonly ICarRepository _context = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        // Application service for Car-related business logic
        private readonly ICarApplicationService _carApplicationService = carApplicationService ?? throw new ArgumentNullException(nameof(carApplicationService));

        // Handles the creation of a new Car entity
        public async Task<string> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            // Creates a new Car entity using the data from the request
            Domain.Entities.Car car = new(request.Make, request.Model, request.Color);

            // Validates the Car entity
            if (!car.IsValid) throw new InvalidEntityDataException(car.GetErrors());

            // Checks if a Car with the same ID already exists
            if (_carApplicationService.CarExists(car.Id)) throw new EntityDoesExistException();

            // If all validations pass, adds the Car to the repository and publishes a domain event
            try
            {
                object createdId = await _context.AddAsync(car);
                await _domainBus.Publish(car.To<CarCreated>(), cancellationToken);
                return createdId.ToString();
            }
            catch (Exception ex)
            {
                throw new BussinessException(ApplicationConstants.PROCESS_EXECUTION_EXCEPTION, ex.InnerException);
            }


        }
    }
}