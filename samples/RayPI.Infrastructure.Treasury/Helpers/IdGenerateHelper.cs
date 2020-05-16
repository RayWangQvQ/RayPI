//本地项目包

using RayPI.Infrastructure.Treasury.Snowflake;

namespace RayPI.Infrastructure.Treasury.Helpers
{
    public static class IdGenerateHelper
    {
        private static readonly IdWorker Worker = new IdWorker(1L, 1L, 0L);

        public static long NewId
        {
            get
            {
                return IdGenerateHelper.Worker.NextId();
            }
        }
    }
}
