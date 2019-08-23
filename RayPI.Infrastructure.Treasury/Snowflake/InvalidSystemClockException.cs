using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Treasury.Snowflake
{
    public class InvalidSystemClockException : Exception
    {
        public InvalidSystemClockException(string message)
            : base(message)
        {
        }
    }
}
