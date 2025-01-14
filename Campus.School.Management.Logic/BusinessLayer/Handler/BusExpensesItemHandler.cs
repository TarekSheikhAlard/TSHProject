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
    public class BusExpensesItemHandler : IRepository<BusExpenseItemViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<BusExpenseItemViewModel> GetAll()
        {
            return Context.BusExpensesItems.Where(c =>  c.IsDeleted == false).Select(c => new BusExpenseItemViewModel
            {
                ID = c.ID,
                Total = c.Total,
                Amount = c.Amount,
                Quantity = c.Quantity,
                ItemDesc = c.ItemDesc,


            }).ToList();

        }

        public BusExpenseItemViewModel GetById(int ID)
        {
         return   Context.BusExpensesItems.Where(c => c.ID == ID && c.IsDeleted == false).Select(c => new BusExpenseItemViewModel
         {
             ID = c.ID,
             Total = c.Total,
             Amount = c.Amount,
             Quantity = c.Quantity,
             ItemDesc = c.ItemDesc,
          

         }).FirstOrDefault();
        }

       
        public void Create(BusExpenseItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var total = model.Quantity * model.Amount;
            BusExpensesItem _BusExpensesItem = new BusExpensesItem()
            {
                BusExpenses_ID = model.BusExpenses_ID,
                Amount = model.Amount,
                Quantity = model.Quantity,
                Total = total,               
                ItemDesc = model.ItemDesc,
                CreatedbyID=_dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,

            };
            Context.BusExpensesItems.Add(_BusExpensesItem);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BusExpensesItem = Context.BusExpensesItems.Where(c => c.ID == ID).FirstOrDefault();
            if (_BusExpensesItem != null)
            {
                _BusExpensesItem.DeletedbyID = _dbUser.ID;
                _BusExpensesItem.IsDeleted = true;
                _BusExpensesItem.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public BusExpenseItemViewModel Update(BusExpenseItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var total = model.Quantity * model.Amount;
            var _BusExpensesItem = Context.BusExpensesItems.Where(c => c.ID == model.ID).FirstOrDefault();
            _BusExpensesItem.Total =total;
            _BusExpensesItem.Amount = model.Amount;
            _BusExpensesItem.Quantity = model.Quantity;
            _BusExpensesItem.ItemDesc = model.ItemDesc;
            _BusExpensesItem.ModifiedDate = DateTime.Now;
            _BusExpensesItem.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }

        public BusExpenseItemViewModel Create()
        {
            BusExpenseItemViewModel model = new BusExpenseItemViewModel();

            return model;
        }
    }
}
