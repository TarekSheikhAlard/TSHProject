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
   public class BusExpenseItemViewModel
    {

        [Display(Name = "ID", ResourceType = typeof(BusExpenseItem))]
        public int ID { get; set; }

        //[Required(ErrorMessageResourceName = "BusExpenses_IDerror", ErrorMessageResourceType = typeof(BusExpenseItem))]
        //[Display(Name = "BusExpenses_ID", ResourceType = typeof(BusExpenseItem))]
     
        public int BusExpenses_ID { get; set; }

        //[Required(ErrorMessageResourceName = "Totalerror", ErrorMessageResourceType = typeof(BusExpenseItem))]
        [Display(Name = "Total", ResourceType = typeof(BusExpenseItem))]
        
        public decimal Total { get; set; }

        [Required(ErrorMessageResourceName = "Amounterror", ErrorMessageResourceType = typeof(BusExpenseItem))]
        [Display(Name = "Amount", ResourceType = typeof(BusExpenseItem))]
        
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceName = "Quantityerror", ErrorMessageResourceType = typeof(BusExpenseItem))]
        [Display(Name = "Quantity", ResourceType = typeof(BusExpenseItem))]
       
        public int Quantity { get; set; }

        [Required(ErrorMessageResourceName = "ItemDescerror", ErrorMessageResourceType = typeof(BusExpenseItem))]
        [Display(Name = "ItemDesc", ResourceType = typeof(BusExpenseItem))]
        
        public string ItemDesc { get; set; }
          
             
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        
        public virtual BusExpens BusExpens { get; set; }
        
    }
}
