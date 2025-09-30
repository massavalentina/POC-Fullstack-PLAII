using Application.DataTransferObjects;
using Core.Application;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Car.Queries.GetCarByChassisNumber
{
    public class GetCarByChassisNumberQuery : IRequestQuery<CarDto>
    {
        [Required]
        public int ChassisNumber { get; set; }

        public GetCarByChassisNumberQuery(int chassisNumber)
        {
            ChassisNumber = chassisNumber;
        }
    }
}
