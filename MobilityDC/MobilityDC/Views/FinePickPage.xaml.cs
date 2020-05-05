using System;
using System.Collections.Generic;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models;
using MobilityDC.Services.NavigationService;
using MobilityDC.ViewModels;
using Xamarin.Forms;

namespace MobilityDC.Views
{
    public partial class FinePickPage : ContentPage
    {
        private List<TaskModel> _observablePicks;

        private INavigationService _navigationService;

        public FinePickPage(INavigationService navigationService, PickModelSearch modelSearch, TaskModel task)
        {
            InitializeComponent();

            this.BindingContext = new FinePickViewModel(navigationService, modelSearch, task);
        }
    }
}
