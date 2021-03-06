﻿using System;
using System.Threading.Tasks;
using poc.providers.api.Models;
using poc.providers.api.Providers;
using poc.providers.api.Providers.MobileFirst;

namespace poc.providers.api
{
    public interface IApiProviderService
    {
        int Timeout { get; set; }
        ProviderType ProviderType { get; set; }
        IApiProviderRepository Provider(ProviderType providerType, IMobileFirstClients mobileFirstClient = null);

    }
}
