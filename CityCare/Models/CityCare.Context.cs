﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CityCareEntities : DbContext
    {
        public CityCareEntities()
            : base("name=CityCareEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CertificationRequest> CertificationRequest { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportSolution> ReportSolution { get; set; }
        public virtual DbSet<ReportState> ReportState { get; set; }
        public virtual DbSet<ReportType> ReportType { get; set; }
        public virtual DbSet<UserIdType> UserIdType { get; set; }
        public virtual DbSet<CCUser> CCUser { get; set; }
    }
}
