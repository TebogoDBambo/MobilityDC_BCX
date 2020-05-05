using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class HomeViewModel
    {
        public ICommand BulkPickCommand { get; set; }
        public ICommand FinePickCommand { get; set; }
        public ICommand StorePickCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private INavigationService _navigationService;


        public HomeViewModel(INavigationService navigationService)
        {
            BulkPickCommand = new Command(async () => await BulkPickMethod());
            FinePickCommand = new Command(async () => await FinePickMethod());
            StorePickCommand = new Command(async () => await StorePickMethod());
            HelpCommand = new Command(async () => await HelpMethod());
            CloseCommand = new Command(async () => await CloseMethod());

            _navigationService = navigationService;
        }
                
        public async Task BulkPickMethod()
        {
            await _navigationService.PushAsync(new BulkPickSearchPage());
        }

        public async Task FinePickMethod()
        {
            await _navigationService.PushAsync(new FinePickSearchPage());
        }
        public async Task StorePickMethod()
        {
            await _navigationService.PushAsync(new StorePickSearchPage());
        }
        public async Task HelpMethod()
        {
            await _navigationService.PushAsync(new AboutPage());
        }
        public async Task CloseMethod()
        {
            await _navigationService.DisplayAlert("Exit", "Would you like to close the app?", "Ok","Cancel");
        }
    }
}
