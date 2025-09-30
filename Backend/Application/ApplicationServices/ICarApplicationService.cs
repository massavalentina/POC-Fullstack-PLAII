using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationServices
{
    // Application service interface for Car-related operations
    public interface ICarApplicationService
    {
       bool CarExists(object value);
    }
}
