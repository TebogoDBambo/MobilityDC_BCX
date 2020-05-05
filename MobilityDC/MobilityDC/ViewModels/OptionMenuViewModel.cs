using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class OptionMenuViewModel
    {
        public ICommand MobilityDCCommand { get; set; }
        public ICommand RFIdCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private INavigationService _navigationService;
        public OptionMenuViewModel(INavigationService navigationService)
        {
            MobilityDCCommand = new Command(() => MobilityDCNavigateMethod());
            RFIdCommand = new Command((async () => await RFIdNavigateMethod()));
            CloseCommand = new Command((async () => await LoginPageNavigateMethod()));
            _navigationService = navigationService;
        }

        public async Task RFIdNavigateMethod()
        {
            await _navigationService.DisplayAlert("Alert", "Under construction", "ok");
        }

        public void  MobilityDCNavigateMethod()
        {
            _navigationService.RootPage(new MainPage());
        }

        public async Task LoginPageNavigateMethod()
        {
            await _navigationService.PushModalAsync(new LoginPage());
        }
    }
}
