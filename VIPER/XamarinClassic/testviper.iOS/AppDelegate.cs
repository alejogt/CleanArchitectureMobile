namespace testviper.iOS
{
    using Autofac;
    using Autofac.Builder;
    using Foundation;
    using testviper.Core.Domains.Transfers.Presenter;
    using testviper.Core.Domains.Transfers.View;
    using testviper.iOS.Domains.Transfers.View;
    using UIKit;

    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        #region Attributes
        private UIStoryboard transfersStoryBoard;
        #endregion

        #region Properties
        public static IContainer Container { get; set; }        
        #endregion

        #region Methods
        [Export("window")]
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            transfersStoryBoard = UIStoryboard.FromName("Transfers", null);
            CreateInstances();
            return true;
        }

        // UISceneSession Lifecycle

        [Export("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration(UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
        {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create("Default Configuration", connectingSceneSession.Role);
        }

        [Export("application:didDiscardSceneSessions:")]
        public void DidDiscardSceneSessions(UIApplication application, NSSet<UISceneSession> sceneSessions)
        {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }

        public void CreateInstances()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register((c, p) =>
            {
                return UIApplication.SharedApplication.KeyWindow.RootViewController is FirstViewController viewController ? viewController : transfersStoryBoard.InstantiateViewController("FirstView");
            }).As<ITransfersFirstView>().SingleInstance();

            builder.Register((c, p) =>
            {
                return UIApplication.SharedApplication.KeyWindow.RootViewController is SecondViewController viewController ? viewController : transfersStoryBoard.InstantiateViewController("SecondView");
            }).As<ITransfersSecondView>().SingleInstance();

            builder.RegisterType<TransfersPresenter>().As<ITransfersPresenter>().WithParameter("domain", this).SingleInstance();

            Container = builder.Build(ContainerBuildOptions.None);
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        #endregion
    }
}

