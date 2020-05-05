using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models.DTO;
using MobilityDC.Services.Data;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class LoginViewModel : BaseViewModel
    { 
        private string _userId;
        public string UserId {
            get {
                return _userId;
            }
            set {
                SetProperty(ref _userId, value);
            }
        }

        private string _password;
        public string Password {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private string _deviceNumber;
        public string DeviceNumber {
            get
            {
                return _deviceNumber;
            }
            set
            {
                SetProperty(ref _deviceNumber, value);
            }
        }

        private bool _busy;
        public bool Busy {
            get
            {
                return _busy;
            }
            set
            {
                SetProperty(ref _busy, value);
            }
        }

        private bool _buttonBusy;
        public bool ButtonBusy
        {
            get
            {
                return _buttonBusy;
            }
            set
            {
                SetProperty(ref _buttonBusy, value);
            }
        }

        public ICommand SignInCommand { get; set; }
        private INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ButtonBusy = true;
            SignInCommand = new Command(async () => await SignIn());
        }

        private async Task SignIn()
        {
            Busy = true;
            ButtonBusy = false;
            var loginModel = new LoginModel()
            {
                Password = _password,
                UserId = _userId,
                DeviceNumber = _deviceNumber
            };

            var data = new LoginService(); 

            try
            {
                var result = data.SignInAsync(loginModel);

                Busy = false;

                if (!result.Status )
                {
                    await _navigationService.DisplayAlert("Sign In Failed.", result.Message, "Ok");
                    ButtonBusy = true;
                    return;
                }

                _navigationService.RootPage(new OptionMenuPage());

            }
            catch (Exception ex)
            {
                Busy = false;
                ButtonBusy = true;
                await _navigationService.DisplayAlert("Server.", ex.Message, "Ok");
            }
        }

       
    }
}
