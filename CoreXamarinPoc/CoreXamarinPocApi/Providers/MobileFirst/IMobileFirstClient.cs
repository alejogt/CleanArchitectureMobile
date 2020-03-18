using System;
using Worklight;

namespace poc.providers.api.Providers.MobileFirst
{
    public interface IMobileFirstClients
    {
        IWorklightClient MobileFirstClient { get; }
    }
}
