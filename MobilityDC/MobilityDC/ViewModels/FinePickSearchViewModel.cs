using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Data.Services;
using MobilityDC.Models;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class FinePickSearchViewModel : BaseViewModel
    {
        public List<TaskModel> observablePicks { get; set; } = new List<TaskModel>();

        private string _searchFromLocationCode;
        public string SearchFromLocationCode
        {
            get
            {
                return _searchFromLocationCode;
            }
            set
            {
                SetProperty(ref _searchFromLocationCode, value);
            }
        }

        private string _searchBarcode;
        public string SearchBarcode
        {
            get
            {
                return _searchBarcode;
            }
            set
            {
                SetProperty(ref _searchBarcode, value);
            }
        }

        private string _searchDocumentNo;
        public string SearchDocumentNo
        {
            get
            {
                return _searchDocumentNo;
            }
            set
            {
                SetProperty(ref _searchDocumentNo, value);
            }
        }


        private string _searchProduct;
        public string SearchProduct
        {
            get
            {
                return _searchProduct;
            }
            set
            {
                SetProperty(ref _searchProduct, value);
            }
        }

        private string _finalDesitnation;
        public string FinalDestination
        {
            get
            {
                return _finalDesitnation;
            }
            set
            {
                SetProperty(ref _finalDesitnation, value);
            }
        }


        private bool _busy;
        public bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                SetProperty(ref _busy, value);
            }
        }
        public ICommand SearchCommand { get; set; }

        private INavigationService _navigationService;


        public FinePickSearchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SearchCommand = new Command(async () => await SearchAndPopulate());

        }

        public async Task SearchAndPopulate()
        {
            Busy = true;
            var data = new PickDataStore();

            var pickModelSearch = new PickModelSearch()
            {
                SearchFromLocationCode = _searchFromLocationCode,
                SearchBarcode = _searchBarcode,
                SearchDocumentNo = _searchDocumentNo,
                SearchProduct = _searchProduct,
                SearchFinalLocationCode = _finalDesitnation,
                TaskType = "ILTFB"
            };

            try
            {
                var results = await data.GetItemAsync(pickModelSearch, "80");

                if (results == null)
                {
                    Busy = false;
                    await _navigationService.DisplayAlert("Alert", "Fine Pick Empty", "Ok");
                }
                else
                {
                    Busy = false;
                    await _navigationService.PushAsync(new FinePickPage(_navigationService, pickModelSearch, (TaskModel)results.Data));
                }
            }
            catch (Exception ex)
            {
                Busy = false;
                await _navigationService.DisplayAlert("Server.", ex.Message, "Ok");
            }
        }
    }
}
