using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ray.Infrastructure.Threading
{
    public static class CancellationTokenProviderExtension
    {
        public static CancellationToken FallbackToProvider(this ICancellationTokenProvider provider, CancellationToken prefferedValue = default)
        {
            return prefferedValue == default || prefferedValue == CancellationToken.None
                ? provider.Token
                : prefferedValue;
        }
    }
}
