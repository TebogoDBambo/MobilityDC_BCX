
using MobilityDC.Models.DTO;
using MobilityDC.Services.NavigationService;
using MobilityDC.ViewModels;
using Xamarin.Forms;

namespace MobilityDC.Views
{
    public partial class BulkPickPage : ContentPage
    {
       

        private INavigationService _navigationService;
        public BulkPickPage()
        {
            InitializeComponent();
        }
            public BulkPickPage(INavigationService navigationService, GetNextTaskModel modelSearch)
        {
            InitializeComponent();
            _navigationService = navigationService;

            this.BindingContext = new BulkPickViewModel( navigationService, modelSearch);
        }
    }
}
