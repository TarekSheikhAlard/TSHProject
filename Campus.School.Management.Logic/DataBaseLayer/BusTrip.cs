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
    
    public partial class BusTrip
    {
        public int BusTripID { get; set; }
        public Nullable<int> BusID { get; set; }
        public string BusStation { get; set; }
        public string Notes { get; set; }
        public string PickUpTime { get; set; }
        public string DropTime { get; set; }
        public string TripType { get; set; }
        public Nullable<int> SchoolID { get; set; }
    
        public virtual Bus Bus { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
    }
}
