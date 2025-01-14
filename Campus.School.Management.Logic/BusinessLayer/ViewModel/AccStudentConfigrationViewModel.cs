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
  public class AccStudentConfigrationViewModel
    {

        [Display(Name = "ID", ResourceType = typeof(AccStudentConfigrations))]
        public int ID { get; set; }


        
        [Display(Name = "StudentAcdID", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<long> StudentAcdID { get; set; }

      
        [Display(Name = "AcademicYear", ResourceType = typeof(AccStudentConfigrations))]
        public string AcademicYear { get; set; }

      
        [Display(Name = "BusID", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<int> BusID { get; set; }

        [Display(Name = "TripType", ResourceType = typeof(AccStudentConfigrations))]
        public string TripType { get; set; }
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "BusFees", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BusFees { get; set; }

        [Display(Name = "ISbusDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public bool ISbusDiscount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "BusDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BusDiscount { get; set; }

        [Display(Name = "BusNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BusNetPrice { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "StudyFees", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> StudyFees { get; set; }

        [Display(Name = "isStudyDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public bool isStudyDiscount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "StudyFeesDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> StudyFeesDiscount { get; set; }

        [Display(Name = "StudyNetFees", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> StudyNetFees { get; set; }

        [Display(Name = "Size", ResourceType = typeof(AccStudentConfigrations))]
        public string Size { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "UniformPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> UniformPrice { get; set; }

        [Display(Name = "isUniformDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public bool isUniformDiscount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "UniformDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> UniformDiscount { get; set; }

        [Display(Name = "UniformNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> UniformNetPrice { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "BooksPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BooksPrice { get; set; }

        [Display(Name = "IsBooksDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public bool IsBooksDiscount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "BooksDiscount", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BooksDiscount { get; set; }

        [Display(Name = "BooksNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BooksNetPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "OperationDateerror", ErrorMessageResourceType = typeof(AccStudentConfigrations))]
        [Display(Name = "OperationDate", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<System.DateTime> OperationDate { get; set; }


        [Required(ErrorMessageResourceName = "TaxRateerror", ErrorMessageResourceType = typeof(AccStudentConfigrations))]
        [Display(Name = "TaxRate", ResourceType = typeof(AccStudentConfigrations))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public Nullable<decimal> TaxRate { get; set; }
    }
}
