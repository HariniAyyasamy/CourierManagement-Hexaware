using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementExceptionLibaray
{

    [Serializable]
    public class InvalidEmployeeIdException : Exception
    {
        public InvalidEmployeeIdException() { }
        public InvalidEmployeeIdException(string message) : base(message) { }
        public InvalidEmployeeIdException(string message, Exception inner) : base(message, inner) { }
        protected InvalidEmployeeIdException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}