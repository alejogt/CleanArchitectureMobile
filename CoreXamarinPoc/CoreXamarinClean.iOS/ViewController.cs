using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poc.providers.api;
using poc.providers.api.Models;
using poc.providers.api.Models.MobileFirst;
using poc.providers.api.Providers.MobileFirst;
using UIKit;
using CoreXamarinPoc.iOS.Domains.Commons.Entities;
using System.Collections.Generic;
using CoreXamarinClean.iOS;

namespace CoreXamarinpoc.iOS
{
    public partial class ViewController : UIViewController, IMobileFirstSecurityCheck
    {
        int count = 1;

        public SecurityChallengeHandler SecurityChallenge { get; set; }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            Button.TouchUpInside += delegate
            {
                var title = string.Format("{0} clicks!", count++);
                Button.SetTitle(title, UIControlState.Normal);


                IApiProviderService apiProviderService = AppDelegate.Resolve<IApiProviderService>();

                Task.Run(async () =>
                {
                    ParametersRequest request = new ParametersRequest();
                request.data = new List<ParametersData>();
                request.data.Add(new ParametersData
                {
                    type = "RemoteConfigInformation",
                    id = "111",
                    attributes = new ParametersAttributes
                    {
                        appID = "co.com.bancolombia.canalesmoviles.apppyme",
                    }
                });

                RequestService requestService = new RequestService();
                requestService.EndPoint = "/adapters/RemoteConfiAdapter_V2/remote-config/remote-config-list";
                requestService.Request = request;
                requestService.Scope = "noAuthenticityScope";

                var abc = await apiProviderService.Provider(ProviderType.MobileFirst, AppDelegate.Resolve<IMobileFirstClients>()).Get(requestService);
                });
            };

            ButtonLogin.AccessibilityIdentifier = "myButtonLogin";
            ButtonLogin.TouchUpInside += delegate
            {
                var title = string.Format("{0} Login!", count++);
                ButtonLogin.SetTitle(title, UIControlState.Normal);

                IApiProviderService apiProviderService = AppDelegate.Resolve<IApiProviderService>();

                //Task.Run(async () =>
                //{
                //    //ProviderResult response = await apiProviderService.Provider(ProviderType.AWS).Authentication("UserAuthenticationSecurityCheck_V2", new Newtonsoft.Json.Linq.JObject());
                //});
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        public Task AddChallenge(string securityCheck, JObject challengeAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
