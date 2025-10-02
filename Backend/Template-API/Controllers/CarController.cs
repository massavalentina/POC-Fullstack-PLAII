

using Application.Repositories;
using Application.UseCases.Car.Commands.DeleteCar;
using Application.UseCases.Car.Commands.UpdateCar;
using Application.UseCases.DummyEntity.Commands.UpdateDummyEntity;
using Application.UseCases.Car.Queries.GetCarByChassisNumber;
using Application.UseCases.DummyEntity.Queries.GetDummyEntityBy;
using Controllers;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Car.Queries.GetAllCars;
using Application.UseCases.Car.Queries.GetCarById;
namespace Controlles
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarsController : BaseController
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

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            var query = new GetCarByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("chassisNumber/{chassisNumber}")]  // ← Solo el parámetro, sin :string
        public async Task<IActionResult> GetByChassisNumber(string chassisNumber)
        {
            if (string.IsNullOrWhiteSpace(chassisNumber))
                return BadRequest("El número de chasis no puede ser nulo");

            var query = new GetCarByChassisNumberQuery(chassisNumber);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }

}