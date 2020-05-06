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
using Xamarin.Forms;

namespace MobilityDC.ViewModels
{
    public class FinePickViewModel : BaseViewModel
    {
        private INavigationService _navigationService { get; set; }

        private GetNextTaskModel _dataContext;
        public GetNextTaskModel DataContext
        {
            get
            {
                return _dataContext;
            }
            set
            {
                SetProperty(ref _dataContext, value);
            }
        }

        private TaskModel _previousTask;
        public TaskModel PreviousTask
        {
            get
            {
                return _previousTask;
            }
            set
            {
                SetProperty(ref _previousTask, value);
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

        public string _skuCode;
        public string SkuCode
        {
            get
            {
                return _skuCode;
            }
            set
            {
                SetProperty(ref _skuCode, value);

                if (SkuCode == DataContext.FormattedSKU || SkuCode == DataContext.Barcode)
                {
                    Quantity += 1;
                    SkuCode = string.Empty;
                }

            }
        }

        FinePickModelSearch _modelSearch;

        public ICommand NextCommand { get; set; }
        public ICommand SkipCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HoldCommand { get; set; }

        PickDataStore pickDataStore;

        public FinePickViewModel(INavigationService navigationService, FinePickModelSearch modelSearch, GetNextTaskModel task)
        {
            _navigationService = navigationService;
            DataContext = task;
            _modelSearch = modelSearch;
            pickDataStore = new PickDataStore();

            NextCommand = new Command(async () => await NextTask());
            SkipCommand = new Command(async () => await SkipTask());
            BackCommand = new Command(async () => await Back());
            HoldCommand = new Command(async () => await HoldTask());

            ContextData();

        }

        #region Commands Methods
        private async Task<object> Back()
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

                Result result =
                        ConnectionService.ExitCurrentTask(
                            new GetNextTaskModel
                            {
                                execTaskId = TaskIds,
                                LastRoleID = AppSession.LastRoleId,
                                LastUserID = AppSession.UserId,
                                WaveID = AppSession.CurrentWaveId,
                                IsHire = AppSession.IsHireWave
                            });


                if (result.Status)
                {
                    AppSession.ExecutingTaskId = 0;
                    AppSession.LastRoleId = 0;

                    await _navigationService.PushAsync(new FinePickSearchPage());
                }
            }
            catch (Exception ex)
            {
                await _navigationService.DisplayAlert("Alert.", ex.Message, "Ok");
            }

            return null;
        }

        private async Task<object> HoldTask()
        {
            ActionTask("3");
            return null;

        }

        private async Task<object> SkipTask()
        {
            ActionTask("2");
            return null;
        }

        private async Task<object> NextTask()
        {
            ActionTask("0");
            return null;
        }

        #endregion

        #region Methods

        private void ActionTask(string status)
        {
            try
            {
                if (Quantity == 0 && status != "2" && status != "3")
                {
                    _navigationService.DisplayAlert("Task Error", @"Quantity is not valid!", "Ok");
                    return;
                }

                DataContext.Quantity = Quantity;
                string qty = Quantity.ToString();

                if (String.IsNullOrEmpty(DataContext.BoxNo) && status == "0")
                {
                    _navigationService.DisplayAlert("Task Error", @"Box Number is not valid!", "Ok");
                    return;
                }

                if (status == "0")
                {
                    DataContext.TaskWasWIP = false;

                }

                Result result;

                string lastPigeonHole = "";
                if (DataContext.BoxNo.Length > 4)
                {
                    lastPigeonHole = DataContext.BoxNo.Substring(DataContext.BoxNo.Length - 4, 4);
                }


                string fromLocationCode = DataContext.FromLocationCode;

                //Result validUser = ConnectionService.ValidateUsertask(AppSession.UserId, DataContext.TaskDetailID);
                //if (!validUser.Status)
                //{
                //    _navigationService.DisplayAlert("Task User Error", "User is not connected to backend system.You will be logged off", "Ok");
                //    ReSetTaskDetail();
                //    _navigationService.PushAsync(new LoginPage());
                //    return;
                //}

                result = ConnectionService.CompleteFinePickTask(DataContext, AppSession.UserId,
                                                               status,
                                                               DataContext.TaskDetailID, false);

                if (result.Status)
                {
                    
                    DataContext = JsonConvert.DeserializeObject<GetNextTaskModel>(result.Data.ToString());

                    switch (status)
                    {
                        case "2":
                            _navigationService.DisplayAlert("Skip Complete", $"{qty} units has been skipped!", "Ok");

                            break;
                        case "3":
                            _navigationService.DisplayAlert("On Hold Complete", $"{qty} units has been put on hold!", "Ok");
                            break;

                        default:
                            break;
                    }

                }
                else
                {
                    AppSession.ExecutingTaskId = 0;
                    AppSession.LastRoleId = 0;


                    if (result.Message.Contains("No more tasks for"))
                    {
                        _navigationService.DisplayAlert("No Tasks", result.Message, "Ok");
                        _navigationService.PushAsync(new FinePickSearchPage());
                    }

                    else
                    {
                        if (result.Message.Contains("This order was already place in a PigeonHole"))
                        {
                            //ViewController.ShowMessage(this, "Task Error", result.Message, true);
                            //isNewPigeonHole = true;
                            //dataContext.BoxNo = fromLocationCode + result.Data.ToString();
                            //lbPigHoleDisplay.Text = result.Data.ToString();
                            //totalQty--;
                            //txtBoxNo.Text = totalQty.ToString();
                            //txtBoxNo.Text = string.Empty;
                            //txtBoxNo.Focus();

                            _navigationService.DisplayAlert("Task Error", result.Message, "Ok");
                            _dataContext.BoxNo = fromLocationCode + result.Data.ToString();

                        }
                        else
                        {
                            _navigationService.DisplayAlert("Task Error", result.Message, "Ok");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _navigationService.DisplayAlert("Task Error", ex.Message, "Ok");
            }
        }

        public void ReSetTaskDetail()
        {
            List<int> TaskIds = new List<int>();

            if (AppSession.TaskIds != null && AppSession.TaskIds.Count > 0)
            {
                TaskIds = AppSession.TaskIds;
            }

            if (AppSession.ExecutingTaskId != null && AppSession.ExecutingTaskId != 0)
            {
                TaskIds.Add(AppSession.ExecutingTaskId);
            }

            Result result =
                    ConnectionService.ExitCurrentTask(
                        new GetNextTaskModel
                        {
                            execTaskId = TaskIds,
                            LastRoleID = AppSession.LastRoleId,
                            LastUserID = AppSession.UserId,
                            WaveID = AppSession.CurrentWaveId,
                            IsHire = AppSession.IsHireWave
                        });
            if (result.Status)
            {
                AppSession.ExecutingTaskId = 0;
                AppSession.LastRoleId = 0;
                AppSession.CurrentWaveId = 0;
            }
        }

        private void ContextData()
        {
            if (DataContext == null) return;
            AppSession.ExecutingTaskId = DataContext.TaskDetailID;
            AppSession.LastRoleId = DataContext.LastRoleID;
        }

        #endregion
    }
}
