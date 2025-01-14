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
    
    public partial class EmployeeBenfitsOther
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeBenfitsOther()
        {
            this.EmployeeAddedBenefitsOthers = new HashSet<EmployeeAddedBenefitsOther>();
        }
    
        public int Id { get; set; }
        public string Name_AR { get; set; }
        public string Name_En { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int AccountTreeID { get; set; }
        public string Formula { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public int SchoolID { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Percentage { get; set; }
    
        public virtual AccountTree AccountTree { get; set; }
        public virtual EmployeeBenfitsOther EmployeeBenfitsOthers1 { get; set; }
        public virtual EmployeeBenfitsOther EmployeeBenfitsOther1 { get; set; }
        public virtual EmployeeBenefitsOthersType EmployeeBenefitsOthersType { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAddedBenefitsOther> EmployeeAddedBenefitsOthers { get; set; }
    }
}