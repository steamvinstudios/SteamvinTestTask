using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BitfinexConnector.Core.Services;
using CommunityToolkit.Mvvm.Input;

namespace BitfinexConnector.WPF.ViewModels
{
    class PortfolioViewModel : INotifyPropertyChanged
    {
        private readonly PortfolioService _portfolioService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<BalanceItem> Balances { get; } = new ObservableCollection<BalanceItem>();

        public PortfolioViewModel(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
            LoadDataCommand = new RelayCommand(async () => await LoadData());
        }

        public ICommand LoadDataCommand { get; }

        private async Task LoadData()
        {
            var balances = await _portfolioService.CalculatePortfolioInAllCurrenciesAsync();
            Balances.Clear();
            foreach (var item in balances)
                Balances.Add(new BalanceItem { Currency = item.Key, Value = item.Value });
        }
    }
}
