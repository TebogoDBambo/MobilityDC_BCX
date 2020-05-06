using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models;
using MobilityDC.Models.Commons;
using MobilityDC.Models.DTO;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class FinePickSearchViewModel : BaseViewModel
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

        private string getNullValue(string text)
        {
            return String.IsNullOrEmpty(text) ? null : text;
        }

        private string formatFinalLocation(string storeNo)
        {
            if (String.IsNullOrEmpty(storeNo))
                return storeNo;

                return !storeNo.Contains(".") ? String.Format(".{0}", storeNo) : storeNo;
        }

        public async Task SearchAndPopulate()
        {
            Busy = true;

            FinePickModelSearch model = new FinePickModelSearch
            {
                SearchBarcode = getNullValue(_searchBarcode),
                SearchFromLocationCode = getNullValue(_searchFromLocationCode),
                SearchToLocationCode = formatFinalLocation(getNullValue(_finalDesitnation)),
                SearchDocumentNo = getNullValue(_searchDocumentNo),
                SearchProduct = getNullValue(_searchProduct)
            };

            try
            {
                Result result = ConnectionService.SearchFinePick(model, AppSession.UserId, false);

                if (result.Status)
                {
                    await _navigationService.PushAsync(new FinePickPage(_navigationService, model, JsonConvert.DeserializeObject<GetNextTaskModel>(result.Data.ToString())));
                }
                else
                {
                    await _navigationService.DisplayAlert("Search - Fine Pick", result.Message, "Ok");
                }
            }
            catch (Exception ex)
            {
                await _navigationService.DisplayAlert("Error - Search", ex.InnerException.StackTrace, "Ok");
            }

            Busy = false;
        }
    }
}
