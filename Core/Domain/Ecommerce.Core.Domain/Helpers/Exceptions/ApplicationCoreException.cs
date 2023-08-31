using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Domain.Helpers.Exceptions
{
    public class ApplicationCoreException:Exception
    {
        public ApplicationCoreException(string? message) : base(message)
        {
        }
        public ApplicationCoreException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
