using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Data.Services;
using MobilityDC.Models;
using MobilityDC.Models.Commons;
using MobilityDC.Models.DTO;
using MobilityDC.Models.Enums;
using MobilityDC.Services.NavigationService;
using MobilityDC.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class BulkPickViewModel : BaseViewModel
    {
        private INavigationService _navigationService { get; set; }

        private GetNextTaskModel _currentTask;
        private int totalQty = 0;
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
                ValidateSkuCode();

            }
        }

        //PickModelSearch _modelSearch;

        public ICommand NextCommand { get; set; }
        public ICommand SkipCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HoldCommand { get; set; }

        //PickDataStore pickDataStore;

        public BulkPickViewModel(INavigationService navigationService, GetNextTaskModel bulkTask)
        {
            _navigationService = navigationService;
            CurrentTask = bulkTask;

            AppSession.ExecutingTaskId = CurrentTask.TaskDetailID;
            AppSession.LastRoleId = CurrentTask.LastRoleID;
            AppSession.CurrentWaveId = CurrentTask.WaveID;
            AppSession.IsHireWave = CurrentTask.IsHire;


            BackCommand = new Command(() => Back(true));
            NextCommand = new Command(() => NextTask());

            //HoldCommand = new Command(async () => await HoldTask());

            //SkipCommand = new Command(async () => await SkipTask());
        }

        #region Commands Methods
        private void Back( bool goToBulkSearch)
        {

            try
            {
                List<int> TaskIds = new List<int>();

                if (AppSession.TaskIds != null && AppSession.TaskIds.Count > 0)
                {
                    TaskIds = AppSession.TaskIds;
                }

                if (AppSession.ExecutingTaskId != 0)
                {
                    TaskIds.Add(AppSession.ExecutingTaskId);
                }

                Result result = ConnectionService.ExitCurrentTask(
                            new GetNextTaskModel
                            {
                                execTaskId = TaskIds,
                                LastRoleID = AppSession.LastRoleId == 0 ? 0 : AppSession.LastRoleId,
                                LastUserID = AppSession.UserId == 0 ? 0 : AppSession.UserId,
                                WaveID = AppSession.CurrentWaveId == 0 ? 0 : AppSession.CurrentWaveId,
                                IsHire = AppSession.IsHireWave ? false : AppSession.IsHireWave
                            });


                if (result.Status)
                {
                    AppSession.ExecutingTaskId = 0;
                    AppSession.LastRoleId = 0;
                    AppSession.CurrentWaveId = 0;
                    if (goToBulkSearch)
                        _navigationService.PushAsync(new BulkPickSearchPage());
                }
            }
            catch (Exception ex)
            {
                _navigationService.DisplayAlert("Task Error", ex.Message, "Ok");
            }
        }


        private void NextTask()
        {

            ActionTask("0");
        }

        #endregion

        #region Methods
        private void ActionTask(string status)
        {
            try
            {
                if (status == "0")
                {

                    if (AppSession.BulkPickByQty)
                    {
                        totalQty = CurrentTask.Quantity;
                    }

                    if (totalQty > 0)
                    {
                        this.CurrentTask.Quantity = Quantity;
                    }
                    else
                    {
                        _navigationService.DisplayAlert("Task Error", "Quantity cannot be 0", "Ok");
                        return;
                    }


                    this.CurrentTask.TaskWasWIP = false;
                }

                Result result = null;
                if (String.IsNullOrEmpty(CurrentTask.FromLocationCode))
                {
                    _navigationService.DisplayAlert("Task Error", "From Location Code cannot be empty!", "Ok");
                    return;
                }


                if (String.IsNullOrEmpty(CurrentTask.ToLocationCode))
                {
                     _navigationService.DisplayAlert("Task Error", "To Location Code cannot be empty!", "Ok");
                }

                Result validUser = ConnectionService.ValidateUsertask(AppSession.UserId, this.CurrentTask.TaskDetailID);
                if (!validUser.Status)
                {
                    _navigationService.DisplayAlert("Task User Error", "User is not connected to backend system. You will be logged off", "Ok");
                    Back(false);
                    _navigationService.RootPage(new LoginPage());
                    return;
                }

                result = ConnectionService.CompleteBulkPickTask(CurrentTask, AppSession.UserId,
                                                                         status,
                                                                         this.CurrentTask.TaskDetailID);

                if (result.Status)
                {

                    this.CurrentTask = JsonConvert.DeserializeObject<GetNextTaskModel>(result.Data.ToString());
                    AppSession.ExecutingTaskId = CurrentTask.TaskDetailID;
                    AppSession.LastRoleId = CurrentTask.LastRoleID;
                    AppSession.CurrentWaveId = CurrentTask.WaveID;
                    AppSession.IsHireWave = CurrentTask.IsHire;
                    switch (status)
                    {
                        default:
                            _navigationService.DisplayAlert("Task Complete", "Task has been completed!", "Ok");
                            break;
                    }

                    if (!AppSession.BulkPickByQty)
                    {
                        totalQty = 0;
                        SkuCode = String.Empty;
                        Quantity = 0;
                    }
                    else
                    {
                        if (AppSession.CompanyId == "FTS")
                        {
                            totalQty = 0;
                            SkuCode = String.Empty;
                            Quantity = 0;
                        }
                        else
                        {
                            Quantity = 0;
                        }
                    }
                }
                else
                {
                    AppSession.ExecutingTaskId = 0;
                    AppSession.LastRoleId = 0;
                    AppSession.CurrentWaveId = 0;


                    if (result.Message.Contains("No more tasks for") || result.Message.ToLower().Contains("no bulk picks available."))
                    {
                         _navigationService.DisplayAlert("No Tasks", result.Message, "Ok");
                         _navigationService.PushAsync(new BulkPickSearchPage());
                    }
                    else
                    {
                         _navigationService.DisplayAlert("Task Error", result.Message, "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                _navigationService.DisplayAlert("Task Error", ex.Message, "Ok");
            }
        }
        private void ValidateSkuCode()
        {
            if (String.IsNullOrEmpty(SkuCode)) return;
            bool ValidSKU;

            if (CurrentTask.BarCodes == null)
            {
                if (CurrentTask.FormattedSKU.Replace("-", "").ToUpper() == SkuCode.ToUpper())
                {
                    ValidSKU = true;
                }
                else
                {
                    ValidSKU = CurrentTask.FormattedSKU.ToUpper() == SkuCode.ToUpper() || CurrentTask.FormattedSKU.Replace("-", "").ToUpper() == SkuCode.ToUpper();
                }
            }
            else
            {
                ValidSKU = CurrentTask.BarCodes.Contains(SkuCode) || CurrentTask.FormattedSKU.ToUpper() == SkuCode.ToUpper() || CurrentTask.FormattedSKU.Replace("-", "").ToUpper() == SkuCode.ToUpper();
            }


            if (!ValidSKU)
            {
                Result result = ConnectionService.GetSKUForBarcode(SkuCode, AppSession.UserId);
                ValidSKU = result.Status;
                if (ValidSKU)
                {
                    ValidSKU = (result.Message == CurrentTask.Barcode);
                }
            }

            if (ValidSKU)
            {

                if (!AppSession.BulkPickByQty)
                {
                    ++totalQty;
                    if (totalQty < CurrentTask.Quantity)
                    {
                        Quantity = totalQty;
                        SkuCode = String.Empty;
                    }
                    else
                    {
                        if (totalQty == CurrentTask.Quantity)
                        {
                            Quantity = totalQty;
                            ActionTask("0");
                        }
                    }
                }
            }
            else
            {
                SkuCode = String.Empty;
                _navigationService.DisplayAlert("Task Error", "Sku code or barcode is not valid!", "Ok");
            }
        }

        #endregion

    }
}
