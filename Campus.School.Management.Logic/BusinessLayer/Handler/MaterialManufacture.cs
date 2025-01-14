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
    public class MaterialManufactureHandler : IRepository<MaterialManufactureViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(MaterialManufactureViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            MaterialManufacture materialManufacture = new MaterialManufacture
            {
                MaterialNameEn = model.MaterialNameEn,
                MaterialNameAr = model.MaterialNameAr,
                MaterialNumber = model.MaterialNumber,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID,
            };
            Context.MaterialManufactures.Add(materialManufacture);
            Context.SaveChanges();

        }

        public MaterialManufactureViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var material = Context.MaterialManufactures.Where(x => x.Id == ID && x.IsDeleted == false).FirstOrDefault();

            if (material != null)
            {
                material.DeletedbyID = _dbUser.ID;
                material.IsDeleted = true;
                material.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<MaterialManufactureViewModel> GetAll()
        {
            return Context.MaterialManufactures.Where(x => x.IsDeleted == false).Select(x => new MaterialManufactureViewModel
            {

                Id = x.Id,
                MaterialNameEn = x.MaterialNameEn,
                MaterialNameAr = x.MaterialNameAr,
                MaterialNumber = x.MaterialNumber


            }).ToList();
        }
        public List<MaterialManufactureViewModel> GetAllCompanyId(int companyId)
        {
            return Context.MaterialManufactures.Where(x => x.CompanyID == companyId && x.IsDeleted == false).Select(x => new MaterialManufactureViewModel
            {

                Id = x.Id,
                MaterialNameEn = x.MaterialNameEn,
                MaterialNameAr = x.MaterialNameAr,
                MaterialNumber = x.MaterialNumber

            }).ToList();
        }

        public MaterialManufactureViewModel GetById(int ID)
        {
            return Context.MaterialManufactures.Where(x => x.Id == ID && x.IsDeleted == false).Select(x => new MaterialManufactureViewModel
            {
                 Id=x.Id,
                MaterialNameEn = x.MaterialNameEn,
                MaterialNameAr = x.MaterialNameAr,
                MaterialNumber = x.MaterialNumber

            }).FirstOrDefault();
        }

        public MaterialManufactureViewModel Update(MaterialManufactureViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var Units = Context.MaterialManufactures.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();

            Units.MaterialNumber = model.MaterialNumber;
            Units.MaterialNameAr = model.MaterialNameAr;
            Units.MaterialNameEn = model.MaterialNameEn;
            Context.SaveChanges();
            return model;

        }
    }
}
