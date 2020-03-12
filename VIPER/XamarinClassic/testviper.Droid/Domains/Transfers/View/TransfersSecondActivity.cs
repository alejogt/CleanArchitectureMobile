using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using testviper.Core.Domains.Transfers.Presenter;
using testviper.Core.Domains.Transfers.View;

namespace testviper.Droid.Domains.Transfers.View
{
    [Activity(Label = "TransfersSecondActivity")]
    public class TransfersSecondActivity : Activity, ITransfersSecondView
    {
        ITransfersPresenter Presenter;

        #region Singleton
        static TransfersSecondActivity instance = null;
        public static TransfersSecondActivity GetInstance()
        {
            return instance ?? (instance = new TransfersSecondActivity());
        }
        void Init(TransfersSecondActivity context)
        {
            instance = context;
        }
        #endregion

        public TransfersSecondActivity()
        {
            Init(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transfers_SecondScreen);

            Presenter = MainApplication.Resolve<ITransfersPresenter>();
            SetHeader();
        }

        public void SetHeader()
        {
            TextView txtTitle = FindViewById<TextView>(Resource.Id.txtSaludo);
            txtTitle.Text = Presenter.Title;
        }
    }
}