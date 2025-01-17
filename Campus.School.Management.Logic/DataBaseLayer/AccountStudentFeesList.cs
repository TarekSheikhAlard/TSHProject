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
    
    public partial class AccountStudentFeesList
    {
        public int ID { get; set; }
        public Nullable<decimal> BooksFees { get; set; }
        public Nullable<decimal> StudyFees { get; set; }
        public Nullable<int> AcademicYearID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual AdmAcademicYear AdmAcademicYear { get; set; }
    }
}
