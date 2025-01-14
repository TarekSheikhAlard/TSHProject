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
    public class BankCurrencyHandler : IRepository<BankCurrencyViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<BankCurrencyViewModel> GetAll()
        {
            return Context.BankCurrencies
                          .Where(x => x.IsDeleted == false)
                          .Select(x =>
            new BankCurrencyViewModel { BankCurrencyID = x.BankCurrencyID, Currency_Type = x.Currency_Type, Factor = x.Factor, Isdefault = x.Isdefault }).OrderByDescending(x => x.Isdefault).ToList();
        }
        public List<BankCurrencyViewModel> GetAllByCompany(int CompanyId)
        {
            var result = Context.BankCurrencies
                          .Where(x => x.IsDeleted == false && x.CompanyID == CompanyId)
                          .Select(x =>
            new BankCurrencyViewModel
            {
                BankCurrencyID = x.BankCurrencyID,
                Currency_Type = x.Currency_Type,
                Factor = x.Factor,
                Isdefault = x.Isdefault
            }).OrderByDescending(x => x.BankCurrencyID).ToList();
            return result;
        }
        public BankCurrencyViewModel GetById(int ID)
        {
            return Context.BankCurrencies.Where(x => x.BankCurrencyID == ID && x.IsDeleted == false).Select(x =>
             new BankCurrencyViewModel { BankCurrencyID = x.BankCurrencyID, Currency_Type = x.Currency_Type, Factor = x.Factor, Isdefault = x.Isdefault }).FirstOrDefault();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BankCurrencies = Context.BankCurrencies.Where(c => c.BankCurrencyID == ID).FirstOrDefault();
            if (_BankCurrencies != null)
            {
                _BankCurrencies.DeletedbyID = _dbUser.ID;
                _BankCurrencies.DeletedDate = DateTime.Now;
                _BankCurrencies.IsDeleted = true;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public void Create(BankCurrencyViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            BankCurrency _BankCurrencies = new BankCurrency()
            {
                Currency_Type = model.Currency_Type,
                Factor = model.Factor,
                CompanyID = model.CompanyID,
                DeletedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                ModifiedDate = DateTime.Now,
                Isdefault = model.Isdefault

            };



            Context.BankCurrencies.Add(_BankCurrencies);

            if (model.Isdefault == true)
            {
                var _defult = Context.BankCurrencies.Where(x => x.Isdefault == true && x.IsDeleted == false).FirstOrDefault();


            }


            Context.SaveChanges();
        }

        public BankCurrencyViewModel Update(BankCurrencyViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BankCurrencies = Context.BankCurrencies.Where(c => c.BankCurrencyID == model.BankCurrencyID).FirstOrDefault();
            if (_BankCurrencies != null)
            {
                _BankCurrencies.ModifiedbyID = _dbUser.ID;
                _BankCurrencies.ModifiedDate = DateTime.Now;
                _BankCurrencies.Currency_Type = model.Currency_Type;
                _BankCurrencies.Factor = model.Factor;

                if (model.Isdefault == true)
                {
                    var _defult = Context.BankCurrencies.Where(x => x.Isdefault == true && x.IsDeleted == false).FirstOrDefault();
                    if (_defult != null)
                    {
                        _defult.Isdefault = false;
                    }
                }
                _BankCurrencies.Isdefault = model.Isdefault;
                Context.SaveChanges();
            }
            return model;

        }

        public BankCurrencyViewModel Create()
        {
            throw new NotImplementedException();
        }

    }
}
