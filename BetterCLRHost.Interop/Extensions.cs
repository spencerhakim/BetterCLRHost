using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLROBS
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
