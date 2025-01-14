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
  public   class StudentFeesTypeViewModel
    {
        [Display(Name = "ID", ResourceType = typeof(StudentFeesType))]
        public int ID { get; set; }


        [Required(ErrorMessageResourceName = "AdditionalfeesError", ErrorMessageResourceType = typeof(StudentFeesType))]
        [Display(Name = "Additionalfees", ResourceType = typeof(StudentFeesType))]
        public string Additionalfees { get; set; }

       

    }
}
