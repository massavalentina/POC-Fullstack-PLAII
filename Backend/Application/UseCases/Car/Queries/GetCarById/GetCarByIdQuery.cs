using Application.DataTransferObjects;
using Core.Application;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Car.Queries.GetCarById
{
    public class GetCarByIdQuery : IRequestQuery<CarDto>
    {
        [Required]
        public Guid Id { get; set; }

        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }

        public GetCarByIdQuery()
        {
        }
    }
}
