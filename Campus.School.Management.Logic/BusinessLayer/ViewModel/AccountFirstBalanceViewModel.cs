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
   public  class AccountFirstBalanceViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(FirstBalance))]
        [Display(Name = "costcenter", ResourceType = typeof(FirstBalance))]
        public Nullable<int> CostCenterID { get; set; }

        [Display(Name = "costcenter", ResourceType = typeof(FirstBalance))]
        public string costcenterName { get; set; }


        [Required(ErrorMessageResourceName = "AccountNameError", ErrorMessageResourceType = typeof(FirstBalance))]
        [Display(Name = "AccountName", ResourceType = typeof(FirstBalance))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountName", ResourceType = typeof(FirstBalance))]
        public string AccountNameEN { get; set; }

        [Required(ErrorMessageResourceName = "ActionTypeError", ErrorMessageResourceType = typeof(FirstBalance))]
        [Display(Name = "ActionType", ResourceType = typeof(FirstBalance))]
        public string ActionType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "DateError", ErrorMessageResourceType = typeof(FirstBalance))]
        [Display(Name = "Date", ResourceType = typeof(FirstBalance))]
        public Nullable<System.DateTime> OperationDate { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "AmountError", ErrorMessageResourceType = typeof(FirstBalance))]
        [Display(Name = "Amount", ResourceType = typeof(FirstBalance))]
         public Nullable<decimal> Amount { get; set; }


        public Nullable<int> PhysicalYearID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SchoolID { get; set; }


    }
}
