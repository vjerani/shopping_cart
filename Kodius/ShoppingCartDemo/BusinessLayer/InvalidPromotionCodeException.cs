using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.BusinessLayer
{
    public class InvalidPromotionCodeException : Exception
    {
        public InvalidPromotionCodeException(string message) : base(message)
        {
        }
    }
}
