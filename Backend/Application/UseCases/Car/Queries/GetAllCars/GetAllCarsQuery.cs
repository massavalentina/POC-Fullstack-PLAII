using Application.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Queries.GetAllCars
{
    public class GetAllCarsQuery : IRequest<List<CarDto>>
    {
        
    }
}
