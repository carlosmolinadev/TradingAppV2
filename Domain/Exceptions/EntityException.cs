using System;
namespace TradingAppMvc.Domain.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException(string message) : base(message)
        {

        }
    }
}