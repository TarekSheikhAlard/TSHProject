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
    public class AccStudentFeesHandler : IRepository<AccStudentFeesViewModel>

    {
        AccStudentConfigHandler _studConfig = new AccStudentConfigHandler();
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();



        public AccStudentFeesViewModel Create(int? ConfigID)
        {
            AccStudentFeesViewModel model = new AccStudentFeesViewModel();

            model.AccStudentConfig = GetConfigration(ConfigID);// _studConfig.GetById(StudentAcdID);
            
            model.AccStudentFee = GetAllFeeByConfig(ConfigID);
            
          
          //  decimal? _totalBeforeTax = (decimal)model.AccStudentConfig.Amount;

            //model.totalBeforeTax = _totalBeforeTax;

            //decimal? _taxRate =(decimal) model.AccStudentConfig.Tax;

            //decimal? _totalaftertax = 0;

            //if (_taxRate != 0)
            //{
            //    _totalaftertax = _totalBeforeTax + (_totalBeforeTax * _taxRate) / 100;

            //}
            //else
            //{
            //    _totalaftertax = _totalBeforeTax;
            //}

            //model.TotalAmount = _totalaftertax;


            ////model.StudAcdID = model.AccStudentConfig.StudentReference.StudAcdID;
            //model.PaidAmount = 0;

            //if (model.AccStudentFee.Count > 0)
            //{
            //    var _Last = model.AccStudentFee.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

            //    model.PaidAmount = _Last.PaidAmount;


            //}

            //model.remainderAmount = model.TotalAmount - model.PaidAmount;
            return model;
        }

     


        public AccStudentConfig GetConfigration(int? ConfigID)
        {
            return context.AccStudentConfigs.Where(x => x.ID == ConfigID).FirstOrDefault();
        }

        public List<AccStudentFee> GetAllFeeByConfig(int? ConfigID)
        {
            return context.AccStudentFees.Where(x => x.StudentConfigrationID == ConfigID).ToList();
        }

        public List<AccStudentFee> GetAllFeeByStudent(long ID)
        {
            return context.AccStudentFees.ToList();
        }

        public void Create(AccStudentFeesViewModel model)
        {  
            BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();

              model.originalAmount = model.Amount;

            //var currency= _BankCurrencyHandler.GetById(model.BankCurrencyID.Value);

            //if (currency.Isdefault==false)
            //{
            //    model.Amount = model.Amount *(decimal) currency.Factor;
            //}

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            if (model.AccStudentFee.Count() == 0)
            {
                model.PaidAmount = model.Amount;

            }
            else
            {
                var _Last = model.AccStudentFee.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                model.PaidAmount = model.Amount + (_Last.PaidAmount);
                //model.TotalAmount = _Last.TotalAmount;
            }

            model.IsPaid = false;

            if (model.PaymentType == 2 && model.PaidAmount == model.TotalAmount)// cash
            {
                model.IsPaid = true;
            }

            int? _dailyjornalid = 0; 
            int? _AccountReciveCash_Id= 0;
            int? _Accountchequesreceivables_Id= 0;


            if (model.PaymentWay == 4)
            {
                //-----------------------------------------Enrollment registration ------------------------------------------
                AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();
                AccountchequesreceivableHandler _AccountchequesreceivableHandler = new AccountchequesreceivableHandler();
                var _AccountchequesreceivableviewModel = _AccountchequesreceivableHandler.Create();
                List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {

                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.CostCenterID,
                    AccountTreeID = model.fromAccount,
                    Credit =(decimal)model.Amount
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.CostCenterID,
                     AccountTreeID =(int)model.AccountTreeID,
                     Debit =(decimal)model.Amount
                 }
            };

                var _DailyJournal = _AccountDailyJournalHandler.Create();


                //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
                _DailyJournal.DailyJournalDate = model.OperationDate;
                _DailyJournal.DocumentNumber = model.docementNumber;
                _DailyJournal.Description = "استلام شيك" + _AccountchequesreceivableviewModel.InvoiceCode +"  خاص  " + model.Description;

                _DailyJournal.Note = "received cheque" + _AccountchequesreceivableviewModel.InvoiceCode;
                _DailyJournal.Note1 = "استلام شيك " + _AccountchequesreceivableviewModel.InvoiceCode;

                if (model.isCollected == true)
                {
                    _DailyJournal.IsPost = true;
                }
                else
                {
                    _DailyJournal.IsPost = false;
                }
                _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                _AccountDailyJournalHandler.Create(_DailyJournal);

                _dailyjornalid = context.AccountDailyJournals.ToList().Last().DailyJournalID;


                //-----------------------------------انهاء تسجيل القيد -------------------------------------------

                //___________________________________Check receipt receipt _____________________________________________



                Accountchequesreceivable _Accountchequesreceivable = new Accountchequesreceivable()
                {
                    BankBranchID = model.BankBranchID,
                    docementNumber = model.docementNumber,
                    isCollected = model.isCollected,
                    operationDate = model.OperationDate,
                    collectionDate = model.collectionDate,
                    Notes = model.Description,
                    costcenter = model.CostCenterID,
                    CreatedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    fromAccount = model.fromAccount,
                    Amount = model.Amount,
                    AccountTreeID = model.AccountTreeID,
                    InvoiceCode = _AccountchequesreceivableviewModel.InvoiceCode,
                    CreatedbyID = _dbUser.ID,
                    SchoolID = _dbUser.SchoolID,
                    //BankCurrencyID = model.BankCurrencyID,
                    Employee_ID = model.Employee_ID,
                    ChequeDate = model.ChequeDate,
                    DailyJournalID = _dailyjornalid

                };
                context.Accountchequesreceivables.Add(_Accountchequesreceivable);
                context.SaveChanges();
                _Accountchequesreceivables_Id = context.Accountchequesreceivables.ToList().Last().ID;
                //___________________________________End receipt of check ____________________________________________
            }
            else
            {
                //------------------------------------------------Enrollment registration ---------------------------------------
                AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

                var treasuryaccount = context.AccountTreasuries.Where(x => x.ID == model.TreasuryID).Select(x => x.AccountTreeID).FirstOrDefault();

                List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.CostCenterID,
                    AccountTreeID = model.AccountTreeID,
                    Credit =(decimal)model.Amount
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.CostCenterID,
                     AccountTreeID =(int)treasuryaccount,
                     Debit =(decimal)model.Amount
                 }
            };

                var _DailyJournal = _AccountDailyJournalHandler.Create();


                //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
                _DailyJournal.DailyJournalDate = model.OperationDate;
                _DailyJournal.DocumentNumber = model.docementNumber;
                _DailyJournal.Description = "سند قبض رقم" + model.docementNumber + "  خاص  " + model.Description;
                _DailyJournal.Note = "received receit" + model.docementNumber;
                _DailyJournal.Note1 = "سند قبض رقم" + model.docementNumber;
                _DailyJournal.IsPost = true;

                _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                _AccountDailyJournalHandler.Create(_DailyJournal);

                _dailyjornalid = context.AccountDailyJournals.ToList().Last().DailyJournalID;
                //------------------------------------------------End of Registration ------------------------------------------------


                //___________________________________________ Cash receipt _________________________________________
                AccountReciveCashHandler _AccountReciveCashHandler = new AccountReciveCashHandler();
                var _AccountReciveCashModel = _AccountReciveCashHandler.Create();
                AccountReciveCash _AccountReciveCash = new AccountReciveCash()
                {

                    Amount = model.Amount,
                    InvoiceCode = _AccountReciveCashModel.InvoiceCode,
                    AccountTreeID = model.AccountTreeID,
                    CostCenterID = model.CostCenterID,
                    docementNumber = model.docementNumber,
                    TreasuryID = model.TreasuryID,
                    Description = model.Description,
                    OperationDate = model.OperationDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    CreatedbyID = _dbUser.ID,
                    SchoolID = _dbUser.SchoolID,
                    //BankCurrencyID = model.BankCurrencyID,
                    Employee_ID = model.Employee_ID,
                    DailyJournalID = _dailyjornalid,
                   
                    

                };
                context.AccountReciveCashes.Add(_AccountReciveCash);
                context.SaveChanges();
                _AccountReciveCash_Id = context.AccountReciveCashes.ToList().Last().ID;
                //______________________________________________End of cash receipt ________________________________________


            }



            //---------------------------------------------Registration of expenses --------------------------------------------

            AccStudentFee _AccStudentfee = new AccStudentFee()
            {
                //StudAcdID = model.StudAcdID,
                StudentConfigrationID = model.StudentConfigrationID,

                Amount = model.Amount,
                PaidAmount = model.PaidAmount,
                TotalAmount = model.TotalAmount,

                PaymentWay = model.PaymentWay,
                PaymentType = model.PaymentType,
                bankStatement = model.bankStatement,

                IsPaid = model.IsPaid,
                OperationDate = model.OperationDate,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                //YearID = model.YearID,
                //GradeID = model.GradeID,
                DailyJournalID = _dailyjornalid,
                CashRecivedID = (_AccountReciveCash_Id>0) ?_AccountReciveCash_Id :null,
                ChequeRecivedID = (_Accountchequesreceivables_Id>0) ?_Accountchequesreceivables_Id:null,
                originalAmount=model.originalAmount
            };
            context.AccStudentFees.Add(_AccStudentfee);
            context.SaveChanges();
        } 
       

        public List<AccStudentFeesViewModel> GetAll(long StudentAcdID)
       {
            return context.AccStudentFees.Where(x => x.StudRefID == StudentAcdID && x.IsDeleted==false)
           .Select(x => new AccStudentFeesViewModel
           {
               ID = x.ID,
               StudAcdID = x.StudRefID,
               StudentConfigrationID = x.StudentConfigrationID,
               PaymentType = x.PaymentType,
               PaymentWay = x.PaymentWay,
               Amount = x.Amount,
               PaidAmount = x.PaidAmount,
               TotalAmount = x.TotalAmount,
               OperationDate=x.OperationDate,
               bankStatement = x.bankStatement,
               IsPaid = x.IsPaid,
               AccountDailyJournal = x.AccountDailyJournal,
               Accountchequesreceivable=x.Accountchequesreceivable,
               AccountReciveCash=x.AccountReciveCash,
               DailyJournalID = x.DailyJournalID,
               ChequeRecivedID =x.ChequeRecivedID,
               CashRecivedID=x.CashRecivedID,
               originalAmount=x.originalAmount

           }).ToList();

        }
        


        public AccStudentFeesViewModel GetById(int ID)
        {
            return context.AccStudentFees.Where(x => x.ID == ID && x.IsDeleted == false)
          .Select(x => new AccStudentFeesViewModel
          {
              ID = x.ID,
              //StudAcdID = x.StudAcdID,
              StudentConfigrationID = x.StudentConfigrationID,
              PaymentType = x.PaymentType,
              PaymentWay = x.PaymentWay,
              Amount = x.Amount,
              PaidAmount = x.PaidAmount,
              TotalAmount = x.TotalAmount,
              OperationDate = x.OperationDate,
              bankStatement = x.bankStatement,
              IsPaid = x.IsPaid,
              AccountDailyJournal = x.AccountDailyJournal,
              Accountchequesreceivable = x.Accountchequesreceivable,
              AccountReciveCash = x.AccountReciveCash,
              DailyJournalID = x.DailyJournalID,
              ChequeRecivedID = x.ChequeRecivedID,
              CashRecivedID = x.CashRecivedID,
              originalAmount=x.originalAmount


          }).FirstOrDefault();

        }



        public AccStudentFeesViewModel Update(AccStudentFeesViewModel model)
        {

            return model;


        }

        public List<AccStudentFeesViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccStudentFeesViewModel Create()
        {
            AccStudentFeesViewModel model = new AccStudentFeesViewModel();

            return model;
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccStudentFees = context.AccStudentFees.Where(x => x.ID == ID).FirstOrDefault();
            if (_AccStudentFees != null)
            {
                _AccStudentFees.IsDeleted = true;
                _AccStudentFees.DeletedDate = DateTime.Now;
                _AccStudentFees.DeletedbyID = _dbUser.ID;
                context.SaveChanges();
                return true;
            }
            else return false;
        }


    }
}
