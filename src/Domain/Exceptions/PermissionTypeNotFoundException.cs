using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.API.Domain.Exceptions
{
    public class PermissionTypeNotFoundException : BusinessException
    {
        public PermissionTypeNotFoundException(Guid id) : base($"Permission Type with ID {id} does not exist")
        {
            
        }
    }
}
