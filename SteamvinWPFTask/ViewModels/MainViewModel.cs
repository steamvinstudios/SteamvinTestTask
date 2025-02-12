using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitfinexConnector.Core.Interfaces;

namespace BitfinexConnector.WPF.ViewModels
{
    class MainViewModel
    {
        private readonly ITestConnector _connector;

        public MainViewModel(ITestConnector connector)
        {
            _connector = connector;
            _connector.NewBuyTrade += OnNewBuyTrade;
        }
    }
}
