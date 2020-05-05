using System;
using System.Text;
using MobilityDC.Models.Commons;
using MobilityDC.Models.DTO;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using MobilityDC.Api.Models.DTO;
using System.Security.Cryptography.X509Certificates;

namespace MobilityDC.Models.Commons
{
    public static class ContentType
    {
        public const string JSon = "application/json";
        public const string Xml = "application/xml";
        public const string URLEncode = "application/x-www-form-urlencoded";
    }

    public enum MethodType
    {
        Post,
        Put,
        Get,
        Delete
    }

    public class ConnectionService
    {
        private static readonly int Timeout = 1 * 60000;
        public static string Server = Constants.ApiUrl;
        public class AuthPolicy
        {
            public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
            {
                //Return True to force the certificate to be accepted.
                return true;
            }
        }

        public static Result GetUser(string userId, string password, string scannerId)
        {
            return Connect(new User { Password = password }, MethodType.Post, $"{Server}enquiry/user/validate/{userId}/{scannerId}");
        }

        public static Result Connect(object body, MethodType method, string route)
        {
            switch (method)
            {
                case MethodType.Post:
                    return HttpPost(body, route);
                //case MethodType.Get:
                //    return HttpGet(route);
                default:
                    return new Result { Status = false, Message = "Http method(enum) not found!" };
            }
        }

        static Result HttpPost(object body, string route)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(Uri.EscapeUriString(route));
                request.Timeout = Timeout;
                // Set the Method property of the request to POST.
                request.Method = MethodType.Post.ToString().ToUpper();


                if (!String.IsNullOrEmpty(AppSession.AccessToken))
                {
                    request.Headers.Add("Authorization", String.Format("bearer {0}", AppSession.AccessToken));
                }

                string postData = JsonConvert.SerializeObject(body);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = ContentType.JSon;
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                // Create POST data and convert it to a byte array.
                Stream dataStream = request.GetRequestStream();


                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);


                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                Result result = new Result();
                result = JsonConvert.DeserializeObject<Result>(responseFromServer);


                reader.Close();
                dataStream.Close();
                response.Close();

                return result;
            }
            catch (Exception ex)
            {
                return new Result { Status = false, Message = ex.Message };
            }
        }

        public static AuthResult QuartzLogin(string userName, string userPassword, string clientId)
        {
            try
            {
                string route = $"{Constants.ServerAuth}/authentication/token";
                               
                var request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(route));
               
                request.ServerCertificateValidationCallback +=
                    (sender, cert, chain, error) =>
                    {
                        return true;
                    };

                request.Timeout = Timeout;

                string authParams = "";
                authParams = $"{authParams}grant_type=password&";
                authParams = $"{authParams}username={ userName}&";
                authParams = $"{authParams}password={userPassword}&";
                authParams = $"{authParams}comid={AppSession.CompanyId}&";
                authParams = $"{authParams}client_id={clientId}&";
                authParams = $"{authParams}client_secret={"40801149"}";

                request.Method = MethodType.Post.ToString().ToUpper();


                request.ContentType = ContentType.URLEncode;
                byte[] byteArray = Encoding.UTF8.GetBytes(authParams);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                AuthResult result = new AuthResult();
                result = JsonConvert.DeserializeObject<AuthResult>(responseFromServer);
                result.Status = true;

                reader.Close();
                dataStream.Close();
                response.Close();

                return result;
            }
            catch (WebException wex)
            {                
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                            AuthFailureResult authFail = JsonConvert.DeserializeObject<AuthFailureResult>(error);

                            return new AuthResult { Status = false, Message = authFail.Error_Description, MessageToken = authFail.Error };
                        }
                    }
                }
                return new AuthResult { Status = false, Message = "Fatal login error occured." };
            }
            catch (Exception ex)
            {
                return new AuthResult { Status = false, Message = ex.Message };
            }
        }

        public static Result SearchBulkPick(BulkPickModelSearch searchModel, int userId)
        {
            return Connect(searchModel, MethodType.Post, String.Format("{0}bulkpick/search/{1}", Server, userId));
        }


    }
}
