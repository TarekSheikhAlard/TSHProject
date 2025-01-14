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
    public class DepartmentTreeViewModel
    {

        //[Required(ErrorMessageResourceName = " Error", ErrorMessageResourceType = typeof(DepartmentTree))]
        //[Display(Name = " ", ResourceType = typeof(DepartmentTree))]

        [Display(Name = "DepartmentID", ResourceType = typeof(DepartmentTrees))]
        public int DepartmentTreeID { get; set; }

        public bool? check { get; set; }

        //[System.Web.Mvc.Remote("CheckDepartmentExist", "DepartmentTree", AdditionalFields = "check", ErrorMessageResourceName = "DepartmentNameArExist", ErrorMessageResourceType = typeof(DepartmentTrees), HttpMethod = "Post")]
        //[Required(ErrorMessageResourceName = "DepartmentNameArError", ErrorMessageResourceType = typeof(DepartmentTrees))]
        [Display(Name = "DepartmentNameAr", ResourceType = typeof(DepartmentTrees))]
        public string DepartmentNameAR { get; set; }

        [Required(ErrorMessageResourceName = "DepartmentNameENError", ErrorMessageResourceType = typeof(DepartmentTrees))]
        [Display(Name = "DepartmentNameEN", ResourceType = typeof(DepartmentTrees))]
        public string DepartmentNameEN { get; set; }


        [Display(Name = "DepartmentCode", ResourceType = typeof(DepartmentTrees))]
        public string DepartmentTreeCode { get; set; }

        //[Required(ErrorMessageResourceName = "DepartmentParentError", ErrorMessageResourceType = typeof(DepartmentTrees))]
        [Display(Name = "DepartmentParent", ResourceType = typeof(DepartmentTrees))]
        public int? DepartmentTreeParentID { get; set; }

        [Required(ErrorMessageResourceName = "DepartmentLevelError", ErrorMessageResourceType = typeof(DepartmentTrees))]
        [DefaultValue(value: 0)]
        [Display(Name = "DepartmentLevel", ResourceType = typeof(DepartmentTrees))]
        public int DepartmentTreeLevel { get; set; }


        [Display(Name = "DepartmentParentNameAr", ResourceType = typeof(DepartmentTrees))]
        public string DepartmentTreeParentNameAr { get; set; }

        [Display(Name = "DepartmentParentNameEN", ResourceType = typeof(DepartmentTrees))]
        public string DepartmentTreeParentNameEN { get; set; }
        public DepartmentTree DepartmentTreeParent { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

    }
}
