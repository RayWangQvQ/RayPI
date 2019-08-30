//系统包
using System;

namespace RayPI.Infrastructure.Treasury.Snowflake
{
    public class InvalidSystemClockException : Exception
    {
        public InvalidSystemClockException(string message)
            : base(message)
        {
        }
    }
}
