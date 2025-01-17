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
    
    public partial class AdmSchool
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdmSchool()
        {
            this.SchoolUsers = new HashSet<SchoolUser>();
            this.AccountEmployeesMonthlySalaries = new HashSet<AccountEmployeesMonthlySalary>();
            this.AccountEmployeesSalaries = new HashSet<AccountEmployeesSalary>();
            this.BusTrips = new HashSet<BusTrip>();
            this.AdmDepartments = new HashSet<AdmDepartment>();
            this.AdmEmployees = new HashSet<AdmEmployee>();
            this.AccountSalaryItems = new HashSet<AccountSalaryItem>();
            this.AccountEmployeeSalaryItems = new HashSet<AccountEmployeeSalaryItem>();
            this.PhysicalYears = new HashSet<PhysicalYear>();
            this.AccountCostCenters = new HashSet<AccountCostCenter>();
            this.AccBusCosts = new HashSet<AccBusCost>();
            this.AccountDailyJournals = new HashSet<AccountDailyJournal>();
            this.AdmAcademicYears = new HashSet<AdmAcademicYear>();
            this.Buses = new HashSet<Bus>();
            this.BusExpenses = new HashSet<BusExpens>();
            this.AccountAssets = new HashSet<AccountAsset>();
            this.AccountchequePayables = new HashSet<AccountchequePayable>();
            this.Accountchequesreceivables = new HashSet<Accountchequesreceivable>();
            this.AccountCustomers = new HashSet<AccountCustomer>();
            this.AccountCustomerTransactions = new HashSet<AccountCustomerTransaction>();
            this.AccountFirstBalances = new HashSet<AccountFirstBalance>();
            this.AccountNotespayables = new HashSet<AccountNotespayable>();
            this.AccountNotesReceivables = new HashSet<AccountNotesReceivable>();
            this.AccountPayableCashes = new HashSet<AccountPayableCash>();
            this.AccountReciveCashes = new HashSet<AccountReciveCash>();
            this.AccountSuppliers = new HashSet<AccountSupplier>();
            this.AccountTreasuries = new HashSet<AccountTreasury>();
            this.AssetTrees = new HashSet<AssetTree>();
            this.EmployeeAssets = new HashSet<EmployeeAsset>();
            this.InvenotryStoreKeepers = new HashSet<InvenotryStoreKeeper>();
            this.InventoryTrees = new HashSet<InventoryTree>();
            this.AdmJobs = new HashSet<AdmJob>();
            this.BusStudentOccupations = new HashSet<BusStudentOccupation>();
            this.GradesFeeConfigs = new HashSet<GradesFeeConfig>();
            this.AccStudentFees = new HashSet<AccStudentFee>();
            this.StudentReferences = new HashSet<StudentReference>();
            this.AccStudentConfigs = new HashSet<AccStudentConfig>();
            this.AccountTrees = new HashSet<AccountTree>();
            this.BankTransactions = new HashSet<BankTransaction>();
            this.EmployeeBenfitsOthers = new HashSet<EmployeeBenfitsOther>();
            this.HRPayrollEmployeeBenfitsOthers = new HashSet<HRPayrollEmployeeBenfitsOther>();
            this.HRPayrollEmployees = new HashSet<HRPayrollEmployee>();
            this.HRPayrolls = new HashSet<HRPayroll>();
        }
    
        public int SchoolID { get; set; }
        public string SchoolNameEn { get; set; }
        public string SchoolNameAr { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Mobile { get; set; }
        public string Logo { get; set; }
        public int CompanyID { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string whatsApp { get; set; }
        public string SmsSender { get; set; }
        public string PostCode { get; set; }
        public string LicenseNo { get; set; }
        public string SchSuperVision { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public string Url { get; set; }
        public string ConnectionString { get; set; }
        public string LogoPosition { get; set; }
        public string XMap { get; set; }
        public string YMap { get; set; }
    
        public virtual AdmCity AdmCity { get; set; }
        public virtual AdmCompany AdmCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolUser> SchoolUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountEmployeesMonthlySalary> AccountEmployeesMonthlySalaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountEmployeesSalary> AccountEmployeesSalaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusTrip> BusTrips { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmDepartment> AdmDepartments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmEmployee> AdmEmployees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountSalaryItem> AccountSalaryItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountEmployeeSalaryItem> AccountEmployeeSalaryItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhysicalYear> PhysicalYears { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountCostCenter> AccountCostCenters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccBusCost> AccBusCosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountDailyJournal> AccountDailyJournals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmAcademicYear> AdmAcademicYears { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bus> Buses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusExpens> BusExpenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountAsset> AccountAssets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountchequePayable> AccountchequePayables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accountchequesreceivable> Accountchequesreceivables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountCustomer> AccountCustomers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountCustomerTransaction> AccountCustomerTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountFirstBalance> AccountFirstBalances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountNotespayable> AccountNotespayables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountNotesReceivable> AccountNotesReceivables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountPayableCash> AccountPayableCashes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountReciveCash> AccountReciveCashes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountSupplier> AccountSuppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTreasury> AccountTreasuries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTree> AssetTrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvenotryStoreKeeper> InvenotryStoreKeepers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTree> InventoryTrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmJob> AdmJobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusStudentOccupation> BusStudentOccupations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradesFeeConfig> GradesFeeConfigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccStudentFee> AccStudentFees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentReference> StudentReferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccStudentConfig> AccStudentConfigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTree> AccountTrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeBenfitsOther> EmployeeBenfitsOthers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRPayrollEmployeeBenfitsOther> HRPayrollEmployeeBenfitsOthers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRPayrollEmployee> HRPayrollEmployees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRPayroll> HRPayrolls { get; set; }
    }
}
