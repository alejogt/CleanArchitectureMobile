using System;
using System.Threading;
using System.Threading.Tasks;
using poc.providers.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Worklight;
using poc.providers.api.Models.MobileFirst;
using poc.providers.api.Utils;

namespace poc.providers.api.Providers.MobileFirst
{
    public class MobileFirstRepository : BaseProvider, IApiProviderRepository
    {
        readonly IWorklightClient client;
        public SecurityChallengeHandler SecurityChallenge { get; set; }

        public MobileFirstRepository(IMobileFirstClients helper)
        {
            this.client = helper.MobileFirstClient;
        }

        public async Task<ProviderResult> Put(RequestService request)
        {
            return await this.CallService(
                request.EndPoint,
                ServiceHttpMethod.PUT,
                JsonFormatter.SerializeJObject(request.Request),
                request.Scope == null ? string.Empty : request.Scope
                );
        }

        public async Task<ProviderResult> Delete(RequestService request)
        {
            return await this.CallService(
                request.EndPoint,
                ServiceHttpMethod.DELETE,
                JsonFormatter.SerializeJObject(request.Request),
                request.Scope == null ? string.Empty : request.Scope
                );
        }

        public async Task<ProviderResult> Get(RequestService request)
        {
            return await this.CallService(
                request.EndPoint,
                ServiceHttpMethod.GET,
                JsonFormatter.SerializeJObject(request.Request),
                request.Scope == null ? string.Empty : request.Scope
                );
        }

        public async Task<ProviderResult> Post(RequestService request)
        {
            return await this.CallService(
                request.EndPoint,
                ServiceHttpMethod.POST,
                JsonFormatter.SerializeJObject(request.Request),
                request.Scope == null ? string.Empty : request.Scope
                );
        }

        #region Mobile first implementation

        public async Task<ProviderResult> Login(ISecurityCheckDelegate checkDelegate, string scopeSecurity, JObject challengeRequest)
        {
            ProviderResult result = new ProviderResult();
            this.SecurityChallenge = new SecurityChallengeHandler(scopeSecurity, challengeRequest);
            this.SecurityChallenge.eventSuccessHandler += checkDelegate.AuthenticationSucessEventHandle;
            this.SecurityChallenge.eventHandleFailure += checkDelegate.AuthenticationFailureEventHandle;

            this.SecurityChallenge.ShouldCancel();
            this.client.RegisterChallengeHandler(this.SecurityChallenge);
            this.client.AuthorizationManager.SetLoginTimeOut(this.Timeout);

            try
            {
                await MobileFirstHelper.Instance.GetSemaphore().WaitAsync();
            }
            catch (ObjectDisposedException)
            {
                MobileFirstHelper.Instance.DisponseSemaphore();
                await MobileFirstHelper.Instance.GetSemaphore().WaitAsync();
            }

            var resultChallenge = await this.client.AuthorizationManager.Login(scopeSecurity, challengeRequest);

            result.Success = resultChallenge.Success;
            result.Message = resultChallenge.Message;
            result.CodeStatus = resultChallenge.HTTPStatus;
            result.Response = JsonConvert.SerializeObject(resultChallenge.ResponseJSON);

            MobileFirstHelper.Instance.GetSemaphore().Release();

            return result;
        }

        public async Task<ProviderResult> Logout(string securityCheck)
        {
            ProviderResult result = new ProviderResult();
            WorklightResponse resultChallenge;
            try
            {
                await Task.Delay(2000);

                try
                {
                    if (MobileFirstHelper.Instance.GetSemaphore().CurrentCount > 0)
                    {
                        MobileFirstHelper.Instance.GetSemaphore().Release();
                    }
                    else
                    {
                        MobileFirstHelper.Instance.GetSemaphore().Dispose();
                    }
                }
                catch (Exception)
                {
                    //Si falla el semaforo se sigue ejecutando el código.
                }

                resultChallenge = await this.client.AuthorizationManager.Logout(securityCheck);

                result.Success = resultChallenge.Success;
                result.Message = resultChallenge.Message;
                result.CodeStatus = resultChallenge.HTTPStatus;
                result.Response = JsonConvert.SerializeObject(resultChallenge.ResponseJSON);
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = string.Empty;
                result.CodeStatus = -1;
                result.Message = string.Empty;
            }

            return result;
        }

        public async Task<ProviderResult> GetAccessToken(string securityCheck)
        {
            ProviderResult result = new ProviderResult();

            try
            {

                WorklightAccessToken autenticated = await this.client.AuthorizationManager.ObtainAccessToken(securityCheck);

                result.Response = autenticated.Value;
                result.Success = autenticated.Value != null;

            }
            catch (NullReferenceException)
            {
                result.Success = false;
            }
            catch (Exception)
            {
                result.Success = false;
            }

            return result;
        }

        private async Task<ProviderResult> CallService(string endpoint, ServiceHttpMethod method, JObject parameters, string scopes)
        {
            ProviderResult result = new ProviderResult();

            try
            {
                await MobileFirstHelper.Instance.GetSemaphore().WaitAsync();
            }
            catch (ObjectDisposedException)
            {
                MobileFirstHelper.Instance.DisponseSemaphore();
                await MobileFirstHelper.Instance.GetSemaphore().WaitAsync();
            }

            try
            {
                StringBuilder build = new StringBuilder().Append(endpoint);

                WorklightResourceRequest request;
                WorklightResponse respuesta;

                if (String.IsNullOrEmpty(scopes))
                {
                    request = this.client.ResourceRequest(new Uri(build.ToString(), UriKind.Relative), method.ToString());
                }
                else
                {
                    request = this.client.ResourceRequest(new Uri(build.ToString(), UriKind.Relative), method.ToString(), scopes);
                }

                request.Timeout = this.Timeout == 0 ? 10000 : this.Timeout;


                if (parameters != null)
                {
                    string param = JsonConvert.SerializeObject(parameters);
                    respuesta = await request.Send(parameters);
                }
                else
                {
                    respuesta = await request.Send();
                }


                result.Success = respuesta.Success;
                result.Message = respuesta.Message;
                result.CodeStatus = respuesta.HTTPStatus;
                result.Response = JsonConvert.SerializeObject(respuesta.ResponseJSON);

            }
            catch (Exception ex)
            {
                result.CodeStatus = ex.HResult;
                result.Success = false;
                result.Message = ex.Message;
                result.Response = "";
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                MobileFirstHelper.Instance.GetSemaphore().Release();
            }

            return result;
        }

        #endregion
    }
}
