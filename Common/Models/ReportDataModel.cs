using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class ReportDataModel
    {
        public double Latitud { get; set; }

        public double Longitud { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public Guid ReportTypeId { get; set; }

        public string ProposedReportType { get; set; }

        public Guid CCUserId { get; set; }

        public Nullable<decimal> Funds { get; set; }

        public string ErrorAddingReport { get; set; }
    }
}
