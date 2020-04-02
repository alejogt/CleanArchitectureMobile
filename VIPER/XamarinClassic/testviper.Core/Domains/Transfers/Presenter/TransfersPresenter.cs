using Autofac;
using testviper.Core.Domains.Transfers.Interactor;
using testviper.Core.Domains.Transfers.Router;
using testviper.Core.Domains.Transfers.View;

namespace testviper.Core.Domains.Transfers.Presenter
{
    public class TransfersPresenter : ITransfersPresenter, ITransfersInteractorOutput
    {
        #region Properties
        private string title;
        #endregion

        #region Views
        public ITransfersFirstView FirstView { get; set; }
        public ITransfersSecondView SecondView { get; set; }
        #endregion

        #region Router
        public ITransfersRouter Router { get; }
        #endregion

        #region Interactor
        internal ITransfersInteractor Interactor { get; }
        #endregion

        #region Container
        internal IContainer Container;
        #endregion

        #region Construct
        public TransfersPresenter(ITransfersDomain domain, ITransfersRouter router, ITransfersInteractor interactor)
        {
            this.Container = domain.GetContainer();
            this.Router = router;
            this.Interactor = interactor;
            this.Interactor.InteractorOutput = this;
        }
        #endregion

        #region ITransfersPresenter
        public string Title { get => title; set => title = value; }
        public void Click(string nombre)
        {
            Interactor.CreateTitle(nombre);
        }
        #endregion

        #region ITransfersInteractorOutput
        public void ShowGreeting(string title)
        {
            Title = title;
            Router.GoToSecondScreen();
        }

        public void SetError()
        {
            FirstView = Container.Resolve<ITransfersFirstView>();
            FirstView.SetSpacesError();
        }
        #endregion
    }
}
