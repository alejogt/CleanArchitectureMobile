using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poc.providers.api.Models;
using poc.providers.api.Providers.MobileFirst;

namespace poc.providers.api.Providers
{
    public interface IApiProviderRepository
    {

        int Timeout { get; set; }
        Task<ProviderResult> Get(RequestService request);
        Task<ProviderResult> Put(RequestService request);
        Task<ProviderResult> Post(RequestService request);
        Task<ProviderResult> Delete(RequestService request);
        Task<ProviderResult> Login(ISecurityCheckDelegate checkDelegate, string scopeSecurity, JObject challengeRequest);
        Task<ProviderResult> Logout(string scopeSecurity);
    }
}
