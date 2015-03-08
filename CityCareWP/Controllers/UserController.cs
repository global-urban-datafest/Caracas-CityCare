using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using CityCareWP.Utils;
using Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using HttpClient = System.Net.Http.HttpClient;


namespace CityCareWP
{

    //PostValidateUser delegates
    public delegate void PostValidateUserSucceed(object source, PostValidateUserSucceedEventArgs e);
    public class PostValidateUserSucceedEventArgs : EventArgs
    {
        public bool _IsValidCrecentials { get; set; }
        public ResponseAuthDataModel fResponseAuthDataModel { get; set; }
    }

    public delegate void PostValidateUserFailed(object source, PostValidateUserFailedEventArgs e);

    public class PostValidateUserFailedEventArgs : EventArgs
    {
        public string _ErrorMessage { get; set; }
    }

    public class UserController
    {
        //Validate Credentials events
        public event PostValidateUserSucceed OnPostValidateUserSucceed;
        public event PostValidateUserFailed OnPostValidateUserFailed;

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="aUserAuthDataModel">The new user to register</param>
        public async void PostValidateUser(UserAuthDataModel aUserAuthDataModel)
        {
            try
            {
                var _RequestUri = new Uri(WebApiRequests.PostValidateUser, UriKind.Absolute);
                var _JsonData = JsonConvert.SerializeObject(aUserAuthDataModel);

                using (var _HttpClient = new HttpClient())
                {
                    _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var _Response = await _HttpClient.PostAsync(_RequestUri, new StringContent(_JsonData, Encoding.UTF8, "application/json"));
     
                    if (_Response.IsSuccessStatusCode)
                    {
                        OnPostValidateUserSucceed(this, new PostValidateUserSucceedEventArgs());
                    }
                    else
                    {
                        var _ErrorMessage = await _Response.Content.ReadAsStringAsync();
                        Debug.WriteLine(_ErrorMessage);
                        OnPostValidateUserFailed(this, new PostValidateUserFailedEventArgs { _ErrorMessage = "Comunication error, please tray again" });
                    }
                   
                }
            }
            catch (Exception _Exception)
            {
                Debug.WriteLine(_Exception.Message);
                OnPostValidateUserFailed(this, new PostValidateUserFailedEventArgs { _ErrorMessage = "Comunication error, please tray again" });
            }
        }








    }


}
