

using Application.UseCases.Car.Commands.DeleteCar;
using Controllers;
using Core.Application;
using Microsoft.AspNetCore.Mvc;
namespace Controlles
{
    [ApiController]

    public class CarController(ICommandQueryBus commandQueryBus) : BaseController
    {
        private readonly ICommandQueryBus _commandQueryBus = commandQueryBus ?? throw new ArgumentNullException(nameof(commandQueryBus));

        [HttpDelete("api/v1/[Controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            await _commandQueryBus.Send(new DeleteCarCommand { CarId = id });

            return NoContent();
        }
    }
}