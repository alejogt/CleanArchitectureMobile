using System;
using System.Threading;

namespace poc.providers.api.Providers.MobileFirst
{
    public class MobileFirstHelper
    {
        private static MobileFirstHelper instance = null;

        public static MobileFirstHelper Instance {
            get
            {
                if (instance == null)
                {
                    instance = new MobileFirstHelper();
                }
                return instance;
            }
        }

        public SemaphoreSlim GetSemaphore()
        {
            return MobileFirstUtilities.GetSemaphoreSlim();
        }

        public bool DisponseSemaphore()
        {
            MobileFirstUtilities.DisponseSemaphoreSlim();
            return true;
        }
    }
}
