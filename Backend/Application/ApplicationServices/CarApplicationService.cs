using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationServices
{
    // Concrete application service for Car-related operations. Overrides e methods defined in ICarApplicationService
    // Implements business logic using ICarRepository for data access.
    public class CarApplicationService(ICarRepository context) : ICarApplicationService
    {
        private readonly ICarRepository _context = context ?? throw new ArgumentNullException(nameof(context));
        public bool CarExists(object value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            var response = _context.FindOne(value);
            return response != null; 
        }
    
    }
}
