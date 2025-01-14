using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public  class GradesSchoolViewModel
    {

        public  Nullable<int> YearID { get; set; }
        public int GradeID { get; set; }
        public int GradeOrig { get; set; }
        public string GradeSchoolNameE { get; set; }
        public string GradeSchoolNameA { get; set; }
        public bool Is_Upload { get; set; }
        public bool Is_Update { get; set; }
        


    }
}
