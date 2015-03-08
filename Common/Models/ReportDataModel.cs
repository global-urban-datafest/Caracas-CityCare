using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class ReportDataModel
    {
        public string SiteAddress { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public System.Guid ReportTypeId { get; set; }
        public System.DateTime Date { get; set; }
        public System.Guid CCUserId { get; set; }
        public Nullable<decimal> Funds { get; set; }
        public bool IsValid { get; set; }
        public string RejectReason { get; set; }
    }
}
