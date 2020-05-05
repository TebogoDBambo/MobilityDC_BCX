using System;
using System.Collections.Generic;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models;
using MobilityDC.Services.NavigationService;
using MobilityDC.ViewModels;
using Xamarin.Forms;

namespace MobilityDC.Views
{
    public partial class StorePickPage : ContentPage
    {

        public StorePickPage(INavigationService navigationService, PickModelSearch modelSearch, TaskModel task)
        {
            InitializeComponent();

            this.BindingContext = new StorePickViewModel(navigationService, modelSearch, task);
        }
    }
}
