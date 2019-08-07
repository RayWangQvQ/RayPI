using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Treasury.Helpers
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
