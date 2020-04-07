using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poc.providers.api.Exceptions;
using poc.providers.api.Models;
using poc.providers.api.Providers.MobileFirst;
using poc.providers.api.Utils;

namespace poc.providers.api.Providers.AWS
{
    public class AwsRepository : BaseProvider, IApiProviderRepository
    {
        static HttpClient client = new HttpClient();

        public AwsRepository()
        {
        }

        private void InitClient(string baseUrl, Dictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0) throw new NoOptionalException("Listado de opcional está nulo. Para el AwsProvider se requiere un listado con los datos opcionales: 0 - Token de mobile first.");

            client.Timeout = this.Timeout == 0 ? TimeSpan.FromMilliseconds(10000) : TimeSpan.FromMilliseconds(this.Timeout);

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (headers.ContainsKey("token") ? headers.GetValueOrDefault("token"): string.Empty));

        }

        public async Task<ProviderResult> Put(RequestService request)
        {
            this.InitClient(request.EndPoint, request.Options);

            string json = JsonFormatter.Serialize(request.Request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(request.EndPoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public async Task<ProviderResult> Post(RequestService request)
        {
            this.InitClient(request.EndPoint, request.Options);

            string json = JsonFormatter.Serialize(request.Request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(request.EndPoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public async Task<ProviderResult> Delete(RequestService  request)
        {
            this.InitClient(request.EndPoint, request.Options);

            //TODO: Concatenar en la URL el _id del elemento que se quiere eliminar.

            HttpResponseMessage response = await client.DeleteAsync(request.EndPoint);

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public async Task<ProviderResult> Get(RequestService request)
        {
            this.InitClient(request.EndPoint, request.Options);

            //TODO: Concatenar en la URL el filtro de datos del listado que se quiere obtener.

            HttpResponseMessage response = await client.GetAsync(request.EndPoint);

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = new ProviderResult();

            result.Success = true;
            result.CodeStatus = (int)response.StatusCode;
            result.Response = responseJson;

            return result;
        }

        public Task<ProviderResult> Login(ISecurityCheckDelegate checkDelegate, string scopeSecurity, JObject challengeRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ProviderResult> Logout(string scopeSecurity)
        {
            throw new NotImplementedException();
        }
    }
}
