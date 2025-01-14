using Campus.School.Management.Logic.DataBaseLayer;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class HRAllowanceEntryViewModel
    {
        public long ID { get; set; }
        public int Employee_ID { get; set; }
        public decimal Allowance { get; set; }
        public string AllowanceType { get; set; }
        public string CREATED_BY { get; set; }

        [Display(Name="Created Date")]
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string DELETED_BY { get; set; }
        public Nullable<System.DateTime> DELETED_DATE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public decimal accommodationallowanc { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
