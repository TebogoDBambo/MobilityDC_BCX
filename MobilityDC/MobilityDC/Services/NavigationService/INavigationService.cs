using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobilityDC.Services.NavigationService
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task PushModalAsync(Page page);
        Task PopModalAsync();
        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
        void RootPage(Page page);
    }
}
