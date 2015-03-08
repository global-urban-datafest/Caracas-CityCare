using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class ResponseReportDataModel:ResponseBaseModel
    {
        public List<ReportDataModel> fReportModels { get; set; }
        public bool fAreReportsInvalid { get; set; }
    }
}
