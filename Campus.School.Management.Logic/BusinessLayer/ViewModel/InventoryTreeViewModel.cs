using Campus.School.Management.Logic.DataBaseLayer;
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
   public  class InventoryTreeViewModel
    {


        [Display(Name = "InventoryID", ResourceType = typeof(Resources.InventoryTree))]
        public int InventoryTreeID { get; set; }

        public bool? check { get; set; }

        [System.Web.Mvc.Remote("CheckInventoryExist", "InventoryTree", AdditionalFields = "check", ErrorMessageResourceName = "InventoryNameArExist", ErrorMessageResourceType = typeof(Resources.InventoryTree), HttpMethod = "Post")]
        [Required(ErrorMessageResourceName = "InventoryNameArError", ErrorMessageResourceType = typeof(Resources.InventoryTree))]
        [Display(Name = "InventoryNameAr", ResourceType = typeof(Resources.InventoryTree))]
        public string InventoryNameAR { get; set; }

        [Required(ErrorMessageResourceName = "InventoryNameENError", ErrorMessageResourceType = typeof(Resources.InventoryTree))]
        [Display(Name = "InventoryNameEN", ResourceType = typeof(Resources.InventoryTree))]
        public string InventoryNameEN { get; set; }


        [Display(Name = "InventoryCode", ResourceType = typeof(Resources.InventoryTree))]
        public string InventoryTreeCode { get; set; }

        //[Required(ErrorMessageResourceName = "InventoryParentError", ErrorMessageResourceType = typeof(InventoryTrees))]
        [Display(Name = "InventoryParent", ResourceType = typeof(Resources.InventoryTree))]
        public int? InventoryTreeParentID { get; set; }

              
            
        [Required(ErrorMessageResourceName = "InventoryLevelError", ErrorMessageResourceType = typeof(Resources.InventoryTree))]
        [DefaultValue(value: 0)]
        [Display(Name = "InventoryLevel", ResourceType = typeof(Resources.InventoryTree))]
        public int InventoryTreeLevel { get; set; }


        [Display(Name = "InventoryParentNameAr", ResourceType = typeof(Resources.InventoryTree))]
        public string InventoryTreeParentNameAr { get; set; }

        [Display(Name = "InventoryParentNameEN", ResourceType = typeof(Resources.InventoryTree))]
        public string InventoryTreeParentNameEN { get; set; }

        public DataBaseLayer.InventoryTree InventoryTreeParent { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }
       




    }
}
