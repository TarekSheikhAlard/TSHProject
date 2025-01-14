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
   public class StudentFeesTypeHandler : IRepository<StudentFeesTypeViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();



        public StudentFeesTypeHandler Create()
        {
            throw new NotImplementedException();
        }

      

        public void Create(StudentFeesTypeViewModel model)
        {
            AccStudentFeesType studenFees = new AccStudentFeesType()
            {
               Additionalfees  = model.Additionalfees
                
                };
            Context.AccStudentFeesTypes.Add(studenFees);
            Context.SaveChanges();
            

          
        }

        public bool Delete(int ID)
        {
            var studenFees = Context.AccStudentFeesTypes.Where(c => c.ID == ID).FirstOrDefault();
            if (studenFees != null)
            {
                Context.AccStudentFeesTypes.Remove(studenFees);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<StudentFeesTypeViewModel> GetAll()
        {

            return Context.AccStudentFeesTypes.Select(c => new StudentFeesTypeViewModel
            {
                Additionalfees = c.Additionalfees,
                ID = c.ID
            }).ToList();
        }

        public StudentFeesTypeViewModel GetById(int ID)
        {
            return Context.AccStudentFeesTypes.Where(x => x.ID == ID).Select(x => new StudentFeesTypeViewModel
            { ID = x.ID, Additionalfees = x.Additionalfees  }).FirstOrDefault();

        
 
        }

      

        public StudentFeesTypeViewModel Update(StudentFeesTypeViewModel model)
        {

            var studentfeestype = Context.AccStudentFeesTypes.Where(x => x.ID == model.ID ).FirstOrDefault();
            studentfeestype.Additionalfees = model.Additionalfees;
            Context.SaveChanges();
            return model;


        }

        StudentFeesTypeViewModel IRepository<StudentFeesTypeViewModel>.Create()
        {
            throw new NotImplementedException();
        }
    }
}
