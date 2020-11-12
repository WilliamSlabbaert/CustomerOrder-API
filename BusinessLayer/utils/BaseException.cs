using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.utils
{
    class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {
        }
    }
}
