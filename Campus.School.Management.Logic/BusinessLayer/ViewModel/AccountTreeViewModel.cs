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
   public class AccountTreeViewModel
    {

        //[Required(ErrorMessageResourceName = " Error", ErrorMessageResourceType = typeof(AccountTree))]
        //[Display(Name = " ", ResourceType = typeof(AccountTree))]



        [Display(Name = "AccountID", ResourceType = typeof(AccountTrees))]
        public int AccountTreeID { get; set; }

        public bool? check { get; set; }

        public bool? check1 { get; set; }

        //[System.Web.Mvc.Remote("CheckAccountExist", "AccountTree", AdditionalFields = "check", ErrorMessageResourceName = "AccountNameArExist", ErrorMessageResourceType = typeof(AccountTrees), HttpMethod = "Post")]
        //[Required(ErrorMessageResourceName = "AccountNameArError", ErrorMessageResourceType = typeof(AccountTrees))]
        [Display(Name = "AccountNameAr", ResourceType = typeof(AccountTrees))]
        public string AccountNameAR { get; set; }

        [System.Web.Mvc.Remote("CheckAccountExist", "AccountTree", AdditionalFields = "check", ErrorMessageResourceName = "AccountNameEnExist", ErrorMessageResourceType = typeof(AccountTrees), HttpMethod = "Post")]
        [Required(ErrorMessageResourceName = "AccountNameENError", ErrorMessageResourceType = typeof(AccountTrees))]
        [Display(Name = "AccountNameEN", ResourceType = typeof(AccountTrees))]
        public string AccountNameEN { get; set; }

 
        [Display(Name = "AccountCode", ResourceType = typeof(AccountTrees))]
        public string AccountTreeCode { get; set; }

        [Required(ErrorMessageResourceName = "AccountParentError", ErrorMessageResourceType = typeof(AccountTrees))]
        [Display(Name = "AccountParent", ResourceType = typeof(AccountTrees))]
        public int? AccountTreeParentID { get; set; }

        [Required(ErrorMessageResourceName = "AccountLevelError", ErrorMessageResourceType = typeof(AccountTrees))]
        [Display(Name = "AccountLevel", ResourceType = typeof(AccountTrees))]
        public int AccountTreeLevel { get; set; }


        [Display(Name = "AccountParentNameAr", ResourceType = typeof(AccountTrees))]
        public string AccountTreeParentNameAr { get; set; }

        [Display(Name = "AccountParentNameEN", ResourceType = typeof(AccountTrees))]
        public string AccountTreeParentNameEN { get; set; }
        public AccountTree AccountTreeParent { get; set; }


        public string Description { get; set; }

        [Required]
        public Decimal OpenBalance { get; set; }

        [Required]
        public string CreditDebitFlag { get; set; }



    }
}
