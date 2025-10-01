

using Application.Repositories;
using Application.UseCases.Car.Commands.DeleteCar;
using Application.UseCases.Car.Commands.UpdateCar;
using Application.UseCases.DummyEntity.Commands.UpdateDummyEntity;
using Controllers;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Car.Queries.GetAllCars;
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarCommand command)
        {
            if (command is null) return BadRequest();

            // Sobreescribimos el id del body con el de la ruta
            command.Id = id;

            var updatedCar = await _mediator.Send(command);

            if (updatedCar == null)
                return NotFound($"Car with ID {id} not found");

            return Ok(updatedCar);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCarsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }



    }
}