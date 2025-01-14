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
     public class BusStudentOccupationViewModel
    { 
        public int ID { get; set; }

        
        public long Ref_Id { get; set; }


        [Required(ErrorMessageResourceName = "Bus_IDError", ErrorMessageResourceType = typeof(BusStudentOccupations))]
        [Display(Name = "Bus_ID", ResourceType = typeof(BusStudentOccupations))]
        public int BusID { get; set; }

    
      
        public bool MorningTrip { get; set; }

     
 
        public bool EviningTrip { get; set; }
    }
}
