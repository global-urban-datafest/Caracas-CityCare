using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityCare.Areas.Service.Models
{
    public enum ResponseStatus
    {
        Success,
        Error,
    }

    public class RequestResponseModel
    {
        public ResponseStatus Status { get; set; }

        public string ErroMessage { get; set; }
    }
}