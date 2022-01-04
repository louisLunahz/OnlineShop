using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoppingPortal
{
    class NotValidOptionException: Exception
    {
        public NotValidOptionException()
        {
        }

        public NotValidOptionException(string message)
            : base(message)
        {
        }

        public NotValidOptionException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
