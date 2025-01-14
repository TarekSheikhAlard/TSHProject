
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
   public class PhysicalYearViewModel
    {
        [Display(Name = "PhysicalYearID", ResourceType = typeof(PhysicalYear))]
        public int PhysicalYearID { get; set; }

        [Required(ErrorMessageResourceName = "PhysicalYearNameerror", ErrorMessageResourceType = typeof(PhysicalYear))]
        [Display(Name = "PhysicalYearName", ResourceType = typeof(PhysicalYear))]
        public string PhysicalYearName { get; set; }

        [Display(Name = "IsCurrent", ResourceType = typeof(PhysicalYear))]
        public bool IsCurrent { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [Required(ErrorMessageResourceName = "FromDateerror", ErrorMessageResourceType = typeof(PhysicalYear))]
        [Display(Name = "FromDate", ResourceType = typeof(PhysicalYear))]
        public Nullable<System.DateTime> FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [Required(ErrorMessageResourceName = "EndDateerror", ErrorMessageResourceType = typeof(PhysicalYear))]
        [Display(Name = "EndDate", ResourceType = typeof(PhysicalYear))]
        public Nullable<System.DateTime> EndDate { get; set; }

        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }


        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SchoolID { get; set; }


    }
}
