using System;
using System.Net.Http;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models.Commons;
using MobilityDC.Models.DTO;
using Newtonsoft.Json;

namespace MobilityDC.Services.Data
{
    public class LoginService
    {
        private static readonly int Timeout = 1 * 60000;
        HttpClient _client;

        

        public LoginService()
        {
            _client = new HttpClient();
        }

        public AuthResult SignInAsync(LoginModel loginModel)
        {
            AppSession.EquipmentID = loginModel.DeviceNumber;

            AuthResult logonResult = new AuthResult();

            logonResult = ValidateInputResult(loginModel);

            if (!logonResult.Status)
            {
                logonResult.Message = logonResult.Message;
                return logonResult;
            }

            logonResult = ConnectionService.QuartzLogin(loginModel.UserId, loginModel.Password, loginModel.DeviceNumber);

            if (logonResult.Status)
            {
                AppSession.AccessToken = logonResult.access_token;
            }
            else
            {
                if (logonResult.MessageToken == "access_denied")
                {
                    logonResult.Message = "Username or password is incorrect";
                    logonResult.Status = false;
                    return logonResult;
                }
                else if (logonResult.MessageToken == "invalid_comid")
                {
                    logonResult.Message = "The company ID in the settings file is incorrect";
                    logonResult.Status = false;
                    return logonResult;
                }
                else if (logonResult.MessageToken == "new_client")
                {
                    logonResult.Message = "Scanner not authorized";              
                    logonResult.Status = false;
                    return logonResult;
                }
                else if (logonResult.MessageToken == "invalid_client")
                {
                    logonResult.Message = "The saved client ID is incorrect.";
                    logonResult.Status = false;
                    return logonResult;
                }
                else if (logonResult.MessageToken == "duplicate_client")
                {
                    logonResult.Message = "The client ID specified already exists in the database and cannot be registered.";
                    logonResult.Status = false;
                    return logonResult;
                }
                else if (logonResult.MessageToken == "invalid_grant")
                {
                    logonResult.Message = "You not authorized to log in.";
                    logonResult.Status = false;
                    return logonResult;
                }               
            }


            Result result = ConnectionService.GetUser(loginModel.UserId, loginModel.Password, loginModel.DeviceNumber);

            if (result.Status)
            {
                User user = JsonConvert.DeserializeObject<User>(result.Data.ToString());
                AppSession.UserId = user.UserId;
                AppSession.UserName = user.UserName;
                AppSession.ScannerId = user.EquipmentId;
                AppSession.ExecutingTaskId = 0;
                AppSession.CurrentWaveId = 0;
                AppSession.MoveToException = user.MoveToException;

                logonResult.Status = true;
            }
            else
            {
                logonResult.Status = false;
                logonResult.Message = result.Message;
            }

            return logonResult;
        }


        private AuthResult ValidateInputResult(LoginModel loginModel)
        {
            var result = new AuthResult();
            result.Status = true;
            if (string.IsNullOrEmpty(loginModel.DeviceNumber))
            {
                result.Status = false;
                result.Message = "Please enter a device ID.";
            }

            if (string.IsNullOrEmpty(loginModel.UserId.ToString()))
            {
                result.Status = false;
                result.Message = "Please enter an user ID.";
            }

            return result;
        }




    }
}
