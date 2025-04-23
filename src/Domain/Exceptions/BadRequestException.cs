using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
           : base()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BadRequestException(string parameter, object value)
            : base($"Invalid value for parameter '{parameter}': {value}.")
        {
        }
    }
}
