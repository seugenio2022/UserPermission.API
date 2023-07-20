using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.API.Domain.Exceptions
{
    public class EmployeeNotFoundException : BusinessException
    {
        public EmployeeNotFoundException(Guid id) : base($"Employee with ID {id} does not exist")
        {
            
        }
    }
}
