using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class EmployeeAddedBenefitsOthersViewModel
    {

        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeBenefitsOtherId { get; set; }

        public string BenefitsOtherNameEn { get; set; }
        public string BenefitsOtherNameAr { get; set; }
        public int Allowancetype { get; set; }

        public decimal BenefitPercent { get; set; }
        public int AccountTreeID { get; set; }

       



    }
}
