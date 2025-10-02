

using Application.UseCases.Car.Commands.DeleteCar;
using Application.UseCases.Car.Queries.GetCarByChassisNumber;
using Application.UseCases.DummyEntity.Queries.GetDummyEntityBy;
using Controllers;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete("api/v1/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("api/v1/{chassisNumber}")]  // ← Solo el parámetro, sin :string
        public async Task<IActionResult> GetByChassisNumber(string chassisNumber)
        {
            if (chassisNumber == null)
                return BadRequest("El número de chasis no puede ser nulo");

            var query = new GetCarByChassisNumberQuery(chassisNumber);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}