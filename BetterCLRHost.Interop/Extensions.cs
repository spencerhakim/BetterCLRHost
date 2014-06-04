using System;

namespace BetterCLRHost
{
    public static class Extensions
    {
        public static void SafeDispose(this IDisposable disposable)
        {
            if( disposable != null )
                disposable.Dispose();
        }
    }
}
