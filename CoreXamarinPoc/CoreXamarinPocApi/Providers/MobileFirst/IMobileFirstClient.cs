using System;
using Worklight;

namespace poc.providers.api.Providers.MobileFirst
{
    public interface IMobileFirstClient
    {
        IWorklightClient MobileFirstClient { get; }
    }
}
