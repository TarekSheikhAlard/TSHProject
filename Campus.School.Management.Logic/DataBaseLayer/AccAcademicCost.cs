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
    
    public partial class AccAcademicCost
    {
        public int ID { get; set; }
        public Nullable<int> Academic_YearID { get; set; }
        public Nullable<decimal> Year_Cost { get; set; }
    
        public virtual AdmAcademicYear AdmAcademicYear { get; set; }
    }
}
