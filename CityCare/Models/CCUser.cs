//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CityCare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CCUser
    {
        public CCUser()
        {
            this.Report = new HashSet<Report>();
            this.ReportSolution = new HashSet<ReportSolution>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserLevel { get; set; }
        public string PersonalId { get; set; }
        public System.Guid PersonalIdTypeId { get; set; }
        public bool IsCertifierUser { get; set; }
        public bool RequestGetCertified { get; set; }
        public Nullable<System.Guid> CertificationRequestId { get; set; }
    
        public virtual CertificationRequest CertificationRequest { get; set; }
        public virtual UserIdType UserIdType { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<ReportSolution> ReportSolution { get; set; }
    }
}