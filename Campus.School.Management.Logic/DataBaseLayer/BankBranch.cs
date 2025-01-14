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
    
    public partial class BankBranch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankBranch()
        {
            this.AccountEmployeesSalaries = new HashSet<AccountEmployeesSalary>();
            this.AccountEmployeesMonthlySalaries = new HashSet<AccountEmployeesMonthlySalary>();
            this.AccountchequePayables = new HashSet<AccountchequePayable>();
            this.Accountchequesreceivables = new HashSet<Accountchequesreceivable>();
            this.AccountCustomers = new HashSet<AccountCustomer>();
            this.AccountSuppliers = new HashSet<AccountSupplier>();
        }
    
        public int ID { get; set; }
        public string BranchName { get; set; }
        public string BranchNameAr { get; set; }
        public int BankID { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhone { get; set; }
        public string BranchMobile { get; set; }
        public string BranchFax { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> AccountTreeID { get; set; }
        public int CompanyID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountEmployeesSalary> AccountEmployeesSalaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountEmployeesMonthlySalary> AccountEmployeesMonthlySalaries { get; set; }
        public virtual AdmCompany AdmCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountchequePayable> AccountchequePayables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accountchequesreceivable> Accountchequesreceivables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountCustomer> AccountCustomers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountSupplier> AccountSuppliers { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
