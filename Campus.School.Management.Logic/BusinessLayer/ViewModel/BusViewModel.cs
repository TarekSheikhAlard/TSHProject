using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class BusViewModel
    {

     

        [Display(Name = "BusID", ResourceType = typeof(Buses))] 
        public int BusID { get; set; }

        [Required(ErrorMessageResourceName = "BusNoError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "BusNo", ResourceType = typeof(Buses))]

        public string BusNo { get; set; }

        [Required(ErrorMessageResourceName = "BusTypeError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "BusType", ResourceType = typeof(Buses))]
        public string BusType { get; set; }


        [Required(ErrorMessageResourceName = "ManufacturError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "Manufactur", ResourceType = typeof(Buses))]
        public string Manufactur { get; set; }

        [Required(ErrorMessageResourceName = "ModelError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "Model", ResourceType = typeof(Buses))]
        public string Model { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "YearError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "Year", ResourceType = typeof(Buses))]
        public string Year { get; set; }

        [Required(ErrorMessageResourceName = "ChassisNoError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "ChassisNo", ResourceType = typeof(Buses))]
        public string ChassisNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "LicenseStartError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "LicenseStart", ResourceType = typeof(Buses))]
        public DateTime? LicenseStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "LicenseEndError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "LicenseEnd", ResourceType = typeof(Buses))]
        public DateTime? LicenseEnd { get; set; }

        [Required(ErrorMessageResourceName = "IssuedFromError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "IssuedFrom", ResourceType = typeof(Buses))]
        public string IssuedFrom { get; set; }

        [Required(ErrorMessageResourceName = "LicenseNo", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "LicenseNo", ResourceType = typeof(Buses))]
        public string LicenseNo { get; set; }

        [Required(ErrorMessageResourceName = "PassengersNumberError", ErrorMessageResourceType = typeof(Buses))]
        [Display(Name = "PassengersNumber", ResourceType = typeof(Buses))]
        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        public int? PassengersNumber { get; set; }

        public ICollection<BusDetailsViewModel> _BusDetails { get; set; }

        public ICollection<BusTripViewModel> _BusTrips { get; set; }

      
    }
}
