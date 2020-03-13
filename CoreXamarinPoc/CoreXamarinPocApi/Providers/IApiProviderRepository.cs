using System;
using System.Threading.Tasks;
using poc.providers.api.Models;

namespace poc.providers.api.Providers
{
    public interface IApiProviderRepository
    {
        int Timeout { get; set; }
        Task<ProviderResult> GetItemByFields<T>(string baseUrl, string url, T input, string[] optional = null);
        Task<ProviderResult> Get<T>(string baseUrl, string url, T input, string[] optional = null);
        Task<ProviderResult> Create<T>(string baseUrl, string url, T input, string[] optional = null);
        Task<ProviderResult> Update<T>(string baseUrl, string url, T input, string[] optional = null);
        Task<ProviderResult> Delete<T>(string baseUrl, string url, T input, string[] optional = null);
    }
}
