using System;
using Android.App;
using Android.Runtime;
using Autofac;
using Plugin.CurrentActivity;
using testviper.Core.Domains.Transfers;
using testviper.Core.Domains.Transfers.Presenter;
using testviper.Core.Domains.Transfers.Router;
using testviper.Core.Domains.Transfers.View;
using testviper.Droid.Domains.Transfers.Router;
using testviper.Droid.Domains.Transfers.View;

namespace testviper.Droid
{
    [Application]
    public class MainApplication : Application, ITransfersDomain
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public IContainer GetContainer()
        {
            return Container;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CreateInstances();
        }

        private void CreateInstances()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TransfersRouter>().As<ITransfersRouter>().SingleInstance();

            builder.Register((c, p) =>
            {
                return CrossCurrentActivity.Current.Activity is TransfersFirstActivity activity ? activity : TransfersFirstActivity.GetInstance();
            }).As<ITransfersFirstView>().SingleInstance();

            builder.Register((c, p) =>
            {
                return CrossCurrentActivity.Current.Activity is TransfersSecondActivity activity ? activity : TransfersSecondActivity.GetInstance();
            }).As<ITransfersSecondView>().SingleInstance();

            builder.RegisterType<TransfersPresenter>().As<ITransfersPresenter>().WithParameter("domain", this).SingleInstance();

            Container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
        }
    }
}