using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class EmployeeSalaryReportViewModel
    {

      
        public int ID { get; set; }
        public string EmpNumber { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Hirdate { get; set; }
        public string Job { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal OverTimeRate { get; set; }
        public decimal Taxes { get; set; }
        public decimal Deductions { get; set; }
        public int? Absent { get; set; }
        public decimal NetTotal { get; set; }

    }
    public class EmployeeSalaryReportInputs
    {
        public int year { get; set; }
        public int month { get; set; }
       // public int companyId { get; set; }
    }

}
