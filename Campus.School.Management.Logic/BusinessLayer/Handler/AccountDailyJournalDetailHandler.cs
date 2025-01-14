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
    public class AccountDailyJournalDetailHandler : IRepository<AccountDailyJournalDetailViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        AccountTreeHandler _AccounttreeHandler = new AccountTreeHandler();

        public List<AccountDailyJournalDetailViewModel> GetAll()
        {
            return Context.AccountDailyJournalDetails.Where(c =>  c.IsDeleted == false).Select(c => new AccountDailyJournalDetailViewModel
            {
               DailyJournalDetailID = c.DailyJournalDetailID,
               DailyJournalID = c.DailyJournalID,
               Credit = c.Credit,
               Debit = c.Debit,  
               AccountTreeID=c.AccountTreeID,
                AccountNameEN = c.AccountTree.AccountNameEN,
               Description=c.Description,
               CostCenterID=c.CostCenterID,
               CostCenter=c.AccountCostCenter.CostCenter,
               AccountDailyJournal=c.AccountDailyJournal
              
            }).ToList();

        }

        public AccountDailyJournalDetailViewModel GetById(int ID)
        {
         return   Context.AccountDailyJournalDetails.Where(c => c.DailyJournalID == ID && c.IsDeleted == false).Select(c => new AccountDailyJournalDetailViewModel
         {
             DailyJournalDetailID = c.DailyJournalDetailID,
             DailyJournalID = c.DailyJournalID,
             Credit = c.Credit,
             Debit = c.Debit,
             AccountTreeID = c.AccountTreeID,
             AccountNameEN = c.AccountTree.AccountNameEN,
             Description = c.Description,
             CostCenterID = c.CostCenterID,
             CostCenter = c.AccountCostCenter.CostCenter

         }).FirstOrDefault();
        }

        public List<AccountDailyJournalDetailViewModel> GetJournalDetailByAccountTree(int AccountTreeID)
        {
            return Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.AccountTreeID == AccountTreeID).Select(c => new AccountDailyJournalDetailViewModel
            {
                DailyJournalID = c.DailyJournalID,
                Credit = c.Credit,
                Debit = c.Debit,
                DailyJournalDetailID = c.DailyJournalDetailID,
                AccountTreeID = c.AccountTreeID,
                AccountNameEN = c.AccountTree.AccountNameEN,
                Description = c.Description,
                DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
            }).OrderBy(x => x.DailyJournalDate).ToList();

        }

        public List<AccountDailyJournalDetailViewModel> GetLedgerByAccountTree(int AccountTreeID)
        {
            List<AccountDailyJournalDetailViewModel> _list = new List<AccountDailyJournalDetailViewModel>();

            var DailyJournalIDList =Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.AccountTreeID == AccountTreeID).Select(c => new 
            {
                DailyJournalID = c.DailyJournalID,
                
            }).Distinct().ToList();
           
            foreach (var item in DailyJournalIDList)
            {
                var _Alllist = Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.DailyJournalID == item.DailyJournalID );
                var _currentAccount = _Alllist.Where(c =>  c.AccountTreeID == AccountTreeID).FirstOrDefault();
                
                if (_currentAccount.Credit > 0)
                {
                    var _currentHand = _Alllist.Where(c => c.Credit > 0).Count();
                    var _SecoundHand = _Alllist.Where(c => c.Debit > 0).Count();
                    var switchValue = _currentHand == 1 ? 1 : 2;

                    switch (switchValue)
                    {
                        case 1:// من طرف واحد  
                            _list.AddRange(GetCreditCase1(_SecoundHand, _Alllist,AccountTreeID));
                            break;
                        case 2:// من اكثر من طرف   
                            _list.AddRange(GetCreditCase2(_SecoundHand, _Alllist, AccountTreeID));
                            break;
                        default:
                            break;
                    }
                   
                }
                else
                {
                    var _currentHand = _Alllist.Where(c => c.Debit > 0).Count();
                    var _SecoundHand = _Alllist.Where(c => c.Credit > 0).Count();
                    var switchValue = _currentHand == 1 ? 1 : 2;

                    switch (switchValue)
                    {
                        case 1:// من طرف واحد  
                            _list.AddRange(GetDebitCase1(_SecoundHand, _Alllist, AccountTreeID));
                            break;
                        case 2:// من اكثر من طرف   
                            _list.AddRange(GetDebitCase2(_SecoundHand, _Alllist, AccountTreeID));
                            break;
                        default:
                            break;
                    }
                    //var itm = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Credit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    //{
                    //    DailyJournalID = c.DailyJournalID,
                    //    Credit = 0,//c.Credit,
                    //    Debit = _currentAccount.Debit,//c.Debit,
                    //    DailyJournalDetailID = c.DailyJournalDetailID,
                    //    AccountTreeID = c.AccountTreeID,
                    //    AccountTreeName = c.AccountTree.AccountTreeName,
                    //    Description = c.Description,
                    //    DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    //});
                    //_list.AddRange(itm);
                }

            }
            return _list.OrderBy(x => x.DailyJournalDate).ToList();
        }

        private IEnumerable<AccountDailyJournalDetailViewModel> GetCreditCase1(int _SecoundHand, IQueryable<AccountDailyJournalDetail> _Alllist,int AccountTreeID)
        {
            var _currentAccount = _Alllist.Where(c => c.AccountTreeID == AccountTreeID).FirstOrDefault();
            switch (_SecoundHand)
            {
                case 1:
                    var itm = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Debit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = _currentAccount.Credit,// c.Credit,
                        Debit = 0,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm;
                case 2:
                    var itm2 = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Debit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = c.Debit,// c.Credit,
                        Debit = 0,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm2;
                default:
                    break;
            }
            return null;
        }

        private IEnumerable<AccountDailyJournalDetailViewModel> GetCreditCase2(int _SecoundHand, IQueryable<AccountDailyJournalDetail> _Alllist, int AccountTreeID)
        {
            var _currentAccount = _Alllist.Where(c => c.AccountTreeID == AccountTreeID).FirstOrDefault();
            switch (_SecoundHand)
            {
                case 1:
                    var itm = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Debit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = _currentAccount.Credit,// c.Credit,
                        Debit = 0,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm;
                case 2:
                    var itm2 = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Debit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = c.Debit,// c.Credit,
                        Debit = 0,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = "Accounts",//c.AccountTree.AccountTreeName,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm2;
                default:
                    break;
            }
            return null;
        }

        private IEnumerable<AccountDailyJournalDetailViewModel> GetDebitCase1(int _SecoundHand, IQueryable<AccountDailyJournalDetail> _Alllist, int AccountTreeID)
        {
            var _currentAccount = _Alllist.Where(c => c.AccountTreeID == AccountTreeID).FirstOrDefault();
            switch (_SecoundHand)
            {
                case 1:
                    var itm = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Credit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit =0 ,// c.Credit,
                        Debit = _currentAccount.Debit,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm;
                case 2:
                    var itm2 = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Credit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = 0,// c.Credit,
                        Debit = c.Credit,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm2;
                default:
                    break;
            }
            return null;
        }

        private IEnumerable<AccountDailyJournalDetailViewModel> GetDebitCase2(int _SecoundHand, IQueryable<AccountDailyJournalDetail> _Alllist, int AccountTreeID)
        {
            var _currentAccount = _Alllist.Where(c => c.AccountTreeID == AccountTreeID).FirstOrDefault();
            switch (_SecoundHand)
            {
                case 1:
                    var itm = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Credit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit =0 ,// c.Credit,
                        Debit = _currentAccount.Debit,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = c.AccountTree.AccountNameEN,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm;
                case 2:
                    var itm2 = _Alllist.Where(c => c.AccountTreeID != AccountTreeID && c.Credit > 0).Select(c => new AccountDailyJournalDetailViewModel
                    {
                        DailyJournalID = c.DailyJournalID,
                        Credit = 0,// c.Credit,
                        Debit = c.Credit,//c.Debit,
                        DailyJournalDetailID = c.DailyJournalDetailID,
                        AccountTreeID = c.AccountTreeID,
                        AccountNameEN = "Accounts",//c.AccountTree.AccountTreeName,
                        Description = c.Description,
                        DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                    });

                    return itm2;
                default:
                    break;
            }
            return null;
        }


        public List<AccountDailyJournalDetailItemViewModel> GetTrialBalance(string From,string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "MM/dd/yyyy", null);
            DateTime DateTo = DateTime.ParseExact(To, "MM/dd/yyyy", null);          
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            List<AccountDailyJournalDetailViewModel> _list = new List<AccountDailyJournalDetailViewModel>();
            List<AccountDailyJournalDetailViewModel> _prplist = new List<AccountDailyJournalDetailViewModel>();
            List<AccountDailyJournalDetailItemViewModel> _finallist = new List<AccountDailyJournalDetailItemViewModel>();
            var DailyJournalIDList = Context.AccountDailyJournals.Where(c => c.IsDeleted == false && c.DailyJournalDate >= DateFrom && c.DailyJournalDate <= DateTo).Select(c => new
            {
                DailyJournalID = c.DailyJournalID,

            }).Distinct().ToList();
            foreach (var item in DailyJournalIDList)
            {
                var itm = Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.DailyJournalID == item.DailyJournalID ).Select(c => new AccountDailyJournalDetailViewModel
                {
                    DailyJournalID = c.DailyJournalID,
                    Credit = c.Credit,
                    Debit = c.Debit,
                    DailyJournalDetailID = c.DailyJournalDetailID,
                    AccountTreeID = c.AccountTreeID,
                    AccountNameEN = c.AccountTree.AccountNameEN,
                    Description = c.Description,
                    DailyJournalDate = c.AccountDailyJournal.DailyJournalDate
                });
                _list.AddRange(itm);
            }
           
            var _accountTreeList = _list.Select(x => new { x.AccountTreeID,x.AccountNameEN }).Distinct();
           
            foreach (var item in _accountTreeList.ToList())
            {
                List<int> _journalIDList = _list.Where(c => c.AccountTreeID == item.AccountTreeID ).Select(x => x.DailyJournalID ).Distinct().ToList();

                foreach (var i in _journalIDList)
                {
                    var _Alllist = Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.DailyJournalID == i);
                    //var _Alllist = _list.Where(c => c.IsDeleted == false && c.DailyJournalID == i);
                    var _currentAccount = _Alllist.Where(c => c.AccountTreeID == item.AccountTreeID).FirstOrDefault();
                    if (_currentAccount.Credit > 0)
                    {
                        var _currentHand = _Alllist.Where(c => c.Credit > 0).Count();
                        var _SecoundHand = _Alllist.Where(c => c.Debit > 0).Count();
                        var switchValue = _currentHand == 1 ? 1 : 2;

                        switch (switchValue)
                        {
                            case 1:// من طرف واحد  
                                _prplist.AddRange(GetCreditCase1(_SecoundHand, _Alllist, item.AccountTreeID));
                                break;
                            case 2:// من اكثر من طرف   
                                _prplist.AddRange(GetCreditCase2(_SecoundHand, _Alllist, item.AccountTreeID));
                                break;
                            default:
                                break;
                        }
                        
                    }
                    else
                    {
                        var _currentHand = _Alllist.Where(c => c.Debit > 0).Count();
                        var _SecoundHand = _Alllist.Where(c => c.Credit > 0).Count();
                        var switchValue = _currentHand == 1 ? 1 : 2;

                        switch (switchValue)
                        {
                            case 1:// من طرف واحد  
                                _prplist.AddRange(GetDebitCase1(_SecoundHand, _Alllist, item.AccountTreeID));
                                break;
                            case 2:// من اكثر من طرف   
                                _prplist.AddRange(GetDebitCase2(_SecoundHand, _Alllist, item.AccountTreeID));
                                break;
                            default:
                                break;
                        }

                    }
                }
                var total = _prplist.Where(c => c.AccountTreeID != item.AccountTreeID && _journalIDList.Contains(c.DailyJournalID)).GroupBy(x => x.DailyJournalID).Select(x => new {
                    totalCredit = x.Sum(y => y.Credit),
                    totalDebit = x.Sum(y => y.Debit)
                }).Distinct();
                decimal totalDebit =0, totalCredit = 0;
                decimal diffDebit = 0, diffCredit = 0;
                foreach (var i in total)
                {
                    totalDebit +=i.totalDebit;
                    totalCredit += i.totalCredit;

                }
                if (totalDebit > totalCredit)
                {
                    diffDebit = totalDebit - totalCredit;
                }
                else
                {
                    diffCredit = totalCredit - totalDebit;
                }
               

                _finallist.Add(new AccountDailyJournalDetailItemViewModel() {
                    AccountTreeID = item.AccountTreeID,
                    AccountNameEN = item.AccountNameEN,
                    TotalCredit = totalCredit,
                    TotalDebit = totalDebit,
                    DifferentCredit =diffCredit,
                    DifferentDebit=diffDebit
                    });
                    
            }
            return _finallist.ToList();
        }


        public GenericVModel GetTotalExpenses(DateTime DateFrom, DateTime DateTo)
        {
            List<int> _ExpensesAccountTree =_AccounttreeHandler.GetChildAccountTreeID(3).Where(x=> x!= 15 && x!= 16).ToList();
            var list = Context.AccountDailyJournalDetails.Where(x => _ExpensesAccountTree.Contains(x.AccountTreeID) && x.AccountDailyJournal.DailyJournalDate >= DateFrom && x.AccountDailyJournal.DailyJournalDate <= DateTo).ToList();

            var _TotalExpenses = decimal.Parse(list.Select(t => t.Debit).Sum().ToString());

            GenericVModel _list = new GenericVModel()
            {

                ItemName = "General Expenses",
                Value = _TotalExpenses,
                TypeID = 2,
                Type = "Expenses",
            };
            return _list;
        }

       

        public void Create(AccountDailyJournalDetailViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
           
            AccountDailyJournalDetail _AccountDailyJournalDetail = new AccountDailyJournalDetail()
            {
                DailyJournalID = model.DailyJournalID,
                Credit = model.Credit,
                Debit = model.Debit,
                AccountTreeID = model.AccountTreeID,               
                Description = model.Description,
                CreatedbyID = _dbUser.ID,
                CostCenterID = model.CostCenterID,
               
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,

            };
            Context.AccountDailyJournalDetails.Add(_AccountDailyJournalDetail);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            var _AccountDailyJournalDetail = Context.AccountDailyJournalDetails.Where(c => c.DailyJournalDetailID == ID).FirstOrDefault();
            if (_AccountDailyJournalDetail != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AccountDailyJournalDetail.DeletedbyID = _dbUser.ID;
                _AccountDailyJournalDetail.DeletedDate = DateTime.Now;
                _AccountDailyJournalDetail.IsDeleted = true;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AccountDailyJournalDetailViewModel Update(AccountDailyJournalDetailViewModel model)
        {
            var _AccountDailyJournalDetail = Context.AccountDailyJournalDetails.Where(c => c.DailyJournalDetailID == model.DailyJournalDetailID).FirstOrDefault();
            _AccountDailyJournalDetail.Debit = model.Debit;
            _AccountDailyJournalDetail.Credit = model.Credit;
            _AccountDailyJournalDetail.AccountTreeID = model.AccountTreeID;               
                _AccountDailyJournalDetail.Description = model.Description;
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            _AccountDailyJournalDetail.ModifiedbyID = _dbUser.ID;
            _AccountDailyJournalDetail.ModifiedDate = DateTime.Now;
            _AccountDailyJournalDetail.CostCenterID = model.CostCenterID;
            Context.SaveChanges();
            return model;
        }

        public AccountDailyJournalDetailViewModel Create()
        {
            AccountDailyJournalDetailViewModel model = new AccountDailyJournalDetailViewModel();

            return model;
        }
    }
}
