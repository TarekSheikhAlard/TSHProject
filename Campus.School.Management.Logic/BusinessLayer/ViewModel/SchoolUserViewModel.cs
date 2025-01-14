using Campus.School.Management.Logic.DataBaseLayer;
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
    public class SchoolUserViewModel
    {
       
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name", ResourceType = typeof(SchoolsUsers))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Role", ResourceType = typeof(SchoolsUsers))]
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "School", ResourceType = typeof(SchoolsUsers))]
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string AspUserID { get; set; }
        public bool IsActive { get; set; }

        public DateTime startYearDate { get; set; }
        public DateTime endYearDate { get; set; }


        [Required]
        [Display(Name = "Company", ResourceType = typeof(SchoolsUsers))]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(SchoolsUsers))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(SchoolsUsers))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        
        [Display(Name = "Confirmpassword", ResourceType = typeof(SchoolsUsers))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Url { get; set; }
        public string Location { get; set; }

        public string logo { get; set; }
        public int CreatedbyID { get; set; }
        public bool isArabic { get; set; }

        public Nullable<int> ModifiedbyID { get; set; }
       
        public Nullable<int> DeletedbyID { get; set; }

        public  ICollection<UserPermission> UserPermissionList { get; set; }
    }
}
