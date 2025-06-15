using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Exceptions
{
    public class ApiRequestException : Exception
    {
        public ApiRequestException(string message, Exception? inner = null)
            : base(message, inner) { }
    }
}
