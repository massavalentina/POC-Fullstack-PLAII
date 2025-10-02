using Core.Domain.Entities;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car : DomainEntity<string, CarValidator>
    {
        private static long _motorSequence = 1;
        private static long _chassisSequence = 1;
        private static string _motorSuffix = "AA";
        private static string _chassisSuffix = "AA";
        private const string MotorPrefix = "MOT-";
        private const string ChassisPrefix = "CHA-";
        private const long MaxSequence = 999999999;
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime ModelYear { get; set; }
        public string MotorNumber { get; set; }
        public string ChassisNumber { get; set; }

        public Car() { }
        public Car(string make, string model, string color)
        {
            Id = Guid.NewGuid();
            Make = make;
            Model = model;
            ModelYear = DateTime.Now;
            MotorNumber = CreateMotorNumber();
            ChassisNumber = CreateChassisNumber();
            Color = color;
        }

        private static string CreateMotorNumber()
        {
            if (_motorSequence > MaxSequence)
            {
                _motorSequence = 1;
                _motorSuffix = IncrementSuffix(_motorSuffix);
            }
            string number = $"{MotorPrefix}{_motorSuffix}-{_motorSequence.ToString().PadLeft(10, '0')}";
            _motorSequence++;
            return number;
        }

        private static string CreateChassisNumber()
        {
            if (_chassisSequence > MaxSequence)
            {
                _chassisSequence = 1;
                _chassisSuffix = IncrementSuffix(_chassisSuffix);
            }
            string number = $"{ChassisPrefix}{_chassisSuffix}-{_chassisSequence.ToString().PadLeft(10, '0')}";
            _chassisSequence++;
            return number;
        }

        // Increments the suffix of two letters (AA, AB, ..., AZ, BA, ..., ZZ)
        private static string IncrementSuffix(string suffix)
        {
            char[] chars = suffix.ToCharArray();
            if (chars[1] < 'Z')
            {
                chars[1]++;
            }
            else
            {
                chars[1] = 'A';
                if (chars[0] < 'Z')
                    chars[0]++;
                else
                    chars[0] = 'A'; // Opcional: reinicia a "AA" después de "ZZ"
            }
            return new string(chars);
        }
    }
}
