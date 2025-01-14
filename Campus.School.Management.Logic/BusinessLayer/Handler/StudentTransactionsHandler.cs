using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class StudentTransactionsHandler : IRepository<StudentTransactionsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
       
        public void Create(StudentTransactionsViewModel model)
        {
            throw new NotImplementedException();
        }

        public StudentTransactionsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }
        public List<StudentTransactionsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<StudentTransactionsViewModel> GetAll(int id)
        {
            string query = @"
              SELECT journal.SerialNo,
              CAST(journal.[CreatedDate] AS DATE) [date], 
              journal.[Description],
              ISNULL(CAST( (fees.originalAmount/installment.Period) AS decimal(18,2)),0.0 ) Amount,
              ISNULL(fees.Discount+fees.StudentDiscount,0)  Discount,
              ISNULL(fees.Tax,0) ,
              ISNULL(CAST( (fees.TotalAmount/installment.Period) AS decimal(18,2)),0.0 ) TotalAmount,
              fees.PaidAmount,
              ISNULL(fees.ReceiptID,0) ReceiptCode,
              ISNULL(CashRecivedID,0) CashRecivedID,
              ISNULL(fees.DailyJournalID,0) DailyJournalID,
              ISNULL(ChequeRecivedID,0) ChequeRecivedID,
              fees.ID,
              fees.StudRefID,
              SchoolUser.[Name] createdBy 
              FROM AccStudentFees fees INNER JOIN  
               AccountDailyJournal journal  ON
               fees.DailyJournalID = journal.DailyJournalID
               INNER JOIN StudentReference ref  ON 
               ref.StudAcdID=@p0 and fees.StudRefID=ref.Ref_Id
               INNER JOIN SchoolUser ON 
               SchoolUser.ID=FEES.CreatedbyID
               INNER JOIN FeeInstallmentTypes Installment ON 
               installment.Id=fees.installment
  ";


            return Context.Database.SqlQuery<StudentTransactionsViewModel>(query, id).ToList();
            //return Context.AccStudentFees.Where(x => x.SchoolID == 5016 && x.StudentReference.StudAcdID == id && x.IsDeleted ==false)
            //    .Select(x => new StudentTransactionsViewModel
            //    {
            //        SerialNo = x.AccountDailyJournal.SerialNo,
            //       // Date = x.AccountDailyJournal.CreatedDate,
            //        Description = x.AccountDailyJournal.Description,
            //        Amount = (decimal)x.originalAmount / x.FeeInstallmentType.Period ?? 0,
            //        Discount = x.Discount + x.StudentDiscount ?? 0,
            //        Tax = x.Tax ?? 0,
            //        TotalAmount = x.TotalAmount / x.FeeInstallmentType.Period ?? 0,
            //        PaidAmount = x.PaidAmount ?? 0,
            //        ReceiptCode = x.ReceiptID,
            //        CashRecivedID = x.CashRecivedID,
            //        DailyJournalID = x.DailyJournalID,
            //        ChequeRecivedID = x.ChequeRecivedID,
            //        ID = x.ID,
            //        StudRefID = x.StudRefID,
            //        createdBy = x.SchoolUser.AspNetUser.UserName
            //    }).ToList();
        }

        public StudentTransactionsViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public StudentTransactionsViewModel Update(StudentTransactionsViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
