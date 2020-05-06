using System.Collections.Generic;
using MobilityDC.Models;
using MobilityDC.Models.DTO;
using MobilityDC.Services.NavigationService;
using MobilityDC.ViewModels;
using Xamarin.Forms;

namespace MobilityDC.Views
{
    public partial class FinePickPage : ContentPage
    {
        private List<TaskModel> _observablePicks;

        private INavigationService _navigationService;

        public FinePickPage(INavigationService navigationService, FinePickModelSearch modelSearch, GetNextTaskModel task)
        {
            InitializeComponent();

            this.BindingContext = new FinePickViewModel(navigationService, modelSearch, task);
        }
    }
}
