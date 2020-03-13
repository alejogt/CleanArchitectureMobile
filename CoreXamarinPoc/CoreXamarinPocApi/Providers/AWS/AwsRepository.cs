using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using poc.providers.api.Exceptions;
using poc.providers.api.Models;
using poc.providers.api.Utils;

namespace poc.providers.api.Providers.AWS
{
    public class AwsRepository : BaseProvider, IApiProviderRepository
    {
        static HttpClient client = new HttpClient();

        public AwsRepository()
        {
        }

        private void InitClient(string baseUrl, string[] optional)
        {
            if (optional == null || optional.Length == 0) throw new NoOptionalException("Listado de opcional está nulo. Para el AwsProvider se requiere un listado con los datos opcionales: 0 - Token de mobile first.");

            client.Timeout = this.Timeout == 0 ? TimeSpan.FromMilliseconds(10000) : TimeSpan.FromMilliseconds(this.Timeout);

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + optional[0]);

        }

        public async Task<ProviderResult> Create<T>(string baseUrl, string url, T input, string[] optional = null)
        {
            this.InitClient(baseUrl, optional);

            string json = JsonFormatter.Serialize<T>(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public async Task<ProviderResult> Delete<T>(string baseUrl, string url, T input, string[] optional = null)
        {
            this.InitClient(baseUrl, optional);

            //TODO: Concatenar en la URL el _id del elemento que se quiere eliminar.

            HttpResponseMessage response = await client.DeleteAsync(url);

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public Task<ProviderResult> GetItemByFields<T>(string baseUrl, string url, T input, string[] optional = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ProviderResult> Get<T>(string baseUrl, string url, T input, string[] optional = null)
        {
            this.InitClient(baseUrl, optional);

            //TODO: Concatenar en la URL el filtro de datos del listado que se quiere obtener.

            HttpResponseMessage response = await client.GetAsync(url);

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public Task<ProviderResult> Update<T>(string baseUrl, string url, T input, string[] optional = null)
        {
            throw new NotImplementedException();
        }
    }
}
