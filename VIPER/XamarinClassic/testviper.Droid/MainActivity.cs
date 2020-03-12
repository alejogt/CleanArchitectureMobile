using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Autofac.Core;
using Android.Content;
using testviper.Droid.Domains.Transfers.View;
using Plugin.CurrentActivity;

namespace testviper.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            var intent = new Intent(this, typeof(TransfersFirstActivity));
            this.StartActivity(intent);

        }
    }
}