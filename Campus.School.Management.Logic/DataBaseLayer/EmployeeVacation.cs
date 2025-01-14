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
    
    public partial class EmployeeVacation
    {
        public int ID { get; set; }
        public int Employee_ID { get; set; }
        public int HolidayTypeId { get; set; }
        public System.DateTime DateOfVacation { get; set; }
        public System.DateTime LastWorkingDate { get; set; }
        public int EmployeeSalaryId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public string ContactOnVacation { get; set; }
        public Nullable<int> EmployeeInCharge { get; set; }
    
        public virtual AccountEmployeesMonthlySalary AccountEmployeesMonthlySalary { get; set; }
        public virtual HolidayType HolidayType { get; set; }
        public virtual AdmEmployee AdmEmployee { get; set; }
    }
}
