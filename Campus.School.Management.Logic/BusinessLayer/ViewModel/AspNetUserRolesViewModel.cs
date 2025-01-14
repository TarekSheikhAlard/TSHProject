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
    public class AspNetUserRolesViewModel
    {

        

        [Required(ErrorMessageResourceName = "GroupNameError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Group", ResourceType = typeof(GeneralResource))]
        public string groupName { get; set; }

        [Required]
        public string groupID { get; set; }

        public string Username { get; set; }

    }
}
