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
   public class AccountStatementTypeViewModel
    {

        [Required(ErrorMessageResourceName = "StatementtypeError", ErrorMessageResourceType = typeof(StatementType))]
        [Display(Name = "Statementtype", ResourceType = typeof(StatementType))]

        public int StatementTypeID { get; set; }

        [Required(ErrorMessageResourceName = "Statementtype_NameError", ErrorMessageResourceType = typeof(StatementType))]
        [Display(Name = "Statementtype_Name", ResourceType = typeof(StatementType))]

        public string StatementypeName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
