//系统包
using System;

namespace RayPI.Infrastructure.Treasury.Snowflake
{
    public static class SnowflakeHelper
    {
        public static Func<long> currentTimeFunc = new Func<long>(InternalCurrentTimeMillis);
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return currentTimeFunc();
        }

        public static IDisposable StubCurrentTime(Func<long> func)
        {
            currentTimeFunc = func;
            return new DisposableAction(() => currentTimeFunc = new Func<long>(InternalCurrentTimeMillis));
        }

        public static IDisposable StubCurrentTime(long millis)
        {
            currentTimeFunc = () => millis;
            return new DisposableAction(() => currentTimeFunc = new Func<long>(InternalCurrentTimeMillis));
        }

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
