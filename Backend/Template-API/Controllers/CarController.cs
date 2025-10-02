

using Application.Repositories;
using Application.UseCases.Car.Commands.CreateCar;
using Application.UseCases.Car.Commands.DeleteCar;
using Application.UseCases.Car.Commands.UpdateCar;
using Application.UseCases.Car.Queries.GetAllCars;
using Application.UseCases.Car.Queries.GetCarByChassisNumber;
using Application.UseCases.DummyEntity.Commands.CreateDummyEntity;
using Application.UseCases.DummyEntity.Commands.UpdateDummyEntity;
using Application.UseCases.DummyEntity.Queries.GetDummyEntityBy;
using Controllers;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/v1/car/[controller]")]
    public class CarsController : BaseController
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Delete method to delete a car by its ID
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }


        // Update method to update a car's details
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

        // Get method to retrieve all cars
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCarsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Get method to retrieve a car by its chassis number
        [HttpGet("chassis/{chassisNumber}")]
        public async Task<IActionResult> GetByChassisNumber(string chassisNumber)
        {
            if (string.IsNullOrWhiteSpace(chassisNumber))
                return BadRequest("El número de chasis no puede ser nulo");

            var query = new GetCarByChassisNumberQuery(chassisNumber);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command)
        {
            if (command is null) return BadRequest();

            var id = await _mediator.Send(command);

            return Ok();
        }
    }

}