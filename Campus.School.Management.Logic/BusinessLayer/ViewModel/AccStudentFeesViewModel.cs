using Campus.School.Management.Logic.DataBaseLayer;
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
    //deprecated
  public class AccStudentFeesViewModel
    {

        [Display(Name = "ID", ResourceType = typeof(AccStudentFees))]
        public int ID { get; set; }

        public long? StudRefID { get; set; }

        [Display(Name = "StudAcdID", ResourceType = typeof(AccStudentFees))]
        public long? StudAcdID { get; set; }

        [Display(Name = "StudyNetFees", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> StudyNetFees { get; set; }

        [Display(Name = "BooksNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BooksNetPrice { get; set; }

        [Display(Name = "UniformNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> UniformNetPrice { get; set; }

        [Display(Name = "BusNetPrice", ResourceType = typeof(AccStudentConfigrations))]
        public Nullable<decimal> BusNetPrice { get; set; }

        [Display(Name = "StudentConfigrationID", ResourceType = typeof(AccStudentFees))]
        public int? StudentConfigrationID { get; set; }

        [Required(ErrorMessageResourceName = "PaymentTypeerror", ErrorMessageResourceType = typeof(AccStudentFees))]
        [Display(Name = "PaymentType", ResourceType = typeof(AccStudentFees))]
        public byte? PaymentType { get; set; }


        [Required(ErrorMessageResourceName = "PaymentWayerror", ErrorMessageResourceType = typeof(AccStudentFees))]
        [Display(Name = "PaymentWay", ResourceType = typeof(AccStudentFees))] 
        public byte? PaymentWay { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Amounterror", ErrorMessageResourceType = typeof(AccStudentFees))]
        [Display(Name = "Amount", ResourceType = typeof(AccStudentFees))]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "PaidAmount", ResourceType = typeof(AccStudentFees))]
         public Nullable<decimal> PaidAmount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "TotalAmount", ResourceType = typeof(AccStudentFees))]
        public Nullable<decimal> TotalAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "OperationDateerror", ErrorMessageResourceType = typeof(AccStudentFees))]
        [Display(Name = "OperationDate", ResourceType = typeof(AccStudentFees))]
        public Nullable<System.DateTime> OperationDate { get; set; }

        [Display(Name = "bankStatement", ResourceType = typeof(AccStudentFees))]
        public string bankStatement { get; set; }

        [Display(Name = "IsPaid", ResourceType = typeof(AccStudentFees))]
        public Nullable<bool> IsPaid { get; set; }


        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "totalBeforeTax", ResourceType = typeof(AccStudentFees))]
        public Nullable<decimal> totalBeforeTax { get; set; }


        [Display(Name = "remainderAmount", ResourceType = typeof(AccStudentFees))]
        public Nullable<decimal> remainderAmount { get; set; }


        [Required(ErrorMessageResourceName = "docement_NumberError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "docement_Number", ResourceType = typeof(chequesPayable))]
        public string docementNumber { get; set; }


       // [Required(ErrorMessageResourceName = "TreasuryError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "TreasuryIDCredit", ResourceType = typeof(chequesPayable))]
        public Nullable<int> TreasuryID { get; set; }

        [Required(ErrorMessageResourceName = "BankCurrencyIDError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "CurrencyType", ResourceType = typeof(chequesPayable))]
        public Nullable<int> BankCurrencyID { get; set; }


        [Display(Name = "Emp_Name", ResourceType = typeof(chequesPayable))]
        public Nullable<int> Employee_ID { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Notes", ResourceType = typeof(chequesPayable))]
        public string Description { get; set; }


        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public Nullable<int> CostCenterID { get; set; }


        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public int AccountTreeID { get; set; }


        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN { get; set; }

        //--------------------------------شيك --------------------------------
        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        //[Required(ErrorMessageResourceName = "Bank_BranchError", ErrorMessageResourceType = typeof(chequesPayable))]

        public Nullable<int> BankBranchID { get; set; }

        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        public string BankBranchName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        // [Required(ErrorMessageResourceName = "collection_DateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "collection_Date", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> collectionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cheque_Date", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> ChequeDate { get; set; }


        [Display(Name = "isCollected", ResourceType = typeof(chequesPayable))]

        public bool isCollected { get; set; }



        [Required(ErrorMessageResourceName = "CreditError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Credit", ResourceType = typeof(chequesPayable))]
        public int fromAccount { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN1 { get; set; }
        //--------------------------------------------------------------------------------------------------

        [Display(Name = "originalamount", ResourceType = typeof(DailyJornal))]
        public Nullable<decimal> originalAmount { get; set; }




        public int GradeID { get; set; }
        public int YearID { get; set; }
        public Nullable<int> DailyJournalID { get; set; }
        public Nullable<int> CashRecivedID { get; set; }
        public Nullable<int> ChequeRecivedID { get; set; }

        public virtual AccountDailyJournal AccountDailyJournal { get; set; }
        public virtual DataBaseLayer.AccStudentConfig AccStudentConfig { get; set; }
        public virtual List<AccStudentFee> AccStudentFee { get; set; }
        public virtual Accountchequesreceivable Accountchequesreceivable { get; set; }
        public virtual AccountReciveCash AccountReciveCash { get; set; }


        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }






    }


    public  class AccStudentFeeViewModel
    {
        [Display(Name = "ID", ResourceType = typeof(AccStudentFees))]
        public long ID { get; set; }

        public long? Ref_Id { get; set; }

        public string AccountNameEN { get; set; }

        public string AccountNameAR { get; set; }

        public int GradeID { get; set; }


        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Amounterror", ErrorMessageResourceType = typeof(AccStudentFees))]
        //[Display(Name = "Amount", ResourceType = typeof(AccStudentFees))]
        [Display(Name = "Fees")]
        public decimal Amount { get; set; }

        //[Display(Name = "PaidAmount", ResourceType = typeof(AccStudentFees))]
        public decimal PaidAmount { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
       // [Display(Name = "TotalAmount", ResourceType = typeof(AccStudentFees))]
        public decimal TotalAmount { get; set; }    

        public decimal? itemAmount { get; set; }


        public int Discount { get; set; }
        public int StudentDiscount { get; set; }

        public int tax { get; set; }
        public int installmentPeriod { get; set; }
        public string installmentTypeEn { get; set; }
        public string installmentTypeAr { get; set; }

        public int paidInstallment { get; set; }
        public string payInstallment { get; set; }
        public decimal partialAmount { get; set; }

        public Nullable<System.DateTime> OperationDate { get; set; }
        public string bankStatement { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DailyJournalID { get; set; }
        public Nullable<int> CashRecivedID { get; set; }
        public Nullable<int> ChequeRecivedID { get; set; }
        [Display(Name = "Fee Type")]
        public int AccountTreeID { get; set; }
        public Nullable<int> installment { get; set; }

        public virtual Accountchequesreceivable Accountchequesreceivable { get; set; }
        public virtual AccountDailyJournal AccountDailyJournal { get; set; }
        public virtual AccountReciveCash AccountReciveCash { get; set; }
        public virtual DataBaseLayer.AccStudentConfig AccStudentConfig { get; set; }
        public virtual StudentReference StudentReference { get; set; }
    }
}
