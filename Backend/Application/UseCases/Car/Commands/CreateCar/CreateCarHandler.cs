using Application.ApplicationServices;
using Application.Constants;
using Application.DomainEvents;
using Application.Exceptions;
using Application.Repositories;
using Application.UseCases.DummyEntity.Commands.CreateDummyEntity;
using Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.CreateCar
{
    internal sealed class CreateCarHandler(ICommandQueryBus domainBus, ICarRepository carRepository, ICarApplicationService carApplicationService) : IRequestCommandHandler<CreateCarCommand, Guid>
    {
        private readonly ICommandQueryBus _domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        private readonly ICarRepository _context = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        private readonly ICarApplicationService _carApplicationService = carApplicationService ?? throw new ArgumentNullException(nameof(carApplicationService));
        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Car entity = new(request.Make, request.Model, request.Color);

            if (!entity.IsValid) throw new InvalidEntityDataException(entity.GetErrors());

            //if (_carApplicationService.CarExists(entity.Id)) throw new EntityDoesExistException();

            try
            {
                var createdId = await _context.AddAsync(entity);

                await _domainBus.Publish(entity.To<CarCreated>(), cancellationToken);

                return createdId;
            }
            catch (Exception ex)
            {
                throw new BussinessException(ApplicationConstants.PROCESS_EXECUTION_EXCEPTION, ex.InnerException);
            }
        }
    }
}
