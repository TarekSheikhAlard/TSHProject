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
    
    public partial class BusExpens
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusExpens()
        {
            this.BusExpensesItems = new HashSet<BusExpensesItem>();
        }
    
        public int ID { get; set; }
        public Nullable<int> BusID { get; set; }
        public int Employee_ID { get; set; }
        public string SerialNo { get; set; }
        public string DocumentNumber { get; set; }
        public decimal Total { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public string Place { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> YearID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusExpensesItem> BusExpensesItems { get; set; }
        public virtual Bus Bus { get; set; }
        public virtual AdmEmployee AdmEmployee { get; set; }
        public virtual AdmCompany AdmCompany { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
    }
}
