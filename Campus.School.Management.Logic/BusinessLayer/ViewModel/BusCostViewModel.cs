using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public  class BusCostViewModel
    {
 


        [Display(Name = "ID", ResourceType = typeof(BusCost))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "BusError", ErrorMessageResourceType = typeof(BusCost))]
        [Display(Name = "Bus", ResourceType = typeof(BusCost))]  
        public int BusID { get; set; }

 
        [Display(Name = "BusNumber", ResourceType = typeof(BusCost))]
        public string BusNo { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "BusSingleTripError", ErrorMessageResourceType = typeof(BusCost))]
        [Display(Name = "BusSingleTrip", ResourceType = typeof(BusCost))]
        public Nullable<decimal> SingleTripCost { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "BusDoubleTripError", ErrorMessageResourceType = typeof(BusCost))]
        [Display(Name = "BusDoubleTrip", ResourceType = typeof(BusCost))]
        public Nullable<decimal> DoubleTripCost { get; set; }

        [Required(ErrorMessageResourceName = "BusStationError", ErrorMessageResourceType = typeof(BusCost))]
        [Display(Name = "BusStation", ResourceType = typeof(BusCost))]

         public string Station { get; set; }






    }
}
