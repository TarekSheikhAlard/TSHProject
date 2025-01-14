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
    public class StudentDiscountsHandler : IRepository<StudentDiscountsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public StudentDiscountsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(StudentDiscountsViewModel model)
        {
            AccStudentDiscount studentdiscount = new AccStudentDiscount
            {
                DiscountType = model.DiscountType,
                Disc_Amount = model.Disc_Amount
            };

            Context.AccStudentDiscounts.Add(studentdiscount);
            Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            var studentdiscount = Context.AccStudentDiscounts.Where(c => c.ID == ID).FirstOrDefault();
            if (studentdiscount != null)
            {
                Context.AccStudentDiscounts.Remove(studentdiscount);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<StudentDiscountsViewModel> GetAll()
        {
            return Context.AccStudentDiscounts.Select(x => new StudentDiscountsViewModel
            {
                ID = x.ID,
                DiscountType = x.DiscountType,
                Disc_Amount = x.Disc_Amount.Value
               
            }).ToList();
        }

        public StudentDiscountsViewModel GetById(int ID)
        {
            return Context.AccStudentDiscounts.Where(x => x.ID == ID).Select(x => new StudentDiscountsViewModel
            {
                ID = x.ID,
                DiscountType = x.DiscountType,
                Disc_Amount = x.Disc_Amount.Value
            }).SingleOrDefault();
        }

        public StudentDiscountsViewModel Update(StudentDiscountsViewModel model)
        {
            var studentdiscount = Context.AccStudentDiscounts.Where(c => c.ID == model.ID).FirstOrDefault();
            if (studentdiscount != null)
            {
                studentdiscount.DiscountType = model.DiscountType;
                studentdiscount.Disc_Amount = model.Disc_Amount;
                Context.SaveChanges();

            }
            return model;
        }
    }
}
