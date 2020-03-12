using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace testviper.Core.Domains.Transfers.Presenter
{
    public interface ITransfersPresenter
    {
        string Title { get; set; }

        void Click(string nombre);
    }
}
