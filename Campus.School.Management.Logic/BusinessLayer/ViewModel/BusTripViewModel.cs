using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class BusTripViewModel
    {
        [Display(Name = "BusTripID", ResourceType = typeof(BusTrips))]
        public int BusTripID { get; set; }

    
        [Display(Name = "BusID", ResourceType = typeof(BusTrips))]
        public Nullable<int> BusID { get; set; }

        [Display(Name = "BusStation", ResourceType = typeof(BusTrips))]
        public string BusStation { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(BusTrips))]
        public string Notes { get; set; }

        [Display(Name = "PickUpTime", ResourceType = typeof(BusTrips))]
        public string PickUpTime { get; set; }

        [Display(Name = "DropTime", ResourceType = typeof(BusTrips))]
        public string DropTime { get; set; }

        [Display(Name = "TripType", ResourceType = typeof(BusTrips))]
        public string TripType { get; set; }

        [Display(Name = "BusNo", ResourceType = typeof(BusTrips))]
        public string BusNo { get; internal set; }
    }
}
