using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
  public class AdmStudent_ClassViewModel
    {
        public int ID { get; set; }
        public Nullable<long> StudentAcdID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> YearID { get; set; }
        public string YearName { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
    }
}
