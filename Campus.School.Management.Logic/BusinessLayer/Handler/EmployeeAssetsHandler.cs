using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.BusinessLayer;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class EmployeeAssetsHandler : IRepository<EmployeeAssetsViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(EmployeeAssetsViewModel model)
        {
      
            EmployeeAsset _employeeAsset = new EmployeeAsset
            {
                Employee_ID = model.Employee_ID,
                AssetId = model.AssetId,
                CreatedbyID = _dbUser.CreatedbyID,
                CreatedDate = DateTime.Now,
                schoolID =_dbUser.SchoolID
                     
            };
            context.EmployeeAssets.Add(_employeeAsset);
            context.SaveChanges();

        }

        public EmployeeAssetsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var employeeAsset = context.EmployeeAssets.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (employeeAsset != null)
            {
                employeeAsset.DeletedbyID = _dbUser.ID;
                employeeAsset.IsDeleted = true;
                employeeAsset.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else return false;
          
        }

        public List<EmployeeAssetsViewModel> GetAll()
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return context.EmployeeAssets.Where(x => x.IsDeleted == false && x.schoolID == _dbUser.SchoolID)
                            .Select(x => new EmployeeAssetsViewModel
                            {
                                ID = x.ID,
                                EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
                                AssetId = x.AssetId,
                                AssetName = culture == "ar" ? x.AssetTree.AssetNameAR : x.AssetTree.AssetNameEN
                            }
                            ).ToList();
        }
        public List<EmployeeAssetsViewModel> GetAllByEmployeeId(int id)
        {
            var culture = Utility.getCultureCookie(false).ToLower();
            return context.EmployeeAssets
                            .Where(x => x.IsDeleted == false && x.Employee_ID == id && x.schoolID == _dbUser.SchoolID) 
                            .Select(x => new EmployeeAssetsViewModel
                            {
                                ID = x.ID,
                                EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
                                AssetId = x.AssetId,
                                AssetName = culture == "ar" ? x.AssetTree.AssetNameAR : x.AssetTree.AssetNameEN
                            }
                            ).ToList();
        }

        public EmployeeAssetsViewModel  GetEmployeeIdFromRecId(int id) {

          return context.EmployeeAssets
                           .Where(x =>x.ID == id && x.schoolID == _dbUser.SchoolID)
                           .Select(x => new EmployeeAssetsViewModel
                           {
                              
                              Employee_ID=x.Employee_ID
                           }
                           ).FirstOrDefault();
      
        }

        public EmployeeAssetsViewModel GetById(int ID)
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return context.EmployeeAssets.Where(x => x.ID == ID && x.IsDeleted == false && x.schoolID == _dbUser.SchoolID)
          .Select(x => new EmployeeAssetsViewModel
          {
              ID = x.ID,
              EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
              AssetId = x.AssetId,
              AssetName = culture == "ar" ? x.AssetTree.AssetNameAR : x.AssetTree.AssetNameEN

          })
          .FirstOrDefault();
          
        }

        public EmployeeAssetsViewModel Update(EmployeeAssetsViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _employeeAsset = context.EmployeeAssets.Where(x => x.ID == model.ID && x.schoolID == _dbUser.SchoolID).FirstOrDefault();

            _employeeAsset.AssetId = model.AssetId;
            _employeeAsset.ModifiedDate = DateTime.Now;
            _employeeAsset.ModifiedbyID = _dbUser.ID;

            context.SaveChanges();
            return model;
          
        }
    }
}
