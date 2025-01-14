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
   
    public class AccFeeItemHandler : IRepository<AccFeeItemViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(AccFeeItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccFeeItem accFeeItem = new AccFeeItem
            {
                NAME_AR = model.NAME_AR,
                NAME_EN = model.NAME_EN,         
                CREATED_DATE = DateTime.Now,
                CREATED_BY = _dbUser.ID.ToString(),
                     
            };
            Context.AccFeeItems.Add(accFeeItem);
            Context.SaveChanges();
        }

        public AccFeeItemViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var accFeeItem = Context.AccFeeItems.Where(x => x.ID == ID && x.IS_ACTIVE == true).FirstOrDefault();
            if (accFeeItem != null)
            {
                accFeeItem.DELETED_BY = _dbUser.ID.ToString();
                accFeeItem.IS_ACTIVE = true;
                accFeeItem.DELETED_DATE = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccFeeItemViewModel> GetAll()
        {
            return Context.AccFeeItems.Where(x => x.IS_ACTIVE == true)
          .Select(x => new AccFeeItemViewModel
         {
            ID=x.ID,
            NAME_EN=x.NAME_EN,
            NAME_AR=x.NAME_AR
         }).ToList();

         
        }

        public AccFeeItemViewModel GetById(int ID)
        {
            return Context.AccFeeItems.Where(x => x.ID == ID && x.IS_ACTIVE == true)
          .Select(x => new AccFeeItemViewModel { ID =x.ID, NAME_AR = x.NAME_AR, NAME_EN = x.NAME_EN })
          .FirstOrDefault();
        }

        public AccFeeItemViewModel Update(AccFeeItemViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
