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
    public class AspNetRolesViewModel
    {

      
        public string ID { get; set; }

        [Required(ErrorMessageResourceName = "RolesNameError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Role", ResourceType = typeof(GeneralResource))]
        public string RoleName { get; set; }

        public int[] schools { get; set; }

        [Required]
        public int[] schoolId { get; set; }

        public int[] selectedSchool { get; set; }
    }
}
