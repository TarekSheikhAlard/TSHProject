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
   public class BusExpenseViewModel
    {
        

        [Display(Name = "ID", ResourceType = typeof(BusExpenses))]
        public int ID { get; set; }


        [Required(ErrorMessageResourceName = "EmployeeIDError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "EmployeeID", ResourceType = typeof(BusExpenses))]
        public int Employee_ID { get; set; }

        [Required(ErrorMessageResourceName = "bus_IDError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "bus_ID", ResourceType = typeof(BusExpenses))]
        public Nullable<int> BusID { get; set; }

      
       [Display(Name = "Serial_No", ResourceType = typeof(BusExpenses))]   
        public string SerialNo { get; set; }

        [Required(ErrorMessageResourceName = "Document_NumberError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "Document_Number", ResourceType = typeof(BusExpenses))]
        public string DocumentNumber { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "TotalError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "Total", ResourceType = typeof(BusExpenses))]
        public decimal Total { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "DateError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "Date", ResourceType = typeof(BusExpenses))]
        public Nullable<System.DateTime> OperationDate { get; set; }

        [Required(ErrorMessageResourceName = "LocationError", ErrorMessageResourceType = typeof(BusExpenses))]
        [Display(Name = "Location", ResourceType = typeof(BusExpenses))]

        public string Place { get; set; }
               
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        
       
        public virtual Bus Bus { get; set; }
        //[Display(Name = "Bus_Name", ResourceType = typeof(BusExpenses))]
 
        public virtual string BusName { get; set; }

        public virtual List<BusExpenseItemViewModel> _BusExpensesItems { get; set; }

       
        public virtual AdmEmployee AdmEmployee { get; set; }
        [Display(Name = "Employee_Name", ResourceType = typeof(BusExpenses))]
 
        public virtual string EmployeeName { get; set; }

    }
}
