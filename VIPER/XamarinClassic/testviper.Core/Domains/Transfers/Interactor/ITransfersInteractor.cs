using System;
using System.Collections.Generic;
using System.Text;

namespace testviper.Core.Domains.Transfers.Interactor
{
    public interface ITransfersInteractor
    {
        ITransfersInteractorOutput InteractorOutput { get; set; }
        void CreateTitle(string nombre);
    }
}
