using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class BusExpensesHandler : IRepository<BusExpenseViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private BusExpensesItemHandler _BusExpensesItemHandler = new BusExpensesItemHandler();
        private string lang = "en";
        public List<BusExpenseViewModel> GetAll()
        {
            lang = Utility.getCultureCookie(false).ToLower();

            return Context.BusExpenses.Where(c =>  c.IsDeleted == false).Select(c => new BusExpenseViewModel
            {
               ID = c.ID,
               Employee_ID = c.Employee_ID,
               EmployeeName = lang == "ar" ? c.AdmEmployee.NameA : c.AdmEmployee.NameE,
                Bus = c.Bus,
                BusName = c.Bus.BusNo,
                BusID = c.BusID,
                SerialNo = c.SerialNo,
               DocumentNumber =  c.DocumentNumber,
               Total=c.Total,
               OperationDate = c.OperationDate,
                Place = c.Place,

               //_BusExpensesItems = c.BusExpensesItems
            }).ToList();

        }
        public BusExpenseViewModel GetById(int ID)
        {
            lang = Utility.getCultureCookie(false).ToLower();

            var model = Context.BusExpenses.Where(c => c.ID == ID && c.IsDeleted == false).Select(c => new BusExpenseViewModel
            {
                ID = c.ID,
                Employee_ID = c.Employee_ID,
                EmployeeName = lang == "ar" ? c.AdmEmployee.NameA : c.AdmEmployee.NameE,
                BusID =c.BusID,
                Bus = c.Bus,
                BusName = c.Bus.BusNo,
                SerialNo = c.SerialNo,
                DocumentNumber = c.DocumentNumber,
                Total = c.Total,
                OperationDate = c.OperationDate,
                Place = c.Place,
                //_BusExpensesItems = c.BusExpensesItems
            }).FirstOrDefault();

            //select all BusExpensesItem
            var BusExpensesItem = Context.BusExpensesItems.Where(c => c.BusExpenses_ID == model.ID && c.IsDeleted==false)
                .Select(c => new BusExpenseItemViewModel
                {
                    ID = c.ID,
                    BusExpenses_ID = c.BusExpenses_ID,
                    Amount = c.Amount,
                    Quantity = c.Quantity,
                    ItemDesc = c.ItemDesc,
                    Total = c.Total,
                })
                .ToList();
            if (BusExpensesItem != null)
            {
                model._BusExpensesItems = new List<BusExpenseItemViewModel>();
                foreach (var item in BusExpensesItem)
                    model._BusExpensesItems.Add(item);
            }
            return model;
        }

        public GenericVModel GetTotalExpenses(DateTime DateFrom, DateTime DateTo)
        {

            var list = Context.BusExpenses.Where(x => x.OperationDate >= DateFrom && x.OperationDate <= DateTo && x.IsDeleted == false).ToList();

            var _TotalExpenses = decimal.Parse(list.Select(t => t.Total).Sum().ToString());

            GenericVModel _list = new GenericVModel()
            {

                ItemName = "Bus Expenses",
                Value = _TotalExpenses,
                TypeID = 2,
                Type = "Expenses",
            };
            return _list;
        }
        public void Create(BusExpenseViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            BusExpens _BusExpenses = new BusExpens()
            {
                Employee_ID = model.Employee_ID,
                BusID = model.BusID,
                DocumentNumber = model.DocumentNumber,
                SerialNo = model.SerialNo,
                OperationDate = model.OperationDate,
                Place = model.Place,
                CreatedbyID=_dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,

            };
            decimal Total = 0;
            foreach (var item in model._BusExpensesItems)
            {
                var itemTotal = item.Quantity * item.Amount;
                BusExpensesItem itm = new BusExpensesItem()
                {
                    Total = itemTotal,
                    Amount = item.Amount,
                    Quantity = item.Quantity,
                    ItemDesc = item.ItemDesc,
                    CreatedbyID=_dbUser.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,

                };

                _BusExpenses.BusExpensesItems.Add(itm);
                Total = Total + itm.Total;
            } 
            _BusExpenses.Total = Total;

            Context.BusExpenses.Add(_BusExpenses);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BusExpenses = Context.BusExpenses.Where(c => c.ID == ID).FirstOrDefault();
            if (_BusExpenses != null)
            {
                _BusExpenses.DeletedbyID = _dbUser.ID;
                _BusExpenses.IsDeleted = true;
                _BusExpenses.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public BusExpenseViewModel Update(BusExpenseViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BusExpenses = Context.BusExpenses.Where(c => c.ID == model.ID).FirstOrDefault();
            _BusExpenses.Employee_ID = model.Employee_ID;
            _BusExpenses.BusID = model.BusID;
            _BusExpenses.SerialNo = model.SerialNo;
            _BusExpenses.DocumentNumber = model.DocumentNumber;
            _BusExpenses.Place = model.Place;
            _BusExpenses.OperationDate = model.OperationDate;
            _BusExpenses.ModifiedDate = DateTime.Now;
            _BusExpenses.ModifiedbyID = _dbUser.ID;
            _BusExpenses.Total = 0;

            #region BusExpensesItem

            var ModelTripListIDs = model._BusExpensesItems.Select(c => c.ID).ToList();
            var DataBaseBusExpensesItemList = Context.BusExpensesItems.Where(c => c.BusExpenses_ID == model.ID).ToList();
            var DataBaseBusExpensesItemIDs = Context.BusExpensesItems.Where(c => c.BusExpenses_ID == model.ID).Select(c => c.ID).ToList();

            //update bus trips
            var BusExpensesItemListToUpdate = model._BusExpensesItems.Where(c => DataBaseBusExpensesItemIDs.Contains(c.ID)).ToList();
            foreach (var item in BusExpensesItemListToUpdate)
            {
                _BusExpensesItemHandler.Update(item);
                _BusExpenses.Total += item.Quantity * item.Amount;
            }

            //Delete bus trips
            var DatabaseDailyJournalItemListToDelete = DataBaseBusExpensesItemList.Where(c => c.BusExpenses_ID == model.ID && !ModelTripListIDs.Contains(c.ID)).ToList();
            foreach (var item in DatabaseDailyJournalItemListToDelete)
                _BusExpensesItemHandler.Delete(item.ID);

            //add bus trip
            var DatabaseDailyJournalItemListToAdd = model._BusExpensesItems.Where(c => !DataBaseBusExpensesItemIDs.Contains(c.ID)).ToList();
            foreach (var item in DatabaseDailyJournalItemListToAdd)
            {
                _BusExpensesItemHandler.Create(item);
                _BusExpenses.Total += item.Total;
            }

            #endregion

            Context.SaveChanges();
            return model;
        }

        public BusExpenseViewModel Create()
        {
            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;

            BusExpenseViewModel model = new BusExpenseViewModel();
           
           
           
            var lastSerialNo = Context.BusExpenses.Where(c=>  c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.ID );
            if (lastSerialNo.Count() > 0) model.SerialNo = (lastSerialNo.Count() + 1).ToString()+"/" + DateTime.Now.Day; 
            else model.SerialNo = "1/"+DateTime.Now.Day;
            


            var lastID = lastSerialNo.Select(c => c.ID).FirstOrDefault();
            if (lastID > 0) model.ID = lastID + 1;
            else model.ID = 1;

            return model;
        }

       
    }
}
