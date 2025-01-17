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
    
    public partial class AdmInitialStudRegistration
    {
        public long id { get; set; }
        public int GradeID { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public System.DateTime DOB { get; set; }
        public string BirthPlace { get; set; }
        public int Nationality { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string LastSchool { get; set; }
        public int CreatedbyID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public int SchoolID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Result { get; set; }
        public bool isRegistered { get; set; }
    
        public virtual AdmGradesSchool AdmGradesSchool { get; set; }
        public virtual Nationality Nationality1 { get; set; }
    }
}
