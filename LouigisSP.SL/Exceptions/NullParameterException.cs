using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL.Exceptions
{
    public class NullParameterException : Exception
    {
        public NullParameterException()
        {
        }

        public NullParameterException(string message) : base(message)
        {
        }
    }
}
