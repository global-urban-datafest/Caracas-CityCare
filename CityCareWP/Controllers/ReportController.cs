using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CityCareWP.Utils;
using Common.Models;
using Newtonsoft.Json;

namespace CityCareWP.Controllers
{

    //PostValidateUser delegates
    public delegate void PostAddReportsSucceed(object source, PostAddReportsSucceedEventArgs e);
    public class PostAddReportsSucceedEventArgs : EventArgs
    {
        public bool _IsValidCrecentials { get; set; }
        public ResponseAuthDataModel fResponseAuthDataModel { get; set; }
    }

    public delegate void PostAddReportsFailed(object source, PostAddReportsFailedEventArgs e);

    public class PostAddReportsFailedEventArgs : EventArgs
    {
        public string _ErrorMessage { get; set; }
    }
    class ReportController
    {
        //Validate Credentials events
        public event PostAddReportsSucceed OnPostAddReportsSucceed;
        public event PostAddReportsFailed OnPostAddReportsFailed;

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="aReportDataModel">The new user to register</param>
        public async void PostAddReports(ReportDataModel aReportDataModel)
        {
            try
            {
                var _RequestUri = new Uri(WebApiRequests.PostAddReports, UriKind.Absolute);
                var _JsonData = JsonConvert.SerializeObject(aReportDataModel);

                using (var _HttpClient = new HttpClient())
                {
                    _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var _Response = await _HttpClient.PostAsync(_RequestUri, new StringContent(_JsonData, Encoding.UTF8, "application/json"));

                    if (_Response.IsSuccessStatusCode)
                    {
                        OnPostAddReportsSucceed(this, new PostAddReportsSucceedEventArgs());
                    }
                    else
                    {
                        var _ErrorMessage = await _Response.Content.ReadAsStringAsync();
                        Debug.WriteLine(_ErrorMessage);
                        OnPostAddReportsFailed(this, new PostAddReportsFailedEventArgs { _ErrorMessage = "Comunication error, please tray again" });
                    }

                }
            }
            catch (Exception _Exception)
            {
                Debug.WriteLine(_Exception.Message);
                OnPostAddReportsFailed(this, new PostAddReportsFailedEventArgs { _ErrorMessage = "Comunication error, please tray again" });
            }
        }


    }
}
