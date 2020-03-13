using System;
using System.Threading.Tasks;
using poc.providers.api.Models;
using poc.providers.api.Providers;
using poc.providers.api.Providers.AWS;
using poc.providers.api.Providers.MobileFirst;

namespace poc.providers.api
{
    public class ApiProviderService : IApiProviderService
    {
        public readonly IApiProviderRepository apiProviderRepository = null;
        public ApiProviderService()
        {
            
        }

        public IApiProviderRepository Provider(ProviderType providerType, IMobileFirstClient mobileFirstClient = null)
        {
            IApiProviderRepository apiProviderRepository = null;
            switch (providerType)
            {
                case ProviderType.MobileFirst:
                    apiProviderRepository = new MobileFirstRepository(mobileFirstClient);
                    break;
                case ProviderType.AWS:
                    apiProviderRepository = new AwsRepository();
                    break;
            }
            return apiProviderRepository;
        }

        public int Timeout { get { return apiProviderRepository.Timeout; } set { apiProviderRepository.Timeout = value; } }
    }
}
