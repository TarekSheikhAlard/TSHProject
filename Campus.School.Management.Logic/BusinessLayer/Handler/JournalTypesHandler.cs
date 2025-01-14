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
    public class JournalTypesHandler : IRepository<JournalTypesViewModal>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(JournalTypesViewModal model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            JournalType journalType = new JournalType
            {
               NameAr=model.NameAr,
               NameEn=model.NameEn,
               CompanyId=model.companyId,
               CreatedbyID=_dbUser.CreatedbyID,
               CreatedDate=DateTime.Now,
               ModifiedDate=DateTime.Now,
               DeletedDate=DateTime.Now
            };

            Context.JournalTypes.Add(journalType);
            Context.SaveChanges();
        }

        public JournalTypesViewModal Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var journalType = Context.JournalTypes.Where(c => c.ID == ID && c.IsDefault == false).FirstOrDefault();

            if (journalType != null)
            {
                journalType.IsDeleted = true;
                journalType.DeletedbyID = _dbUser.CreatedbyID;
                journalType.DeletedDate = DateTime.Now;
                    
                Context.SaveChanges();
                return true;
            }
            else return false;
        
        }

        public List<JournalTypesViewModal> GetAllByCompany(int companyId)
        {
            return Context.JournalTypes.Where(x => x.CompanyId == companyId && x.IsDeleted==false).Select(x => new JournalTypesViewModal
            {

               ID=x.ID,
               NameAr=x.NameAr,
               NameEn=x.NameEn,
               isDefault=x.IsDefault
              
            }).ToList();
        }
        public List<JournalTypesViewModal> GetAllByCompanyWithDefault(int companyId)
        {
            return Context.JournalTypes.Where(x => x.CompanyId == companyId && x.IsDeleted == false ).Select(x => new JournalTypesViewModal
            {

                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                  isDefault = x.IsDefault

            }).ToList();
        }
        public List<JournalTypesViewModal> GetAll()
        {
            return Context.JournalTypes.Where(x=> x.IsDeleted == false && x.IsDefault == false).Select(x => new JournalTypesViewModal
            {

                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                isDefault = x.IsDefault

            }).ToList();
         
        }


        public JournalTypesViewModal GetById(int ID)
        {
            return Context.JournalTypes.Where(x => x.ID == ID && x.IsDeleted == false && x.IsDefault == false).Select(x => new JournalTypesViewModal
            {

                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn

            }).FirstOrDefault();
        }

        public JournalTypesViewModal Update(JournalTypesViewModal model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
       
            var journalType  = Context.JournalTypes.Where(c => c.ID == model.ID && c.IsDefault == false).FirstOrDefault();
            if (journalType != null)
            {
                journalType.NameAr = model.NameAr;
                journalType.NameEn = model.NameEn;
                journalType.ModifiedbyID = _dbUser.ModifiedbyID;
                journalType.ModifiedDate = DateTime.Now;
                Context.SaveChanges();
            }
            return model;
        }
    }
}
