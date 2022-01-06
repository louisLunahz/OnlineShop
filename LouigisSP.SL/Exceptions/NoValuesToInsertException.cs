using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL.Exceptions
{
    public class NoValuesToInsertException : Exception
    {
        public NoValuesToInsertException()
        {
        }

        public NoValuesToInsertException(string message) : base(message)
        {
        }

        public NoValuesToInsertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoValuesToInsertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
