using Core.Application;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Car.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequestCommand<Unit>
    {
        [Required]
        public int CarId { get; set; }

        public DeleteCarCommand()
        {
        }
    }
}
