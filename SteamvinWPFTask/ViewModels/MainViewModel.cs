using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BitfinexConnector.Core.Services;
using BitfinexConnector.WPF.Models;
using System.Threading.Tasks;

namespace BitfinexConnector.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PortfolioService _portfolioService;
        public ObservableCollection<PortfolioItem> PortfolioItems { get; set; }

        public MainViewModel()
        {
            _portfolioService = new PortfolioService();
            PortfolioItems = new ObservableCollection<PortfolioItem>();
            LoadPortfolio();
        }

        private async void LoadPortfolio()
        {
            var portfolioData = await _portfolioService.CalculatePortfolioAsync();
            PortfolioItems.Clear();
            foreach (var item in portfolioData)
            {
                PortfolioItems.Add(new PortfolioItem
                {
                    Currency = item.Key,
                    Balance = item.Value
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
