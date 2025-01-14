using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class TransportBusesViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "BusNameEn", ResourceType = typeof(Transport))]
        public string BusNameEn { get; set; }

        [Required]
        [Display(Name = "BusNameAr", ResourceType = typeof(Transport))]
        public string BusNameAr { get; set; }
        [Required]
        [Display(Name = "BusNumber", ResourceType = typeof(Transport))]
        public int BusNumber { get; set; }
        [Required]
        [Display(Name = "Driver", ResourceType = typeof(Transport))]
        public int Driver { get; set; }

        public string DriverName { get; set; }



        [Required]
        [Display(Name = "Supervisor", ResourceType = typeof(Transport))]
        public int Supervisor { get; set; }

        public string SupervisorName { get; set; }

        [Required]
        [Display(Name = "TotalSeats", ResourceType = typeof(Transport))]
        public int TotalSeats { get; set; }
        [Required]
        [Display(Name = "FarestPoint", ResourceType = typeof(Transport))]
        public string FarestPoint { get; set; }
        [Required]
        [Display(Name = "FarestPointCost", ResourceType = typeof(Transport))]
        public decimal FarestPointCost { get; set; }

        [Display(Name = "FarestPointDiscount", ResourceType = typeof(Transport))]
        public Nullable<decimal> FarestPointDiscount { get; set; }
        public string NearestPoint { get; set; }
        public decimal NearestPointCost { get; set; }
        public Nullable<decimal> NearestPointDiscount { get; set; }
        public string MidPoint { get; set; }
        public decimal MidPointCost { get; set; }
        public Nullable<decimal> MidPointDiscount { get; set; }
      
        [Display(Name = "AvailableSeats", ResourceType = typeof(Transport))]
        public Nullable<int> AvaliableSeats { get; set; }
       
        [Display(Name = "ManufacturerName", ResourceType = typeof(Transport))]
        public string ManufacturerName { get; set; }
      
        [Display(Name = "Model", ResourceType = typeof(Transport))]
        public string Model { get; set; }

        [Required]
        [Display(Name = "LicenseNumber", ResourceType = typeof(Transport))]
        public int LicenseNumber { get; set; }

        [Required]
        [Display(Name = "LicenseExpiryDate", ResourceType = typeof(Transport))]
        public System.DateTime LicenseExpiryDate { get; set; }
        public string PlateNumber { get; set; }
    }
}
