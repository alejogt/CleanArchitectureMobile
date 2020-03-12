using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [Activity(Label = "TransfersActivity")]
    public class TransfersFirstActivity : Activity, ITransfersFirstView
    {
        public EditText txtNombre;

        ITransfersPresenter Presenter;

        #region Singleton
        static TransfersFirstActivity instance = null;

        public static TransfersFirstActivity GetInstance()
        {
            return instance ?? (instance = new TransfersFirstActivity());
        }

        void Init(TransfersFirstActivity context)
        {
            instance = context;
        }
        #endregion

        public TransfersFirstActivity()
        {
            Init(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transfers_FirstScreen);

            Presenter = MainApplication.Resolve<ITransfersPresenter>();

            Button btnSiguiente = FindViewById<Button>(Resource.Id.btnSiguiente);
            txtNombre = FindViewById<EditText>(Resource.Id.txtNombre);

            btnSiguiente.Click += (object sender, EventArgs e) =>
            {
                Presenter.Click(txtNombre.Text);
            };
        }

        public void SetSpacesError()
        {
            txtNombre.SetError("No ingrese espacios", GetDrawable(Resource.Drawable.error));
        }
    }
}