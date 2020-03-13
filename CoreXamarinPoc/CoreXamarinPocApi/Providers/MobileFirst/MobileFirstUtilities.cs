using System;
using System.Threading;

namespace poc.providers.api.Providers.MobileFirst
{
    public static class MobileFirstUtilities
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public static SemaphoreSlim GetSemaphoreSlim()
        {
            return semaphoreSlim;
        }

        public static void DisponseSemaphoreSlim()
        {
            semaphoreSlim = new SemaphoreSlim(1, 1);
        }
    }
}
