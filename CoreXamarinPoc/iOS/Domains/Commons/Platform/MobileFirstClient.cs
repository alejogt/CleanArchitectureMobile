using System;
using MobileFirst.Xamarin.iOS;
using poc.providers.api.Providers.MobileFirst;
using Worklight;

namespace CoreXamarinPoc.iOS.Domains.Commons.Platform
{
    public class MobileFirstClients : IMobileFirstClients
    {
        public IWorklightClient MobileFirstClient
        {
            get
            {
                return WorklightClient.CreateInstance();
            }
        }
    }
}
