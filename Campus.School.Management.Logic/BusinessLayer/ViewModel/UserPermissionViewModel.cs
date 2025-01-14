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
    public class UserPermissionViewModel
    {

        public int ID { get; set; }

       
        [Display(Name = "User", ResourceType = typeof(User_Permission))]
        public int UserID { get; set; }

        //public virtual List<UserPermissionViewModel> UserPermission { get; set; }

        [Required]
        public string RoleID { get; set; }

       // [Display(Name = "UserName", ResourceType = typeof(User_Permission))]
        public string RoleName { get; set; }


        [Display(Name = "UserName", ResourceType = typeof(User_Permission))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Menu", ResourceType = typeof(User_Permission))]
        public int PageID { get; set; }

        [Display(Name = "MenuName", ResourceType = typeof(User_Permission))]
        public string PageName { get; set; }


        [Required]
        [Display(Name = "View", ResourceType = typeof(User_Permission))]
        public bool ViewAct { get; set; }

        [Required]
        [Display(Name = "Create", ResourceType = typeof(User_Permission))]
        public bool CreateAct { get; set; }

        [Required]
        [Display(Name = "Edit", ResourceType = typeof(User_Permission))]
        public bool EditAct { get; set; }

        [Required]
        [Display(Name = "Delete", ResourceType = typeof(User_Permission))]
        public bool DeleteAct { get; set; }

        public virtual SchoolUser SchoolUser { get; set; }
        public virtual MenuPage MenuPage { get; set; }


        public int CreatedbyID { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public Nullable<int> DeletedbyID { get; set; }


    }
}
