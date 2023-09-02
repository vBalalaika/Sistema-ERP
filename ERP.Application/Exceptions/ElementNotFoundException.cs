using System;

namespace ERP.Application.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
        {

        }
        public ElementNotFoundException(string message) : base(message)
        {

        }
    }
}
