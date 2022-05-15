using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Exceptions
{
    public class EntitiesNotFoundException : Exception
    {
        public EntitiesNotFoundException()
        { }

        public EntitiesNotFoundException(string message) : base(message)
        { }

        public EntitiesNotFoundException(string message, Exception inner) : base(message, inner)
        { }
    }
}
