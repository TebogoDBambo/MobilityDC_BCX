using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Data.Services;
using MobilityDC.Models;
using MobilityDC.Models.DTO;
using MobilityDC.Models.Enums;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class BulkPickViewModel : BaseViewModel
    {
        private INavigationService _navigationService { get; set; }
   
        private GetNextTaskModel _currentTask;
        public GetNextTaskModel CurrentTask
        {
            get
            {
                return _currentTask;
            }
            set
            {
                SetProperty(ref _currentTask, value);
            }
        }

        //private TaskModel _previousTask;
        //public TaskModel PreviousTask
        //{
        //    get
        //    {
        //        return _previousTask;
        //    }
        //    set
        //    {
        //        SetProperty(ref _previousTask, value);
        //    }
        //}

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

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                SetProperty(ref _quantity, value);
            }
        }

        private string _skuCode;
        public string SkuCode
        {
            get
            {
                return _skuCode;
            }
            set
            {
                SetProperty(ref _skuCode, value);

                if (SkuCode == CurrentTask.FormattedSKU.Replace("-", "") || SkuCode == CurrentTask.Barcode)
                {
                    Quantity += 1;
                    SkuCode = string.Empty;
                }

            }
        }

        PickModelSearch _modelSearch;

        public ICommand NextCommand { get; set; }
        public ICommand SkipCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HoldCommand { get; set; }

        PickDataStore pickDataStore;

        public BulkPickViewModel(INavigationService navigationService, GetNextTaskModel bulkTask)
        {
            _navigationService = navigationService;
            CurrentTask = bulkTask;
            //_modelSearch = modelSearch;
            //pickDataStore = new PickDataStore();

            //NextCommand = new Command(async () => await NextTask());
            //SkipCommand = new Command(async () => await SkipTask());
            //BackCommand = new Command(async () => await Back());
            //HoldCommand = new Command(async () => await HoldTask());
      
        }

        #region Commands Methods
        //private async Task<object> Back()
        //{
        //    await _navigationService.DisplayAlert("Info", "loading previous task.", "Ok");
        //    Busy = true;

        //    await ExitTaskMethod();

        //    await GetTaskMethod();

        //    Busy = false;

        //    return null;
        //}

        //private async Task<object> HoldTask()
        //{

        //    var result = await _navigationService.DisplayAlert("Alert", "Setting task to hold?", "Yes", "No");

        //    if (!result)
        //        return null;
        //    Busy = true;

        //    CurrentTask.Status = (int)TaskStatusEnum.Hold;
        //    await CompleteMethod();
        //    await GetTaskMethod();
        //    return null;
        //}

        //private async Task<object> SkipTask()
        //{
        //    var result = await _navigationService.DisplayAlert("Alert", "Would you like to skip this task?", "Yes", "No");

        //    if (!result)
        //        return null;

        //    CurrentTask.Status = (int)TaskStatusEnum.Skip;
        //    await CompleteMethod();
        //    await GetTaskMethod();

        //    return null;
        //}

        //private async Task<object> NextTask()
        //{
        //    var result = await _navigationService.DisplayAlert("Alert", "Would you like to complete this task?", "Yes", "No");

        //    if (!result)
        //        return null;

        //    CurrentTask.Status = (int)TaskStatusEnum.Complete;
        //    await CompleteMethod();
        //    await GetTaskMethod();

        //    return null;
        //}

        #endregion

        #region Methods
        //private async Task CompleteMethod()
        //{
        //    try
        //    {

        //        var result = await pickDataStore.CompleteItemsAsync(CurrentTask, "80");

        //        var status = result.IsException == true ? "Error" : "Info";

        //        Busy = false;

        //        await _navigationService.DisplayAlert(status, result.Message, "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _navigationService.DisplayAlert("Alert", ex.Message, "Ok");
        //    }

        //}

        //private async Task GetTaskMethod()
        //{
        //    var result = await pickDataStore.GetItemAsync(_modelSearch, "80");

        //    if (result.IsException)
        //    {
        //        await _navigationService.DisplayAlert("Alert.", result.Message, "Ok");
        //        await _navigationService.PopAsync();
        //    }
        //    if (result.Status)
        //    {
        //        await _navigationService.DisplayAlert("Info", "Loading next Task", "Ok");

        //        var jObject = JObject.Parse(result.Data.ToString());
                
        //        CurrentTask = jObject.ToObject<TaskModel>(); 
        //    }
        //}

        //private async Task ExitTaskMethod()
        //{
        //    var exitResult = await pickDataStore.ExitItemAsync(CurrentTask, "80");
        //    if (exitResult.IsException)
        //    {
        //        await _navigationService.DisplayAlert("Alert.", exitResult.Message, "Ok");
        //        await _navigationService.PopAsync();
        //    }

        //    if (exitResult.Status)
        //    {
        //        await _navigationService.DisplayAlert("Alert.", exitResult.Message, "Ok");
        //    }
        //}
        #endregion
    }
}
