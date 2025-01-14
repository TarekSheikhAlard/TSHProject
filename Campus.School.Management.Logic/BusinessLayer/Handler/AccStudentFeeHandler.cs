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
    public class AccStudentFeeHandler : IRepository<AccStudentFeeViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

        AccStudentConfigHandler _studConfig = new AccStudentConfigHandler();
        GradesFeeConfig gradesFeeConfig = new GradesFeeConfig();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(AccStudentFeeViewModel model)
        {
            throw new NotImplementedException();
        }
        public StudentReceiptViewModel Create(List<AccStudentFeeViewModel> viewModel, int StudAcdID, int amountPaid, int amountBalances, int CashAccount)
        {
            string query = "select * from dbo.GetFeePaymentTable(@p0)";

            var model = context.Database.SqlQuery<AccStudentFeeViewModel>(query, StudAcdID).ToList();
            int index = 0; decimal amount, perInstallmentAmount, partialAmount; string desc = string.Empty; int installment = 0;
            int _AccStudentfees_Id = 0;
            int  paidInstallment, totalInstallment;
            bool isPartial = false,exitFlag=false;
            List<StudentFeesReceiptViewModel> feesReceipts = new List<StudentFeesReceiptViewModel>();
            string[] months;
            string[] schoolMonths = new string[12];

            int startMonth = _dbUser.startYearDate.Month;
            int EndMonth = _dbUser.endYearDate.Month;

            months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            bool firstComplete = false;
            for (var i = 0; i < months.Length;)
            {
                if (!firstComplete)
                {
                    for (var j = startMonth; j < 12; j++)
                    {
                        schoolMonths[i] = months[j];
                        if (j == 11) { firstComplete = true; }
                        i++;
                    }
                }
                if (firstComplete)
                {

                    for (var j = 0; j < startMonth; j++)
                    {
                        schoolMonths[i] = months[j];
                        i++;
                    }

                }

            }


            foreach (var item in viewModel)
            {

                var feesObj = model[index];

                paidInstallment = feesObj.paidInstallment;
                totalInstallment = feesObj.installmentPeriod;

                perInstallmentAmount = Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod);
                feesObj.partialAmount = feesObj.partialAmount == perInstallmentAmount ? 0 : feesObj.partialAmount;
                paidInstallment = feesObj.partialAmount > 0 ? paidInstallment - 1 : paidInstallment;

                if (!string.IsNullOrEmpty(item.payInstallment))
                {
                    if (item.payInstallment.Contains(','))
                    {

                        foreach (var installemtPeriod in item.payInstallment.Split(','))
                        {

                            if (feesObj.partialAmount > 0 && !isPartial)
                            {
                                registerFeePaymentPartial(StudAcdID, (int)feesObj.partialAmount, amountBalances, CashAccount, out amount, out desc, feesObj, installemtPeriod, out _AccStudentfees_Id);
                                isPartial = true;
                            }
                            else
                            {
                                registerFeePayment(StudAcdID, amountPaid, amountBalances, CashAccount, out amount, out desc, feesObj, installemtPeriod, out _AccStudentfees_Id);
                            }

                            FillReceipt(_AccStudentfees_Id, feesReceipts, desc);
                        }
                    }
                    else
                    {
                        if (feesObj.partialAmount > 0 && !isPartial)
                        {
                            registerFeePaymentPartial(StudAcdID, (int)feesObj.partialAmount, amountBalances, CashAccount, out amount, out desc, feesObj, item.payInstallment, out _AccStudentfees_Id);
                            isPartial = true;
                        }
                        else
                        {
                            registerFeePayment(StudAcdID, amountPaid, amountBalances, CashAccount, out amount, out desc, feesObj, item.payInstallment, out _AccStudentfees_Id);
                        }


                        FillReceipt(_AccStudentfees_Id, feesReceipts, desc);
                    }



                }
                else
                {


                 
                    //indicates 12 months in a year
                    var m = (12 / totalInstallment) * paidInstallment;
                    //m = m + 1;
                    if (item.itemAmount>0) {
                        if (item.itemAmount >= perInstallmentAmount && item.PaidAmount<item.TotalAmount)
                        {
                            var amt = (decimal)amountPaid;
                            partialAmount = feesObj.partialAmount;

                            do
                            {

                                if (partialAmount > 0) {

                                    registerFeePaymentPartial(StudAcdID, partialAmount, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);

                                    amt = amt - partialAmount;
                                    partialAmount = 0;

                                }
                                else if (amt >= perInstallmentAmount && partialAmount == 0)
                                {
                                    registerFeePayment(StudAcdID, amt, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);
                                    paidInstallment += paidInstallment;

                                    amt = amt - (int)Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod);

                                }
                                else
                                {
                                    registerFeePaymentPartial(StudAcdID, amt, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);
                                    amt = 0;
                                }

                                FillReceiptPartial(_AccStudentfees_Id, feesReceipts, desc);


                                m += 1;

                            } while (amt > 0);


                        }
                        else
                        {
                            //if (amountPaid > 0)
                            //{
                                var amt = (decimal)item.itemAmount;//amountPaid;
                                do
                                {
                                    partialAmount = feesObj.partialAmount;
                                    if (partialAmount > 0 && amt >= partialAmount && item.PaidAmount < item.TotalAmount)
                                    {
                                        registerFeePaymentPartial(StudAcdID, partialAmount, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);

                                        //registerFeePayment(StudAcdID, amountPaid, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);
                                        m = +1;
                                        partialAmount = 0;
                                    }
                                    else
                                    {
                                        registerFeePaymentPartial(StudAcdID, amt, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);
                                        amt = 0;
                                    }
                                    FillReceiptPartial(_AccStudentfees_Id, feesReceipts, desc);

                                    amt = amt - (int)feesObj.partialAmount;


                                } while (amt > 0);

                                //  registerFeePaymentPartial(StudAcdID, amountPaid, amountBalances, CashAccount, out amount, out desc, feesObj, schoolMonths[m], out _AccStudentfees_Id);
                                //  FillReceiptPartial(_AccStudentfees_Id, feesReceipts, desc);
                            //}
                        }
                    }
                }

                index++;

            }

            string studentQuery = @"SELECT TOP 1  NameFt +' '+ NameM  +' '+ NameFm  NameEn ,NameA  ArabicName,school.GradeSchoolNameA GradeName FROM [campuserp].[dbo].[AdmStudents] student inner join StudentReference ref  on student.StudAcdID = @p0  and ref.IsCurrent = 1 inner join AdmGradesSchool school on school.GradeID=ref.GradeID";

            var studentDetails = context.Database.SqlQuery<StudentViewModel>(studentQuery, StudAcdID).SingleOrDefault();

            StudentReceiptViewModel studentFees = new StudentReceiptViewModel
            {
                NameEn = studentDetails.NameEn,
                ArabicName = studentDetails.ArabicName,
                GradeName = studentDetails.GradeName,
                AmountPaid = amountPaid,
                Balance = amountBalances,
                studentFeesReceipts = feesReceipts,
                ReceiptID = feesReceipts[0].ReceiptID,
                datePayment = DateTime.Now.ToShortDateString(),
                email = studentDetails.Mail,
                pemail = studentDetails.ParentMail
            };


            return studentFees;
        }


        public StudentReceiptViewModel CreateFeeAutomatic(List<automateFeeDetails> viewModel, int StudAcdID, int amountPaid, int CashAccount)
        {
            string query = "select * from dbo.GetFeePaymentTable(@p0)";

            var model = context.Database.SqlQuery<AccStudentFeeViewModel>(query, StudAcdID).ToList();
            int index = 0; decimal amount; string desc = string.Empty; int installment = 0;
            int _AccStudentfees_Id = 0;
            int amountBalances = 0;
            string installemtPeriod = string.Empty;
            int startMonth = _dbUser.startYearDate.Month;
            int EndMonth = _dbUser.endYearDate.Month;
            decimal amountLeftover = amountPaid; decimal actualAmount;
            var Months = normalizeMonths(startMonth, EndMonth);
            int paidInstallment;
            List<StudentFeesReceiptViewModel> feesReceipts = new List<StudentFeesReceiptViewModel>();

            for (var i = 0; i < viewModel.Count; i++)
            {
                var feesObj = model[i];
                if (viewModel[i].isSelected)
                {
                    for (var j = 0; j < viewModel[i].AutomaticCount; j++)
                    {
                        actualAmount = Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod);
                        paidInstallment = feesObj.paidInstallment;
                        if (amountLeftover >= actualAmount && paidInstallment <= feesObj.installmentPeriod)
                        {
                            installemtPeriod = Months[paidInstallment + j];
                            registerFeePayment(StudAcdID, amountPaid, amountBalances, CashAccount, out amount, out desc, feesObj, installemtPeriod, out _AccStudentfees_Id);
                            FillReceipt(_AccStudentfees_Id, feesReceipts, desc);
                            amountLeftover = amountLeftover - actualAmount;
                        }
                    }
                }
            }
            //note:amountBalances may or may not be used in this function verification needed 

            string studentQuery = @"SELECT TOP 1  NameFt +' '+ NameM  +' '+ NameFm  NameEn ,NameA  ArabicName,school.GradeSchoolNameA GradeName FROM [campuserp].[dbo].[AdmStudents] student inner join StudentReference ref  on student.StudAcdID = @p0  and ref.IsCurrent = 1 inner join AdmGradesSchool school on school.GradeID=ref.GradeID";

            var studentDetails = context.Database.SqlQuery<StudentViewModel>(studentQuery, StudAcdID).FirstOrDefault();

            StudentReceiptViewModel studentFees = new StudentReceiptViewModel
            {
                NameEn = studentDetails.NameEn,
                ArabicName = studentDetails.ArabicName,
                GradeName = studentDetails.GradeName,
                AmountPaid = amountPaid,
                Balance = amountLeftover,
                studentFeesReceipts = feesReceipts,
                ReceiptID = feesReceipts[0].ReceiptID,
                datePayment = DateTime.Now.ToShortDateString()
            };

            return studentFees;
        }
        private void FillReceipt(int _AccStudentfees_Id, List<StudentFeesReceiptViewModel> feesReceipts, string desc)
        {
            var AccStudentFees = context.AccStudentFees.Where(x => x.ID == _AccStudentfees_Id).Select(x => new StudentFeesReceiptViewModel
            {
                InvoiceCode = x.AccountReciveCash.InvoiceCode,
                Amount = x.PaidAmount ?? 0,
                originalAmount = (x.originalAmount / x.FeeInstallmentType.Period) ?? 0,
                Discount = x.Discount + x.StudentDiscount,
               // feeType = x.AccountTree.AccountNameEN,
                ReceiptID = x.ReceiptID,
                Description = desc

            }).FirstOrDefault();

            feesReceipts.Add(AccStudentFees);
        }

        private void registerFeePayment(int StudAcdID, decimal amountPaid, int amountBalances, int CashAccount, out decimal amount, out string desc, AccStudentFeeViewModel feesObj, string installemtPeriod, out int _AccStudentfees_Id)
        {

            amount = Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod);

            // 1. insert into daily  journal 
            int? _dailyjornalid = 0;
            int? _AccountReciveCash_Id = 0;
            int? _Accountchequesreceivables_Id = 0;

            AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=11035,
                    AccountTreeID = feesObj.AccountTreeID,
                    Credit =amount
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=11035,
                     AccountTreeID =CashAccount,
                     Debit =amount
                 }
           };
            var _DailyJournal = _AccountDailyJournalHandler.Create();

            desc = string.Format("{0} Fees Payment of {1}",feesObj.AccountNameEN.Replace("Fees Revenue", ""), installemtPeriod); 

            //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
            _DailyJournal.DailyJournalDate = DateTime.Now;
            // _DailyJournal.DocumentNumber = model.docementNumber;
            _DailyJournal.Description = desc;
            _DailyJournal.Note = string.Format("Student of Id ({0}), reference Id ({1}) | fees Payment of ({2}) | Journal Serial: {3}", StudAcdID, feesObj.Ref_Id, installemtPeriod, _DailyJournal.SerialNo); ;//string.Format("Cash Collected :{0} Cash Returned :{1} ", amountPaid, amountBalances);
            //_DailyJournal.Note1 = "سند قبض رقم" + _DailyJournal.SerialNo;
            _DailyJournal.IsPost = false;
            _DailyJournal.JournalType = 15;//for student payments
            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
            _AccountDailyJournalHandler.Create(_DailyJournal);

            _dailyjornalid = context.AccountDailyJournals.ToList().Last().DailyJournalID;


            //___________________________________________ Cash receipt _________________________________________
            AccountReciveCashHandler _AccountReciveCashHandler = new AccountReciveCashHandler();

            var _AccountReciveCashModel = _AccountReciveCashHandler.Create();
            AccountReciveCash _AccountReciveCash = new AccountReciveCash()
            {

                Amount = amount,
                InvoiceCode = _AccountReciveCashModel.InvoiceCode,
                AccountTreeID = feesObj.AccountTreeID,
                CostCenterID = 11035,
                // docementNumber = model.docementNumber,
                //TreasuryID = model.TreasuryID,
                Description = desc,
                OperationDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID = _dbUser.SchoolID,
                //  BankCurrencyID = model.BankCurrencyID,
                // Employee_ID = model.Employee_ID,
                DailyJournalID = _dailyjornalid,



            };
            context.AccountReciveCashes.Add(_AccountReciveCash);
            context.SaveChanges();

            _AccountReciveCash_Id = _AccountReciveCash.ID;//context.AccountReciveCashes.ToList().Last().ID;

            //______________________________________________End of cash receipt ________________________________________


            //installment = context.AccStudentFees.Where(x => x.AccountTreeID == feesObj.AccountTreeID && x.StudRefID == feesObj.Ref_Id).ToList()
            string installmentCode = string.Format("{0}-{1}-{2}", feesObj.AccountTreeID, installemtPeriod.Substring(0, 3), DateTime.Now.Year.ToString().Substring(2, 2));
            var receiptId = context.Database.SqlQuery<long>(@"SELECT CAST(IDENT_CURRENT ({0}) AS BIGINT) + 1 AS Current_Identity;", "AccReceipts").First();


            //---------------------------------------------Registration of expenses --------------------------------------------

            AccStudentFee _AccStudentfees = new AccStudentFee()
            {
                //StudAcdID = model.StudAcdID,
                //StudentConfigrationID = ,
                AccountTreeID = feesObj.AccountTreeID,
                StudRefID = feesObj.Ref_Id,
                Amount = amount,
                PaidAmount = amount,
                TotalAmount = feesObj.TotalAmount,
                Discount = feesObj.Discount,
                StudentDiscount = feesObj.StudentDiscount,
                PaymentWay = 1,
                PaymentType = 1,
                installment = feesObj.installment,
                InstallmentCode = installmentCode,
                Tax=feesObj.tax,
                // bankStatement = model.bankStatement,
                // IsPaid = model.IsPaid,
                OperationDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ReceiptID = receiptId,
                //YearID = model.YearID,
                //GradeID = model.GradeID,
                DailyJournalID = _dailyjornalid,
                CashRecivedID = (_AccountReciveCash_Id > 0) ? _AccountReciveCash_Id : null,
                ChequeRecivedID = (_Accountchequesreceivables_Id > 0) ? _Accountchequesreceivables_Id : null,
                originalAmount = feesObj.Amount
            };
            context.AccStudentFees.Add(_AccStudentfees);
            context.SaveChanges();
            _AccStudentfees_Id = _AccStudentfees.ID;


        }



        private void registerFeePaymentPartial(int StudAcdID, decimal amountPaid, int amountBalances, int CashAccount, out decimal amount, out string desc, AccStudentFeeViewModel feesObj, string installemtPeriod, out int _AccStudentfees_Id)
        {

            amount = amountPaid;//Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod);

            // 1. insert into daily  journal 
            int? _dailyjornalid = 0;
            int? _AccountReciveCash_Id = 0;
            int? _Accountchequesreceivables_Id = 0;

            AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=11035,
                    AccountTreeID = feesObj.AccountTreeID,
                    Credit =amount 
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=11035,
                     AccountTreeID =CashAccount,
                     Debit =amount
                 }
           };
            var _DailyJournal = _AccountDailyJournalHandler.Create();

            desc = string.Format("{0} Fees Partial Payment of {1}", feesObj.AccountNameEN.Replace("Fees Revenue", ""), installemtPeriod);

            //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
            _DailyJournal.DailyJournalDate = DateTime.Now;
            // _DailyJournal.DocumentNumber = model.docementNumber;
            _DailyJournal.Description = desc;
            _DailyJournal.Note = string.Format("Student of Id ({0}), reference Id ({1}) | Partial fees Payment of ({2}) | Journal Serial: {3}", StudAcdID, feesObj.Ref_Id, installemtPeriod, _DailyJournal.SerialNo); ;//string.Format("Cash Collected :{0} Cash Returned :{1} ", amountPaid, amountBalances);
            //_DailyJournal.Note1 = "سند قبض رقم" + _DailyJournal.SerialNo;
            _DailyJournal.IsPost = false;
            _DailyJournal.JournalType = 15;//for student payments
            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
            _AccountDailyJournalHandler.Create(_DailyJournal);

            _dailyjornalid = context.AccountDailyJournals.ToList().Last().DailyJournalID;


            //___________________________________________ Cash receipt _________________________________________
            AccountReciveCashHandler _AccountReciveCashHandler = new AccountReciveCashHandler();

            var _AccountReciveCashModel = _AccountReciveCashHandler.Create();
            AccountReciveCash _AccountReciveCash = new AccountReciveCash()
            {

                Amount = amount,
                InvoiceCode = _AccountReciveCashModel.InvoiceCode,
                AccountTreeID = feesObj.AccountTreeID,
                CostCenterID = 11035,
                // docementNumber = model.docementNumber,
                //TreasuryID = model.TreasuryID,
                Description = desc,
                OperationDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID = _dbUser.SchoolID,
                //  BankCurrencyID = model.BankCurrencyID,
                // Employee_ID = model.Employee_ID,
                DailyJournalID = _dailyjornalid,



            };
            context.AccountReciveCashes.Add(_AccountReciveCash);
            context.SaveChanges();

            _AccountReciveCash_Id = _AccountReciveCash.ID;//context.AccountReciveCashes.ToList().Last().ID;

            //______________________________________________End of cash receipt ________________________________________


            //installment = context.AccStudentFees.Where(x => x.AccountTreeID == feesObj.AccountTreeID && x.StudRefID == feesObj.Ref_Id).ToList()
            string installmentCode = string.Format("{0}-{1}-{2}", feesObj.AccountTreeID, installemtPeriod.Substring(0, 3), DateTime.Now.Year.ToString().Substring(2, 2));
            var receiptId = context.Database.SqlQuery<long>(@"SELECT CAST(IDENT_CURRENT ({0}) AS BIGINT) + 1 AS Current_Identity;", "AccReceipts").First();
            var TotalPaid = context.Database.SqlQuery<decimal>(String.Format("SELECT ISNULL(SUM(PaidAmount),0.00) TotalPaid FROM [campuserp].[dbo].[AccStudentFees] WHERE InstallmentCode ='{0}' AND StudRefID ='{1}'", installmentCode, feesObj.Ref_Id.ToString())).First();


            //---------------------------------------------Registration of expenses --------------------------------------------

            AccStudentFee _AccStudentfees = new AccStudentFee()
            {
                //StudAcdID = model.StudAcdID,
                //StudentConfigrationID = ,
                AccountTreeID = feesObj.AccountTreeID,
                StudRefID = feesObj.Ref_Id,
                Amount = Math.Round(feesObj.TotalAmount / feesObj.installmentPeriod) - TotalPaid,
                PaidAmount = amount,
                TotalAmount = feesObj.TotalAmount,
                Discount = feesObj.Discount,
                StudentDiscount = feesObj.StudentDiscount,
                PaymentWay = 1,
                PaymentType = 1,
                installment = feesObj.installment,
                InstallmentCode = installmentCode,
                Tax = feesObj.tax,
                // bankStatement = model.bankStatement,
                // IsPaid = model.IsPaid,
                OperationDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ReceiptID = receiptId,
                //YearID = model.YearID,
                //GradeID = model.GradeID,
                DailyJournalID = _dailyjornalid,
                CashRecivedID = (_AccountReciveCash_Id > 0) ? _AccountReciveCash_Id : null,
                ChequeRecivedID = (_Accountchequesreceivables_Id > 0) ? _Accountchequesreceivables_Id : null,
                originalAmount = feesObj.Amount
            };
            context.AccStudentFees.Add(_AccStudentfees);
            context.SaveChanges();
            _AccStudentfees_Id = _AccStudentfees.ID;


        }

        private void FillReceiptPartial(int _AccStudentfees_Id, List<StudentFeesReceiptViewModel> feesReceipts, string desc)
        {
            var AccStudentFees = context.AccStudentFees.Where(x => x.ID == _AccStudentfees_Id).Select(x => new StudentFeesReceiptViewModel
            {
                InvoiceCode = x.AccountReciveCash.InvoiceCode,
                Amount = x.PaidAmount ?? 0,
                originalAmount = (x.originalAmount / x.FeeInstallmentType.Period) ?? 0,
                Discount = x.Discount + x.StudentDiscount,
                Tax = x.Tax,
                feeType = x.AccountTree.AccountNameEN,
                ReceiptID = x.ReceiptID,
                Description = desc

            }).FirstOrDefault();

            feesReceipts.Add(AccStudentFees);
        }



        private string[] normalizeMonths(int startMonth, int EndMonth)
        {
            var months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string[] schoolMonths = new string[12];

            bool firstComplete = false;
            for (var i = 0; i < months.Length;)
            {
                if (!firstComplete)
                {
                    for (var j = startMonth; j < 12; j++)
                    {
                        schoolMonths[i] = months[j];
                        if (j == 11) { firstComplete = true; }
                        i++;
                    }
                }
                if (firstComplete)
                {

                    for (var j = 0; j < startMonth; j++)
                    {
                        schoolMonths[i] = months[j];
                        i++;
                    }

                }

            }
            return schoolMonths;
        }


        public AccStudentFeeViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AccStudentFeeViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccStudentFeeViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public AccStudentFeeViewModel Update(AccStudentFeeViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
