using Core.Application;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Car.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequestCommand<Unit>
    {
        [Required]
        public Guid CarId { get; set; }

        public DeleteCarCommand(Guid carId)
        {
            CarId = carId;
        }
    }
}
