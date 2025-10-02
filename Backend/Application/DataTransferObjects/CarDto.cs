using static Domain.Enums.Enums;

namespace Application.DataTransferObjects
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime ModelYear { get; set; }
        public string MotorNumber { get; set; }
        public string ChassisNumber { get; set; }
    }
}
