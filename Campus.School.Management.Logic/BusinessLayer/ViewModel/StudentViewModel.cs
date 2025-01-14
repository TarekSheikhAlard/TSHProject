using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{


  public  class StudentViewModel
    {
        public int? _gradeId { get; set; }
        public string _yearId { get; set; }

        public StudentViewModel(int? gradeId, string yearId) {
            _gradeId = gradeId;
            _yearId = yearId;

        }
        public StudentViewModel()
        {
           

        }
        [Display(Name = "ID", ResourceType = typeof(Student))]
        public int ID  { get; set; }

       // [ReadOnly(true)]
        public long? StudRefID { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "StudentAcdIDerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "StudentAcdID", ResourceType = typeof(Student))]
        public long StudentAcdID { get; set; }

      
        //[Required(ErrorMessageResourceName = "YearIDerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "YearID", ResourceType = typeof(Student))]
        public string YearID { get; set; }

        [Display(Name = "YearName", ResourceType = typeof(Student))]
        public string YearName { get; set; }

      
        [Required(ErrorMessageResourceName = "GradeIDerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "GradeID", ResourceType = typeof(Student))]
        public Nullable<int> GradeID { get; set; }

  
        [Display(Name = "GradeName", ResourceType = typeof(Student))]
        public string GradeName { get; set; }



        //-----------------------------
        //public bool? check { get; set; }
        //[System.Web.Mvc.Remote("classSpace", "Student", AdditionalFields = "YearID,check", ErrorMessageResourceName = "Classspace", ErrorMessageResourceType = typeof(Student),HttpMethod = "Post")]
        //[Required(ErrorMessageResourceName = "ClassIDerror", ErrorMessageResourceType = typeof(Student))]
        //[Display(Name = "ClassID", ResourceType = typeof(Student))]
        //public Nullable<int> ClassID { get; set; }


        //[Display(Name = "className", ResourceType = typeof(Student))]
        //public string className { get; set;}

      
        [Required(ErrorMessageResourceName = "NameEnerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "NameEn", ResourceType = typeof(Student))]
        public string NameEn { get; set; }
       
        [Required(ErrorMessageResourceName = "ArabicNameerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "ArabicName", ResourceType = typeof(Student))]
         public string ArabicName { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "BirthDateerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "BirthDate", ResourceType = typeof(Student))]
        
        public string BirthDate { get; set; }

     
        [Required(ErrorMessageResourceName = "BirthPlaceerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "BirthPlace", ResourceType = typeof(Student))]
        public string BirthPlace { get; set; }

             
        [Required(ErrorMessageResourceName = "Addresserror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Address", ResourceType = typeof(Student))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "Sexerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Sex", ResourceType = typeof(Student))]
         public string Sex { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "IqamaNoerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "IqamaNo", ResourceType = typeof(Student))]
        public string IqamaNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "IqamaStartDateerror", ErrorMessageResourceType = typeof(Student))]
       
        [Display(Name = "IqamaStartDate", ResourceType = typeof(Student))]
         public string IqamaStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "IqamaEndDateerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "IqamaEndDate", ResourceType = typeof(Student))]
    
        public string IqamaEndDate { get; set; }


        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "PassportNoerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "PassportNo", ResourceType = typeof(Student))]
        public string PassportNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "PassportStartDateerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "PassportStartDate", ResourceType = typeof(Student))]
 
        public string PassportStartDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "PassEndDateerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "PassEndDate", ResourceType = typeof(Student))]
      
        public string PassEndDate { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "Mobileerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Mobile", ResourceType = typeof(Student))]
        public string Mobile { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "Telerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Tel", ResourceType = typeof(Student))]
        public string Tel { get; set; }
        

        [Display(Name = "ParentJob", ResourceType = typeof(Student))]
        public string ParentJob { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "ParentTellerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "ParentTell", ResourceType = typeof(Student))]
        public string ParentTell { get; set; }


        [Display(Name = "ParentAddress", ResourceType = typeof(Student))]
        public string ParentAddress { get; set; }
        [Display(Name = "Requirments", ResourceType = typeof(Student))]
        public string Requirments { get; set; }
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Orignalfees", ResourceType = typeof(Student))]
        public string Orignalfees { get; set; }

        [Display(Name = "LastEduShcool1", ResourceType = typeof(Student))]
        public string LastEduShcool1 { get; set; }

        [Display(Name = "LastEduShcool2", ResourceType = typeof(Student))]
        public string LastEduShcool2 { get; set; }

        [Display(Name = "LastEduCity1", ResourceType = typeof(Student))]
        public string LastEduCity1 { get; set; }

        [Display(Name = "LastEduCity2", ResourceType = typeof(Student))]
        public string LastEduCity2 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastEduDateFrom1", ResourceType = typeof(Student))]
        
        public string LastEduDateFrom1 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastEduDateFrom2", ResourceType = typeof(Student))]
 
        public string    LastEduDateFrom2 { get; set; }

        [Display(Name = "LastEduFull1", ResourceType = typeof(Student))]
        public string LastEduFull1 { get; set; }

        [Display(Name = "LastEduFull2", ResourceType = typeof(Student))]
         public string LastEduFull2 { get; set; }

        [Display(Name = "PersonStm", ResourceType = typeof(Student))]
        public string PersonStm { get; set; }

        [Display(Name = "Refrences", ResourceType = typeof(Student))]
        public string Refrences { get; set; }


        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "Parentmobileerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Parentmobile", ResourceType = typeof(Student))]
        public string Parentmobile { get; set; }

        [EmailAddress(ErrorMessageResourceName = "E_mailerror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "ParentMail", ResourceType = typeof(Student))]
        public string ParentMail { get; set; }


        [Display(Name = "IqamaPlace", ResourceType = typeof(Student))]
        public string IqamaPlace { get; set; }

        [Display(Name = "IsArchives", ResourceType = typeof(Student))]
        public bool IsArchives { get; set; }


        [Display(Name = "PrevSchool", ResourceType = typeof(Student))]
         public string PrevSchool { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ParentEqmEndDate", ResourceType = typeof(Student))]
 
        public string ParentEqmEndDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "GraduateDate", ResourceType = typeof(Student))]
 
        public string GraduateDate { get; set; }
        

        [Display(Name = "Is_Upload", ResourceType = typeof(Student))]
        public bool Is_Upload { get; set; }

        [Display(Name = "Is_Update", ResourceType = typeof(Student))]
        public bool Is_Update { get; set; }

        [Display(Name = "PrevSchAr", ResourceType = typeof(Student))]
        public string PrevSchAr { get; set; }

        [Required(ErrorMessageResourceName = "NationalityIDerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "NationalityID", ResourceType = typeof(Student))]
        public string NationalityID { get; set; }


        [Display(Name = "NationalityEn", ResourceType = typeof(Student))]
        public string NationalityEn { get; set; }

        [Display(Name = "Student_Img", ResourceType = typeof(Student))]
        public string Student_Img { get; set; }


        public int StudentConfigCount { get; set; }

        public string Location { get; set; }

        public long initId { get; set; }
        public string AdmDate { get; set; }

     //   [System.Web.Mvc.Remote("Checkmail", "SchoolUser", ErrorMessageResourceName = "toastrEmailExist", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "Mailerror", ErrorMessageResourceType = typeof(Student))]
        [Display(Name = "Mail", ResourceType = typeof(Student))]
        public string Mail { get; set; }

        //[StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(GeneralResource), MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessageResourceName = "Passworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Display(Name = "Password", ResourceType = typeof(GeneralResource))]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessageResourceName = "ComparePassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "ConfirmPassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Display(Name = "ConfirmPassword", ResourceType = typeof(GeneralResource))]
        //public string ConfirmPassword { get; set; }

    }
}
