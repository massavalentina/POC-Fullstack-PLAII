using Core.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.CreateCar
{
    public class CreateCarCommand : IRequestCommand<string>
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Color { get; set; }

        public CreateCarCommand()
        {
        }
    }
}
