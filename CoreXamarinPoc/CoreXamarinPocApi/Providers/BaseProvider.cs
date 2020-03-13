using System;
namespace poc.providers.api.Providers
{
    public class BaseProvider
    {
        public int Timeout { get; set; }

        protected BaseProvider()
        {
            this.Timeout = 10000;
        }
    }
}
