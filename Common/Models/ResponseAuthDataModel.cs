using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class ResponseAuthDataModel : ResponseBaseModel
    {
        public bool IsValidData { get; set; }

        public Guid fUserId { get; set; }
    }
}
