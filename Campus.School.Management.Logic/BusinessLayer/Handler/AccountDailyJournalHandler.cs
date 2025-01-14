using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AccountDailyJournalHandler : IRepository<AccountDailyJournalViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private AccountDailyJournalDetailHandler _AccountDailyJournalDetailHandler = new AccountDailyJournalDetailHandler();
        private PhysicalYearHandler _PhysicalYearHandler = new PhysicalYearHandler();
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        //------------------------------------------------------------------------------------------------------------

        public List<AccountDailyJournalViewModel> GetAll()
        {
            return Context.AccountDailyJournals.Where(c => c.IsDeleted == false &&c.SchoolID==_dbUser.SchoolID).Select(c => new AccountDailyJournalViewModel
            {
                DailyJournalID = c.DailyJournalID,
                Credit = c.Credit,
                Debit = c.Debit,
                DocumentNumber = c.DocumentNumber,
                SerialNo = c.SerialNo,
                DailyJournalDate = c.DailyJournalDate,
                IsApprove = c.IsApprove,
                Note = c.Note,
                Note1=c.Note1,
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYear.PhysicalYearName,
                Description = c.Description,
                Currency_Type = c.BankCurrency.Currency_Type,
                TransationRate = c.TransationRate,
                IsPost = c.IsPost,
                JournalType=(int)c.JournalType,
                AttachedDocName = c.AttachedDocName
            }).ToList();

        }
        public List<AccountDailyJournalViewModel> GetAllByType(int type)
        {
            return Context.AccountDailyJournals.Where(c => c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID && c.JournalType == type).Select(c => new AccountDailyJournalViewModel
            {
                DailyJournalID = c.DailyJournalID,
                Credit = c.Credit,
                Debit = c.Debit,
                DocumentNumber = c.DocumentNumber,
                SerialNo = c.SerialNo,
                DailyJournalDate = c.DailyJournalDate,
                IsApprove = c.IsApprove,
                Note = c.Note,
                Note1 = c.Note1,
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYear.PhysicalYearName,
                Description = c.Description,
                Currency_Type = c.BankCurrency.Currency_Type,
                TransationRate = c.TransationRate,
                IsPost = c.IsPost,
                AttachedDocName = c.AttachedDocName
            }).ToList();

        }


        public AccountDailyJournalViewModel GetById(int ID)
        {
            var model = Context.AccountDailyJournals.Where(c => c.DailyJournalID == ID && c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID).Select(c => new AccountDailyJournalViewModel
            {
               
                DailyJournalID = c.DailyJournalID,
                Credit = c.Credit,
                Debit = c.Debit,
                SerialNo = c.SerialNo,
                DailyJournalDate = c.DailyJournalDate,
                AttachedDocName = c.AttachedDocName,
                IsApprove = c.IsApprove,
                Note = c.Note,
                Note1=c.Note1,
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYear.PhysicalYearName,
                Description = c.Description,
                Currency_Type = c.BankCurrency.Currency_Type,
                TransationRate = c.TransationRate,
                BankCurrencyID = c.BankCurrencyID,
                IsPost = c.IsPost,
                CreatedDate =c.CreatedDate,
                ModifiedDate =c.ModifiedDate,
                CreatedbyID=c.CreatedbyID,
                ModifiedbyID=c.ModifiedbyID,
                JournalType=(int)c.JournalType
            }).FirstOrDefault();

            model.Createdby = Context.SchoolUsers.Where(x => x.ID == model.CreatedbyID).Select(x => x.Name).FirstOrDefault();

            var Name_Modifiy = Context.SchoolUsers.Where(x => x.ID == model.ModifiedbyID).FirstOrDefault();

            if (Name_Modifiy == null)
            {
                model.Modifiedby = model.Createdby;
            }
            else
            {
                model.Modifiedby = Name_Modifiy.Name;
            }
            //select all AccountDailyJournalDetail
            var AccountDailyJournalDetail = Context.AccountDailyJournalDetails.Where(c => c.DailyJournalID == model.DailyJournalID && c.IsDeleted == false)
                .Select(c => new AccountDailyJournalDetailViewModel
                {
                    DailyJournalDetailID = c.DailyJournalDetailID,
                    DailyJournalID = c.DailyJournalID,
                    Credit = c.Credit,
                    Debit = c.Debit,
                    AccountTreeID = c.AccountTreeID,
                    AccountTreeCode = c.AccountTree.AccountTreeCode,
                    AccountNameEN = c.AccountTree.AccountNameEN,
                    Description = c.Description,
                    CostCenterID = c.CostCenterID,
                    CostCenter = c.AccountCostCenter.CostCenter, 
                 
                })
                .ToList();
            if (AccountDailyJournalDetail != null)
            {
                model._AccountDailyJournalDetail = new List<AccountDailyJournalDetailViewModel>();
                foreach (var item in AccountDailyJournalDetail)
                    model._AccountDailyJournalDetail.Add(item);
            }
            return model;
        }

        public void  Create(AccountDailyJournalViewModel model)
        {
            try
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();


                AccountDailyJournal _AccountDailyJournal = new AccountDailyJournal()
                {

                    DocumentNumber = model.DocumentNumber,
                    SerialNo = model.SerialNo,
                    DailyJournalDate = model.DailyJournalDate,
                    SchoolID=_dbUser.SchoolID,
                    CompanyID= _dbUser.CompanyID,
                    PhysicalYearID = _PhysicalYearHandler.GetCurrent().PhysicalYearID,
                    IsApprove = true,
                    Note = model.Note,
                    Note1 = model.Note1,
                    CreatedbyID = _dbUser.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    Description = model.Description,
                    AttachedDocName = model.AttachedDocName,
                    //BankCurrencyID = model.BankCurrencyID,
                    TransationRate = model.TransationRate,
                    IsPost = model.IsPost,
                    JournalType=model.JournalType
                    

                };
                if (model.IsPost == true)
                {
                    _AccountDailyJournal.PostedbyID = _dbUser.ID;
                    _AccountDailyJournal.PostedDate = DateTime.Now;
                }
                decimal tDepit = 0;
                decimal tCredit = 0;
                foreach (var item in model._AccountDailyJournalDetail)
                {

                    if (item.Credit > 0 || item.Debit > 0 &&(item.AccountTreeID!=0))
                    {
                        tDepit += item.Debit;
                        tCredit += item.Credit;
                        AccountDailyJournalDetail itm = new AccountDailyJournalDetail()
                        {
                            Credit = item.Credit,
                            Debit = item.Debit,
                            AccountTreeID = item.AccountTreeID,
                            Description = item.Description,
                            CreatedbyID = _dbUser.ID,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            DeletedDate = DateTime.Now,
                            CostCenterID = item.CostCenterID,
                            SchoolID=_dbUser.SchoolID
                        };
                        _AccountDailyJournal.AccountDailyJournalDetails.Add(itm);
                    }

                }
                _AccountDailyJournal.Credit = tCredit;
                _AccountDailyJournal.Debit = tDepit;

                Context.AccountDailyJournals.Add(_AccountDailyJournal);
                Context.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public bool Delete(int ID)
        {
            var _AccountDailyJournal = Context.AccountDailyJournals.Where(c => c.DailyJournalID == ID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountDailyJournal != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AccountDailyJournal.DeletedbyID = _dbUser.ID;
                _AccountDailyJournal.DeletedDate = DateTime.Now;
                _AccountDailyJournal.IsDeleted = true;
            var _AccountDailyJournalDetail=Context.AccountDailyJournalDetails.Where(x => x.DailyJournalID == ID);
                foreach (var item in _AccountDailyJournalDetail)
                {
                    item.DeletedbyID = _dbUser.ID;
                    item.DeletedDate = DateTime.Now;
                    item.IsDeleted = true;
                }
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AccountDailyJournalViewModel Update(AccountDailyJournalViewModel model)
        {
            var _AccountDailyJournal = Context.AccountDailyJournals.Where(c => c.DailyJournalID == model.DailyJournalID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            
            _AccountDailyJournal.SerialNo = model.SerialNo;
            _AccountDailyJournal.DocumentNumber = model.DocumentNumber;

            _AccountDailyJournal.DailyJournalDate = model.DailyJournalDate;
            _AccountDailyJournal.Note = model.Note;
            _AccountDailyJournal.Note1 = model.Note1;
            _AccountDailyJournal.Description = model.Description;
            _AccountDailyJournal.AttachedDocName = model.AttachedDocName;
            //_AccountDailyJournal.BankCurrencyID = model.BankCurrencyID;
            _AccountDailyJournal.TransationRate = model.TransationRate;

            _AccountDailyJournal.ModifiedbyID = _dbUser.ID;
            _AccountDailyJournal.ModifiedDate = DateTime.Now;

            decimal tDepit = 0;
            decimal tCredit = 0;
            foreach (var items in model._AccountDailyJournalDetail)
            {

                tDepit += items.Debit;
                tCredit += items.Credit;

            }

            _AccountDailyJournal.Debit = tDepit;
            _AccountDailyJournal.Credit = tCredit;

            #region AccountDailyJournalDetail

            var ModelTripListIDs = model._AccountDailyJournalDetail.Select(c => c.DailyJournalDetailID).ToList();
            var DataBaseAccountDailyJournalDetailList = Context.AccountDailyJournalDetails.Where(c => c.DailyJournalID == model.DailyJournalID).ToList();
            var DataBaseAccountDailyJournalDetailIDs = Context.AccountDailyJournalDetails.Where(c => c.DailyJournalID == model.DailyJournalID).Select(c => c.DailyJournalDetailID).ToList();

            //update bus trips
            var AccountDailyJournalDetailListToUpdate = model._AccountDailyJournalDetail.Where(c => DataBaseAccountDailyJournalDetailIDs.Contains(c.DailyJournalDetailID)).ToList();
            foreach (var item in AccountDailyJournalDetailListToUpdate)
                if (item.AccountTreeID != 0)
                {
                    _AccountDailyJournalDetailHandler.Update(item);
                }
            //Delete bus trips
            var DatabaseDailyJournalDetailListToDelete = DataBaseAccountDailyJournalDetailList.Where(c => c.DailyJournalID == model.DailyJournalID && !ModelTripListIDs.Contains(c.DailyJournalDetailID)).ToList();
            foreach (var item in DatabaseDailyJournalDetailListToDelete)
                if (item.AccountTreeID != 0)
                {
                    _AccountDailyJournalDetailHandler.Delete(item.DailyJournalDetailID);
                }
            //add bus trip
            var DatabaseDailyJournalDetailListToAdd = model._AccountDailyJournalDetail.Where(c => !DataBaseAccountDailyJournalDetailIDs.Contains(c.DailyJournalDetailID)).ToList();
            foreach (var item in DatabaseDailyJournalDetailListToAdd)
                if (item.AccountTreeID!=0) {
                    _AccountDailyJournalDetailHandler.Create(item);
                }
            #endregion

            Context.SaveChanges();
            return model;
        }

        public AccountDailyJournalViewModel Create()
        {
            AccountDailyJournalViewModel model = new AccountDailyJournalViewModel();
            var lastID = Context.AccountDailyJournals.OrderByDescending(c => c.DailyJournalID).Select(c => c.DailyJournalID).FirstOrDefault();
            if (lastID > 0)
                model.DailyJournalID = lastID + 1;
            else
                model.DailyJournalID = 1;

            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            var lastSerialNo = Context.AccountDailyJournals.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.DailyJournalID).Count();

            if (lastSerialNo > 0)
                model.SerialNo = (lastSerialNo + 1).ToString() + "-" + DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
            else
                model.SerialNo = "1-" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; ;
            //  model.DailyJournalDate =  DateTime.Now;
            return model;
        }


        public string GetReferenceNo( string jornalDate ) {

            DateTime DateFrom = DateTime.ParseExact(jornalDate, "dd/MM/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            string serialNo;
            var lastSerialNo = Context.AccountDailyJournals.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.DailyJournalID).Count();

            if (lastSerialNo > 0)
                serialNo = (lastSerialNo + 1).ToString() + "-" + DateFrom.Day + "/" + DateFrom.Month + "/" + DateFrom.Year;
            else
                serialNo = "1-" + DateFrom.Day + "/" + DateFrom.Month + "/" + DateFrom.Year;
            return serialNo;
        }

        public List<AccountProfViewModel>accountprof(string From, string To, int? Accounttree)
        {
            //----------------------------بنجيب كل الحسابات اللى تحت المستوى الثانى المختار سواء المستوى الثاالث او الرابع ---------------------//

            List<AccountTreeViewModel> accountlevel_3_4 = new List<AccountTreeViewModel>();

            List<AccountTreeViewModel> accountlevel_4 = new List<AccountTreeViewModel>();

            if (Accounttree != null)
            {
                var accountlevel_3 = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == Accounttree ).ToList();

                foreach (var item in accountlevel_3)
                {
                    accountlevel_4 = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == item.AccountTreeID ).ToList();

                    if (accountlevel_4 == null)
                    {
                        accountlevel_3_4.Add(item);
                    }
                    else
                    {
                        accountlevel_3_4.Add(item);
                        foreach (var items in accountlevel_4)
                        {
                            accountlevel_3_4.Add(items);
                        }
                    }
                }
            }
            else
            {
                accountlevel_3_4 = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeLevel ==3||x.AccountTreeLevel==4 ).OrderBy(x=>x.AccountTreeParentID).ToList();

            }


            //--------------------------------------------------------------------------------------------------------
            //----------------------------بنجيب كل تفاصيل القيود بين التاريخ المحدد -------------------------------

            DateTime DateFrom = DateTime.ParseExact(From, "MM/dd/yyyy", null);
            DateTime DateTo = DateTime.ParseExact(To, "MM/dd/yyyy", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;
            List<AccountDailyJournalDetailViewModel> _AccountDailyJournalDetail = new List<AccountDailyJournalDetailViewModel>();
            var _Dailyjournal = GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo);

            foreach (var item in _Dailyjournal)
            {

                var _AccountDailyJournalDetailitem = _AccountDailyJournalDetailHandler.GetAll().Where(x => x.DailyJournalID == item.DailyJournalID).ToList();

                foreach (var items in _AccountDailyJournalDetailitem)
                {
                    _AccountDailyJournalDetail.Add(items);
                }

            }
            //---------------------------------------------------------------------------------------------------------------------------------------------


            //----------------------------------------------بنحسب لكل حساب الاستاذ بتاعة من  التفاصيل اللى رجعناها ----------------------------------------
            List<AccountProfViewModel> _AccountProfViewModels = new List<AccountProfViewModel>();

            foreach (var item in accountlevel_3_4)
            {
                decimal Debit = 0; decimal Credit = 0; decimal Balance = 0;

                var ll = _AccountDailyJournalDetail.Where(x => x.AccountTreeID == item.AccountTreeID).ToList();

                foreach (var items in ll)
                {
                    Debit += items.Debit;
                    Credit += items.Credit;
                }

                Balance = Debit - Credit;

                AccountProfViewModel _AccountProfViewModel = new AccountProfViewModel()
                {
                    Credit = Credit,
                    Debit = Debit,
                    Balance = Balance,
                    AccountID = item.AccountTreeID,
                    AccountName = item.AccountNameAR
                };

                _AccountProfViewModels.Add(_AccountProfViewModel);

                
            }
            return _AccountProfViewModels;
        }



        //--------------------------------------------------------------الاستاذ المساعد ----------------------------------------------------

        public List<AccountsubsidiaryledgerViewModel> subsidiaryledger(string From, string To, int? Accounttree)
        {
            List<AccountDailyJournalDetailViewModel> _AccountDailyJournalDetail = new List<AccountDailyJournalDetailViewModel>();

            List<AccountsubsidiaryledgerViewModel> _AccountsubsidiaryledgerViewModels = new List<AccountsubsidiaryledgerViewModel>();

            BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();

            var currency = _BankCurrencyHandler.GetAll().Where(x => x.Isdefault == true).FirstOrDefault();

            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var _Dailyjournal = GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost==true);

            foreach (var item in _Dailyjournal)
            {

                var _AccountDailyJournalDetailitem = _AccountDailyJournalDetailHandler.GetAll().Where(x => x.DailyJournalID == item.DailyJournalID).ToList();

                foreach (var items in _AccountDailyJournalDetailitem)
                {
                    _AccountDailyJournalDetail.Add(items);
                }

            }

            var xxx = _AccountDailyJournalDetail.Where(x => x.AccountTreeID == Accounttree).ToList();

            decimal obalance = 0;
            decimal ocuurency = 0;
            foreach (var item in xxx)
            {
                ocuurency = 0;


                if (item.AccountDailyJournal.BankCurrencyID != currency.BankCurrencyID)
                {

                    double? factor = _BankCurrencyHandler.GetAll().Where(x => x.BankCurrencyID == item.AccountDailyJournal.BankCurrencyID).Select(x => x.Factor).FirstOrDefault();
                    if (item.Credit != 0)
                    {
                        ocuurency = item.Credit;
                        item.Credit = item.Credit * (decimal)factor;
                    }
                    else
                    {
                        ocuurency = item.Debit;
                        item.Debit = item.Debit * (decimal)factor;
                    }



                }
                else
                {
                    if (item.Credit > 0)
                    {
                        ocuurency = item.Credit;
                    }
                    else
                    {
                        ocuurency = item.Debit;
                    }

                }
                AccountsubsidiaryledgerViewModel _Accountsubsidiaryledger = new AccountsubsidiaryledgerViewModel()
                {
                    SerialNo =item.AccountDailyJournal.SerialNo,
                    DailyJournalDate=item.AccountDailyJournal.DailyJournalDate,
                    DocumentNumber=item.AccountDailyJournal.DocumentNumber,
                    Description=item.AccountDailyJournal.Description,
                    Currency_Type=item.AccountDailyJournal.BankCurrency.Currency_Type,
                     originalamount=ocuurency,
                    Credit =item.Credit,
                    Debit =item.Debit,
                    Balance = item.Debit-item.Credit+obalance
                   
                };
                obalance = _Accountsubsidiaryledger.Balance;
                _AccountsubsidiaryledgerViewModels.Add(_Accountsubsidiaryledger);
            }

            return _AccountsubsidiaryledgerViewModels;
        }

    }
}
