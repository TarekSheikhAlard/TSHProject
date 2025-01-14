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
    public class MenuPageHandler : IRepository<MenuPageViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public MenuPageViewModel Update(MenuPageViewModel model)
        {
            var _MenuPage = Context.MenuPages.Where(c => c.ID == model.ID).FirstOrDefault();
            if (_MenuPage != null)
            {
                _MenuPage.ID = model.ID;
                _MenuPage.PageName = model.PageName;


                Context.SaveChanges();
            }
            return model;

        }


        public List<MenuPageViewModel> GetAll()
        {
            return Context.MenuPages.Select(c => new MenuPageViewModel
            {
                ID = c.ID,
                PageName = c.PageName,             

            }).ToList();

        }

        public MenuPageViewModel GetById(int ID)
        {
            return Context.MenuPages.Where(c => c.ID == ID ).Select(c => new MenuPageViewModel
            {
                ID = c.ID,
                PageName = c.PageName,
            }).FirstOrDefault();
        }

        public void Create(MenuPageViewModel model)
        {


            MenuPage _MenuPage = new MenuPage
            {
                ID = model.ID,
                PageName = model.PageName,
                

            };
            Context.MenuPages.Add(_MenuPage);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            var _MenuPage = Context.MenuPages.Where(c => c.ID == ID).FirstOrDefault();
            if (_MenuPage != null)
            {
                Context.MenuPages.Remove(_MenuPage);
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public MenuPageViewModel Create()
        {
            MenuPageViewModel model = new MenuPageViewModel();
       
            return model;
        } 
    }
}