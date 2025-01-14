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
   public class AssetTreeViewModel
    {

        //[Required(ErrorMessageResourceName = " Error", ErrorMessageResourceType = typeof(AssetTree))]
        //[Display(Name = " ", ResourceType = typeof(AssetTree))]



        [Display(Name = "AssetID", ResourceType = typeof(AssetTrees))]
        public int AssetTreeID { get; set; }

        public bool? check { get; set; }

        //[System.Web.Mvc.Remote("CheckAssetExist", "AssetTree", AdditionalFields = "check", ErrorMessageResourceName = "AssetNameArExist", ErrorMessageResourceType = typeof(AssetTrees), HttpMethod = "Post")]
        //[Required(ErrorMessageResourceName = "AssetNameArError", ErrorMessageResourceType = typeof(AssetTrees))]
        [Display(Name = "AssetNameAr", ResourceType = typeof(AssetTrees))]
        public string AssetNameAR { get; set; }

        [Required(ErrorMessageResourceName = "AssetNameENError", ErrorMessageResourceType = typeof(AssetTrees))]
        [Display(Name = "AssetNameEN", ResourceType = typeof(AssetTrees))]
        public string AssetNameEN { get; set; }

 
        [Display(Name = "AssetCode", ResourceType = typeof(AssetTrees))]
        public string AssetTreeCode { get; set; }

        //[Required(ErrorMessageResourceName = "AssetParentError", ErrorMessageResourceType = typeof(AssetTrees))]
        [Display(Name = "AssetParent", ResourceType = typeof(AssetTrees))]
        public int? AssetTreeParentID { get; set; }

        [Required(ErrorMessageResourceName = "AssetLevelError", ErrorMessageResourceType = typeof(AssetTrees))]
        [DefaultValue(value:0)]
        [Display(Name = "AssetLevel", ResourceType = typeof(AssetTrees))]
        public int AssetTreeLevel { get; set; }


        [Display(Name = "AssetParentNameAr", ResourceType = typeof(AssetTrees))]
        public string AssetTreeParentNameAr { get; set; }

        [Display(Name = "AssetParentNameEN", ResourceType = typeof(AssetTrees))]
        public string AssetTreeParentNameEN { get; set; }
        public AssetTree AssetTreeParent { get; set; }

        [Display(Name ="Notes")]
        public string Notes { get; set; }
      
        [Display(Name = "BankCurrencyID", ResourceType = typeof(AssetTrees))]
        public Nullable<int> BankCurrencyID { get; set; }

        [Display(Name = "BankCurrencyID", ResourceType = typeof(AssetTrees))]
        public string BankCurrency { get; set; }
    }
}
