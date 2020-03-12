using System;
using System.Collections.Generic;
using System.Text;

namespace testviper.Core.Domains.Transfers.Interactor
{
    public class TransfersInteractor : ITransfersInteractor
    {
        public ITransfersInteractorOutput InteractorOutput { get; set; }

        public TransfersInteractor(ITransfersInteractorOutput interactorOutput)
        {
            InteractorOutput = interactorOutput;
        }

        public void CreateTitle(string nombre)
        {
            if (nombre.Contains(" "))
            {
                InteractorOutput.SetError();
                return;
            }
            InteractorOutput.ShowGreeting(string.Format("Hola {0}. ¡Que tengas un lindo día!", nombre));
        }
    }
}
