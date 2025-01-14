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
   public class StageHandler : IRepository<StageViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public StageViewModel Update(StageViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Stage = Context.AdmStages.Where(c => c.Stage_ID == model.Stage_ID).FirstOrDefault();
            if (_Stage != null)
            {
                _Stage.StageNameAr = model.StageNameAr;
                _Stage.StageNameEn = model.StageNameEn;
                _Stage.ModifiedbyID = _dbUser.ID;
                _Stage.ModifiedDate = DateTime.Now;
                Context.SaveChanges();
            }
            return model;

        }

        public List<StageViewModel> GetAll()
        {
            return Context.AdmStages.Where(x => x.IsDeleted == false).Select(c => new StageViewModel
            {
                SchoolD = c.SchoolD,
                StageNameAr = c.StageNameAr,
                StageNameEn = c.StageNameEn,
                Stage_ID = c.Stage_ID
            }).ToList();

        }

        public StageViewModel GetById(int ID)
        {
            return Context.AdmStages.Where(x => x.IsDeleted == false).Where(c => c.Stage_ID == ID).Select(c => new StageViewModel
            {
                IsNew = false,
                SchoolD = c.SchoolD,
                StageNameAr = c.StageNameAr,
                StageNameEn = c.StageNameEn,
                Stage_ID = c.Stage_ID
            }).FirstOrDefault();
        }

        public StageViewModel Create()
        {
            StageViewModel model = new StageViewModel();
            model.IsNew = true;
            return model;
        }

        public void Create(StageViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmStage _Stage = new AdmStage
            {
                SchoolD = model.SchoolD,
                Stage_ID = model.Stage_ID,
                StageNameAr = model.StageNameAr,
                StageNameEn = model.StageNameEn,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,


            };
            Context.AdmStages.Add(_Stage);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Stage = Context.AdmStages.Where(c => c.Stage_ID == ID).FirstOrDefault();
            if (_Stage != null)
            {
                _Stage.IsDeleted = true;
                _Stage.DeletedbyID = _dbUser.ID;
                _Stage.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }


    }
}
  