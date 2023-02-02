using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSoulution.Utilities.Excentions
{
    public class EShopException : Exception
    {
        public EShopException(string message) : base(message)
        {

        }
        public EShopException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
