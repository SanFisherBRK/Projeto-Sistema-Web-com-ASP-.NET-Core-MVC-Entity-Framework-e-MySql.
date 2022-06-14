using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //Exceção personalizada NotFound
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
