using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poc.providers.api.Models.MobileFirst;

namespace poc.providers.api.Providers.MobileFirst
{
    public interface ISecurityCheckDelegate
    {
        //public SecurityChallengeHandler SecurityChallenge { get; set; }
        //public Task AddChallenge(string securityCheck, JObject challenge);
        public void AuthenticationFailureEventHandle(JObject challenge, FailureType type);
        public void AuthenticationSucessEventHandle(JObject challenge);
    }
}
