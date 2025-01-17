//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Campus.School.Management.Logic.DataBaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountNotesReceivable
    {
        public int ID { get; set; }
        public Nullable<int> InvoiceCode { get; set; }
        public string docementNumber { get; set; }
        public Nullable<int> AccountTreeID { get; set; }
        public string AccountTreeCode { get; set; }
        public Nullable<int> costcenterID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> TreasuryID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string description { get; set; }
        public bool IsRelay { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> PhysicalYearID { get; set; }
    
        public virtual AccountCostCenter AccountCostCenter { get; set; }
        public virtual AccountTreasury AccountTreasury { get; set; }
        public virtual AdmCompany AdmCompany { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
        public virtual PhysicalYear PhysicalYear { get; set; }
        public virtual AccountTree AccountTree { get; set; }
    }
}
