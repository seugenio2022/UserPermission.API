using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermission.API.Domain.Common;

namespace UserPermission.API.Domain.Exceptions
{
    public class InvalidEmailFormatException : BusinessException
    {
        public InvalidEmailFormatException(string email)
        : base($"Invalid email address format: \"{email}\"")
        {
        }
    }
}
