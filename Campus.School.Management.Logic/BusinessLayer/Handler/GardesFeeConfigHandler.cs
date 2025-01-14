using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class GardesFeeConfigHandler : IRepository<GradesFeeConfigViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(GradesFeeConfigViewModel model)
        {
            throw new NotImplementedException();
        }

        public GradesFeeConfigViewModel Create()
        {
            throw new NotImplementedException();
        }
        public bool Upsert(List<GradesFeeConfigViewModel> model)
        {
            foreach (var item in model)
            {
                if (item.Id != 0 && item.Id != null)
                {
                    var gradeFees = Context.GradesFeeConfigs.Where(x => x.Id == item.Id).SingleOrDefault();

                    gradeFees.Discount = item.Discount;
                    gradeFees.Amount = item.Amount;
                   // gradeFees.Tax = item.Tax;
                    gradeFees.InstallmentType = item.InstallmentType;
                    gradeFees.ModifiedbyID = _dbUser.ModifiedbyID;
                    gradeFees.ModifiedDate = DateTime.Now;
                    gradeFees.Priority = item.Priority;
                }
                else
                {
                    GradesFeeConfig gradesFeeConfig = new GradesFeeConfig()
                    {
                        GradeID = item.GradeID,
                        AccountTreeID = item.AccountTreeID,
                        Amount = item.Amount,
                        Discount = item.Discount,
                       // Tax = item.Tax,
                        Priority = item.Priority,
                        InstallmentType=item.InstallmentType,
                        CreatedbyID = _dbUser.ID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DeletedDate = DateTime.Now,
                        SchoolID = _dbUser.SchoolID

                    };

                    Context.GradesFeeConfigs.Add(gradesFeeConfig);
                }
                Context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<GradesFeeConfigViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public GradesFeeConfigViewModel GetById(int ID)
        {
           

            throw new NotImplementedException();
        }
        public List<GradesFeeConfigViewModel> GetAllByGradeId(int ID)
        {
            var lang = Utility.getCultureCookie(false);

            var query = string.Format(@"Select ISNULL(config.Id,0) Id,ISNULL(config.GradeID,'{0}') GradeID,tree.AccountTreeID,tree.AccountName{2} AccountTreeName,ISNULL(config.Amount,0) Amount,ISNULL(config.Tax,0) Tax,ISNULL(config.Discount,0) Discount,ISNULL(config.[Priority],0) [Priority],ISNULL(config.[InstallmentType],0) InstallmentType,ISNULL(Installment.Name{2},0) InstallmentName,tree.SchoolID 
                      from [dbo].[GradesFeeConfig] config 
                      right outer join AccountTree tree 
                      on config.[AccountTreeID] = tree.AccountTreeID 
                      and config.SchoolID = tree.SchoolID
                      and config.GradeID ='{0}'
                      left join [dbo].[FeeInstallmentTypes] Installment
                      on Installment.Id=config.[InstallmentType]
                      Where tree.AccountTreeParentID='27'
                      and tree.SchoolID='{1}'  and  tree.AccountNameEn like '%Fees Revenue'
                      order by config.[Priority] 
                    ", ID, _dbUser.SchoolID, lang);

           return Context.Database.SqlQuery<GradesFeeConfigViewModel>(query).ToList();

         
        }
        public GradesFeeConfigViewModel Update(GradesFeeConfigViewModel model)
        {
            throw new NotImplementedException();
        }

        
    }
}
