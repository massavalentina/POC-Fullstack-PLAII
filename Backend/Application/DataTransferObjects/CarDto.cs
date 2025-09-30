using static Domain.Enums.Enums;

namespace Application.DataTransferObjects
{
    public class CarDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Color { get; set; }
        public DateTime Year { get; set; }

    }
}
