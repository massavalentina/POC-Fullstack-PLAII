using Application.DataTransferObjects;
using Core.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest<CarDto>
    {
        public Guid Id { get; set; }         
        public int Color { get; set; }       
        public int MotorNumber { get; set; } 
    } 
}
