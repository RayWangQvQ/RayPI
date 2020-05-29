using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ray.Infrastructure.Threading
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
