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
    
    public partial class HolidayType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HolidayType()
        {
            this.EmployeeVacations = new HashSet<EmployeeVacation>();
        }
    
        public int HolidayID { get; set; }
        public string HolidayNameAr { get; set; }
        public string HolidayNameEn { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyID { get; set; }
        public decimal Rate { get; set; }
        public string Notes { get; set; }
        public int Days { get; set; }
    
        public virtual AdmCompany AdmCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeVacation> EmployeeVacations { get; set; }
    }
}
