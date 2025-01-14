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
    
    public partial class AccountCustomerTransaction
    {
        public int ID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> AccountTreeID { get; set; }
        public Nullable<decimal> Depit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> OperaionDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual AccountCustomer AccountCustomer { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
        public virtual AccountTree AccountTree { get; set; }
    }
}
