using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL.Exceptions
{
    public class IDNotFoundException : Exception
    {
        public IDNotFoundException()
        {
        }

        public IDNotFoundException(string message) : base(message)
        {
        }
    }
}
