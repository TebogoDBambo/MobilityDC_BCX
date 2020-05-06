using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Data.Services;
using MobilityDC.Models;
using MobilityDC.Models.Commons;
using MobilityDC.Models.DTO;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class BulkPickSearchViewModel : BaseViewModel
    {
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


        public BulkPickSearchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SearchCommand = new Command(async () => await SearchAndPopulate());

        }

        public async Task SearchAndPopulate()
        {
            Busy = true;

            var bulkModelSearch = new BulkPickModelSearch()
            {
                SearchFromLocationCode = _searchFromLocationCode,
                SearchBarcode = _searchBarcode,
                SearchDocumentNo = _searchDocumentNo
            };

            try
            {
                var result = ConnectionService.SearchBulkPick(bulkModelSearch, AppSession.UserId);

                if (!result.Status)
                {
                    Busy = false;
                    await _navigationService.DisplayAlert("Alert", result.Message, "Ok");
                }
                else
                {
                    Busy = false;

                    var bulkTask = JsonConvert.DeserializeObject<GetNextTaskModel>(result.Data.ToString());

                    //var jObject = JObject.Parse(result.Data.ToString());
                    //TaskModel taskObject = jObject.ToObject<TaskModel>();
                    await _navigationService.PushAsync(new BulkPickPage(_navigationService, bulkTask));
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
