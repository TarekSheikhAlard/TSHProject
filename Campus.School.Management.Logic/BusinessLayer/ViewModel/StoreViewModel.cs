using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using System.Web.Mvc;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class StoreViewModel
    {
        [Required]
        [Display(Name = "StoreNumber", ResourceType = typeof(Stores))]
        public string StoreNumber { get; set; }
        [Required]
        [Display(Name = "NameEn", ResourceType = typeof(Stores))]
        public string NameEn { get; set; }
        [Required]
        [Display(Name = "NameAr", ResourceType = typeof(Stores))]
        public string NameAr { get; set; }
        [Required]
        [Display(Name = "PricingType", ResourceType = typeof(Stores))]
        public string PricingType { get; set; }


        [Required]
        public Nullable<int> InventoryTreeId { get; set; }
        [Display(Name = "InventoryTree", ResourceType = typeof(Stores))]
        public string InventoryTreeName { get; set; }

        [Required]
        public Nullable<int> FundAccount { get; set; }
        [Display(Name = "FundAccount", ResourceType = typeof(Stores))]
        public string FundAccountName { get; set; }

        [Required] 
        public Nullable<int> StoreAccount { get; set; }
        [Display(Name = "StoreAccount", ResourceType = typeof(Stores))]
        public string StoreAccountName { get; set; }

        [Required]
        public Nullable<int> CostAccount { get; set; }
        [Display(Name = "CostAccount", ResourceType = typeof(Stores))]
        public string CostAccountName { get; set; }


        [Required]
        public Nullable<int> SalesAccount { get; set; }
        [Display(Name = "SalesAccount", ResourceType = typeof(Stores))]
        public string SalesAccountName { get; set; }

        [Required]
        [Display(Name = "CostCenter1", ResourceType = typeof(Stores))]
        public Nullable<int> CostCenter1 { get; set; }

        [Required]
        [Display(Name = "CostCenter2", ResourceType = typeof(Stores))]
        public Nullable<int> CostCenter2 { get; set; }

        [Required]
        [Display(Name = "BankName", ResourceType = typeof(Stores))]
        public Nullable<int> BankName { get; set; }

        [Required]
        [Display(Name = "BankAccountNumber", ResourceType = typeof(Stores))]
        public string BankAccountNumber { get; set; }

        [Required]
        [Display(Name = "Size", ResourceType = typeof(Stores))]
        public string Size { get; set; }

        [Required]
        [Display(Name = "Manager", ResourceType = typeof(Stores))]
        public Nullable<int> Manager { get; set; }

        [Required]
        [Display(Name = "Address", ResourceType = typeof(Stores))]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Stores))]
        public Nullable<int> City { get; set; }

        [Required]
        [Display(Name = "tel", ResourceType = typeof(Stores))]
        public Nullable<int> tel { get; set; }

        [Required]
        [Display(Name = "Mobile", ResourceType = typeof(Stores))]
        public Nullable<int> Mobile { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Stores))]
        public Nullable<int> Email { get; set; }

        [Required]
        [Display(Name = "Location", ResourceType = typeof(Stores))]
        public string Location { get; set; }


        [Required]
        public DateTime OperationDate { get; set; }

        public int[] inventoryStorekeeperIds { get; set; }

        public int storeId {get;set;}
    }
}
