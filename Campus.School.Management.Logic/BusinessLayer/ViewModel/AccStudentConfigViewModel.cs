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
    public class AccStudentConfigViewModel
    {
        public int ID { get; set; }

        public long Ref_Id { get; set; }

        [Display(Name = "FeeItemId", ResourceType = typeof(AccStudentConfig))]
        public int FeeItemId { get; set; }

        [Display(Name = "FeeItemId", ResourceType = typeof(AccStudentConfig))]
        public string FeeItemName { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Amount", ResourceType = typeof(AccStudentConfig))]
        public double Amount { get; set; }


        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Discount", ResourceType = typeof(AccStudentConfig))]
        public double Discount { get; set; }


        [Required(ErrorMessageResourceName = "TaxRateerror", ErrorMessageResourceType = typeof(AccStudentConfigrations))]
        [Display(Name = "Tax", ResourceType = typeof(AccStudentConfig))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public double Tax { get; set; }

        [Required(ErrorMessageResourceName = "Descerror", ErrorMessageResourceType = typeof(AccStudentConfig))]
        [Display(Name = "Description", ResourceType = typeof(AccStudentConfig))]
        public string Description { get; set; }

        [Display(Name = "StudentAcdID", ResourceType = typeof(AccStudentConfig))]
        public Nullable<long>  StudentAcdID { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int BusID { get; set; }
        public bool MorningTrip { get; set; }
        public bool EveningTrip { get; set; }



    }

}
