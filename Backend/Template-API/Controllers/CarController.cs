

using Application.UseCases.Car.Commands.CreateCar;
using Application.UseCases.Car.Commands.DeleteCar;
using Controllers;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Controlles
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Post method to create a new car
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok();
         }


        // Delete method to delete a car by its ID
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}