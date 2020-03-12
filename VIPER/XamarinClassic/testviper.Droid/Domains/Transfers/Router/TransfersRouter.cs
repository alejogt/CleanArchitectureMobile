using Android.Content;
using Plugin.CurrentActivity;
using System;
using testviper.Core.Domains.Transfers.Router;
using testviper.Droid.Domains.Transfers.View;

namespace testviper.Droid.Domains.Transfers.Router
{
    class TransfersRouter : ITransfersRouter
    {
        public void GoToSecondScreen()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            Intent intent = new Intent(activity, typeof(TransfersSecondActivity));
            activity.StartActivity(intent);
        }
    }
}