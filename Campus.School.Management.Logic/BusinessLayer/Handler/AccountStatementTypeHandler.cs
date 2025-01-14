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
    public class AccountStatementTypeHandler : IRepository<AccountStatementTypeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        
        public List<AccountStatementTypeViewModel> GetAll()
        {
            return Context.AccountStatementTypes.Where(x => x.IsDeleted == false)
            .Select(x => new AccountStatementTypeViewModel
            {StatementTypeID=x.StatementTypeID,StatementypeName=x.StatementypeName
            }).ToList();
        }

        public AccountStatementTypeViewModel GetById(int ID)
        {
            return Context.AccountStatementTypes.Where(x => x.IsDeleted == false &&x.StatementTypeID==ID)
         .Select(x => new AccountStatementTypeViewModel
         {
             StatementTypeID = x.StatementTypeID,
             StatementypeName = x.StatementypeName
         }).FirstOrDefault();
        }


        public bool Delete(int ID)
        {
            var _AccountStatementType = Context.AccountStatementTypes.Where(x => x.StatementTypeID == ID).FirstOrDefault();
            if (_AccountStatementType != null)
            {
                _AccountStatementType.IsDeleted = true;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public void Create(AccountStatementTypeViewModel model)
        {
            AccountStatementType _AccountStatementType = new AccountStatementType()
            {
                StatementypeName =model.StatementypeName
            };
            Context.AccountStatementTypes.Add(_AccountStatementType);
            Context.SaveChanges();
        }

        public AccountStatementTypeViewModel Update(AccountStatementTypeViewModel model)
        {
            var _AccountStatementType = Context.AccountStatementTypes.Where(x => x.StatementTypeID == model.StatementTypeID).FirstOrDefault();
            _AccountStatementType.StatementypeName = model.StatementypeName;
            Context.SaveChanges();
            return model;
        }



   

     
        public AccountStatementTypeViewModel Create()
        {
            throw new NotImplementedException();
        }










    }
}
