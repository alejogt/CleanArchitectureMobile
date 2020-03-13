using System;
using Newtonsoft.Json.Linq;
using Worklight;

namespace poc.providers.api.Models.MobileFirst
{
    public delegate void EventSuccessHandler(JObject identity);
    public delegate void EventHandleFailure(JObject identity, FailureType type);

    public class SecurityChallengeHandler : SecurityCheckChallengeHandler
    {
        private bool shouldSubmitAnswer = false;

        private JObject ChallengeAnswer { get; set; }

        public event EventSuccessHandler eventSuccessHandler;
        public event EventHandleFailure eventHandleFailure;

        public bool IsInSession { get; set; }

        public SecurityChallengeHandler(string realm, JObject challengeAnswer)
        {
            //Realm = security method.
            this.SecurityCheck = realm;
            this.ChallengeAnswer = challengeAnswer;
        }

        public override string SecurityCheck { get; set; }

        public override JObject GetChallengeAnswer()
        {
            return this.ChallengeAnswer;
        }

        public override void HandleChallenge(object challenge)
        {
            shouldSubmitAnswer = true;
        }

        public override void HandleFailure(JObject error)
        {
            shouldSubmitAnswer = false;

            var str = error.ToString();

            FailureType type = FailureType.Invalid;

            if (str.ToLower().IndexOf("account blocked") > -1)
            {
                type = FailureType.Blocked;
            }

            this.eventHandleFailure(error, type);
        }

        public override void HandleSuccess(JObject identity)
        {
            shouldSubmitAnswer = false;
            this.eventSuccessHandler(identity);
        }

        public override bool ShouldSubmitChallengeAnswer()
        {
            return shouldSubmitAnswer;
        }
    }
}
