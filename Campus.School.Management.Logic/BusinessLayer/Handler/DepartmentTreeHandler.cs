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
    public class DepartmentTreeHandler : IRepository<DepartmentTreeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        //-------------------------------Interface-Implementation--------------------------------------------//

        public List<DepartmentTreeViewModel> GetAll()
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            return Context.DepartmentTrees.Where(x => x.IsDeleted == false && x.SchoolID==_dbUser.SchoolID ).Select(c => new DepartmentTreeViewModel
            {
                DepartmentTreeID = c.DepartmentTreeID,
                DepartmentNameAR = c.DepartmentNameAR,
                DepartmentNameEN = c.DepartmentNameEN,
                DepartmentTreeCode = c.DepartmentTreeCode,
                DepartmentTreeLevel = c.DepartmentTreeLevel,
                DepartmentTreeParentID = c.DepartmentTreeParentID,
                DepartmentTreeParentNameEN = c.DepartmentTree2.DepartmentNameEN,
                DepartmentTreeParentNameAr = c.DepartmentTree2.DepartmentNameAR,
                DepartmentTreeParent = c.DepartmentTree2,
                Notes = c.Notes

            }).ToList();

        }

        public List<DepartmentTreeViewModel> Getlevel3_4_5()
        {
            List<DepartmentTreeViewModel> Department = new List<DepartmentTreeViewModel>();
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var list3_4_5 = Context.DepartmentTrees.Where(x => x.IsDeleted == false &&x.SchoolID==_dbUser.SchoolID&& x.DepartmentTreeLevel == 3 || x.DepartmentTreeLevel == 4 || x.DepartmentTreeLevel == 5).Select(x => new DepartmentTreeViewModel
            {
                DepartmentTreeID = x.DepartmentTreeID,
                DepartmentNameAR = x.DepartmentNameAR,
                DepartmentNameEN = x.DepartmentNameEN,
                DepartmentTreeCode = x.DepartmentTreeCode,
                DepartmentTreeParentID = x.DepartmentTreeParentID,
                DepartmentTreeLevel = x.DepartmentTreeLevel
            });

            var level3 = list3_4_5.Where(x => x.DepartmentTreeLevel == 3);

            foreach (var item in level3)
            {
                var level4 = list3_4_5.Where(x => x.DepartmentTreeParentID == item.DepartmentTreeID).ToList();

                if (level4.Count() > 0)
                {
                    Department.Add(item);
                    foreach (var item1 in level4)
                    {
                        var level5 = list3_4_5.Where(x => x.DepartmentTreeParentID == item1.DepartmentTreeID).ToList();


                        if (level5.Count() > 0)
                        {
                            Department.Add(item1);

                            foreach (var item2 in level5)
                            {
                                Department.Add(item2);
                            }
                        }
                        else
                        {
                            Department.Add(item1);
                        }
                    }
                }

                else
                {
                    Department.Add(item);
                }

            }
            return Department;
        }

        public DepartmentTreeViewModel GetById(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            return Context.DepartmentTrees.Where(c => c.DepartmentTreeID == ID && c.IsDeleted == false && c.SchoolID==_dbUser.SchoolID).Select(c => new DepartmentTreeViewModel
            {
                DepartmentTreeID = c.DepartmentTreeID,
                DepartmentNameAR = c.DepartmentNameAR,
                DepartmentNameEN = c.DepartmentNameEN,
                DepartmentTreeCode = c.DepartmentTreeCode,
                DepartmentTreeLevel = c.DepartmentTreeLevel,
                DepartmentTreeParentID = c.DepartmentTreeParentID,
                //DepartmentTreeParentNameEN = c.DepartmentTree2 == null ? "" : c.DepartmentTree2.DepartmentNameEN,
                //DepartmentTreeParentNameAr = c.DepartmentTree2 == null ? "" : c.DepartmentTree2.DepartmentNameAR,
                DepartmentTreeParentNameEN = c.DepartmentTree2.DepartmentNameEN,
                DepartmentTreeParentNameAr = c.DepartmentTree2.DepartmentNameAR,
                Notes = c.Notes,
                DepartmentTreeParent = c.DepartmentTree2

            }).FirstOrDefault();
        }

        public DepartmentTreeViewModel Create()
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            DepartmentTreeViewModel model = new DepartmentTreeViewModel();
            var lastID = Context.DepartmentTrees.Where(x=>x.SchoolID==_dbUser.SchoolID).OrderByDescending(c => c.DepartmentTreeID).Select(c => c.DepartmentTreeID).FirstOrDefault();
            model.DepartmentTreeID = lastID;
            model.check = true;
            return model;
        }

        public void Create(DepartmentTreeViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            model.DepartmentTreeParentID = model.DepartmentTreeParentID == null ? 0 : model.DepartmentTreeParentID;


            if (model.DepartmentTreeParentID == 0)
            {

                model.DepartmentTreeParentID = null;
                model.DepartmentTreeLevel = 1;
               
                model.DepartmentTreeCode = GetCodeForLevel1(model);

            }
            else
            {
                DepartmentTreeViewModel _parent = GetById(int.Parse(model.DepartmentTreeParentID.ToString()));
                model.DepartmentTreeLevel = _parent.DepartmentTreeLevel + 1;
                //generate code 
                model.DepartmentTreeCode = GetCode(_parent);
            }



            DepartmentTree _DepartmentTree = new DepartmentTree()
            {
                DepartmentNameAR = model.DepartmentNameAR,
                DepartmentNameEN = model.DepartmentNameEN,
                DepartmentTreeCode = model.DepartmentTreeCode,
                DepartmentTreeLevel = model.DepartmentTreeLevel,
                DepartmentTreeParentID = model.DepartmentTreeParentID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                CompanyID= _dbUser.CompanyID,
                Notes = model.Notes
            };
            Context.DepartmentTrees.Add(_DepartmentTree);
            Context.SaveChanges();
        }

        public string GetCodeForLevel1(DepartmentTreeViewModel _tree)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var count = Context.DepartmentTrees.Where(x => x.DepartmentTreeParentID == null &&x.SchoolID==_dbUser.SchoolID).Count();



            return _tree.DepartmentTreeCode + (count + 1).ToString();
        }

        public string GetCode(DepartmentTreeViewModel _tree)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var count = Context.DepartmentTrees.Where(x => x.DepartmentTreeParentID == _tree.DepartmentTreeID&&x.SchoolID ==_dbUser.SchoolID).Count();

            return _tree.DepartmentTreeCode + (count + 1).ToString();
        }


        public bool Delete(int ID)
        {
           
            var _DepartmentTree = Context.DepartmentTrees.Where(c => c.DepartmentTreeID == ID).FirstOrDefault();
            if (_DepartmentTree != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _DepartmentTree.DeletedbyID = _dbUser.ID;
                _DepartmentTree.IsDeleted = true;
                _DepartmentTree.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public DepartmentTreeViewModel Update(DepartmentTreeViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            //DepartmentTreeViewModel _parent = GetById(int.Parse(model.DepartmentTreeParentID.ToString()));
            //model.DepartmentTreeLevel = _parent.DepartmentTreeLevel + 1;

            //generate code 
            //model.DepartmentTreeCode = GetCode(_parent);


            var _DepartmentTree = Context.DepartmentTrees.Where(c => c.DepartmentTreeID == model.DepartmentTreeID).FirstOrDefault();
            _DepartmentTree.DepartmentNameAR = model.DepartmentNameAR;
            _DepartmentTree.DepartmentNameEN = model.DepartmentNameEN;
            _DepartmentTree.Notes = model.Notes;
            //_DepartmentTree.DepartmentTreeCode = model.DepartmentTreeCode;
            //_DepartmentTree.DepartmentTreeParentID = model.DepartmentTreeParentID;
            //_DepartmentTree.DepartmentTreeLevel = model.DepartmentTreeLevel;
            _DepartmentTree.ModifiedDate = DateTime.Now;
            _DepartmentTree.ModifiedbyID = _dbUser.ID;

            Context.SaveChanges();
            return model;
        }

        //--------------------------------- End-Interface-Implementation--------------------------------------//


        //----------------------------------------start-menu--------------------------------------------------//

        public string GetChildDepartmentTreeMenu(bool side)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            string List1 = "";
            string html = "";

            List<int> _List = Context.DepartmentTrees.Where(c => c.DepartmentTreeParentID == null && c.IsDeleted == false &&c.SchoolID==_dbUser.SchoolID).Select(x => x.DepartmentTreeID).ToList();

            foreach (var item in _List)
            {
                html += GetChildDepartmentTreeSubMenu(item, List1);
            }


            string _menu = "<div id = 'tree_1' class='tree-demo'><ul><li data-jstree='{" + '"' + "opened" + '"' + " : true }' style='color:black'><a onclick='GetRoot()' >....</a><ul>" + html;//+ List2 + List3 ;
            _menu = _menu + "</ul></li></ul></div>";
            return _menu;
        }

        public string GetChildDepartmentTreeSubMenu(int _parentID, string HTMLString)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
            {
                List<DepartmentTree> _List = GetChildDepartmentTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.DepartmentTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.DepartmentTreeID + ")' >" +
                         parent.DepartmentNameAR + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + " yellow folder icon" + '"' + " }'><a onclick='GetChild(" + parent.DepartmentTreeID + ")' >" +
                     parent.DepartmentNameAR + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.DepartmentTreeID != _parentID))
                    {

                        HTMLString += GetChildDepartmentTreeSubMenu(item.DepartmentTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }

            }
            else
            {
                List<DepartmentTree> _List = GetChildDepartmentTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.DepartmentTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.DepartmentTreeID + ")' >" +
                         parent.DepartmentNameEN + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "  folder icon" + '"' + " }'><a onclick='GetChild(" + parent.DepartmentTreeID + ")' >" +
                     parent.DepartmentNameEN + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.DepartmentTreeID != _parentID))
                    {

                        HTMLString += GetChildDepartmentTreeSubMenu(item.DepartmentTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
            }

            return HTMLString;
        }


        public List<DepartmentTree> GetChildDepartmentTree(int _parentID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            List<DepartmentTree> _List = new List<DepartmentTree>();
            var parent = Context.DepartmentTrees.Where(x => x.DepartmentTreeID == _parentID && x.IsDeleted == false &&x.SchoolID==_dbUser.SchoolID).ToList().FirstOrDefault();


            _List.Add(parent);


            var child = parent.DepartmentTree1.Where(x => x.IsDeleted == false).ToList();
            foreach (var item in child)
            {
                _List.Add(item);

            }
            return _List;
        }

        //---------------------------------------End-menu------------------------------------------------------//




        //----------------------------------used in  DepartmentDailyJournalDetailHandler-------------------------//

        public List<int> GetChildDepartmentTreeID(int _parentID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            List<int> _List = new List<int>();

            var child = Context.DepartmentTrees.Where(x => x.DepartmentTreeParentID == _parentID && x.IsDeleted == false &&x.SchoolID==_dbUser.SchoolID).Select(x => x.DepartmentTreeID).ToList();
            foreach (var item in child)
            {
                _List.Add(item);
                GetChildDepartmentTreeID(item);
            }
            return _List;
        }







        //--------------------------------------unused-code--------------------------------------------------//

        //public string GetChildDepartmentTreeSubSideMenu(int _parentID, string HTMLString)
        //{

        //    List<DepartmentTree> _List = GetChildDepartmentTree(_parentID);
        //    var parent = _List == null ? null : _List.Where(x => x.DepartmentTreeID == _parentID).FirstOrDefault();
        //    if (parent != null)
        //    {
        //        if (_List.Count > 1)
        //        {
        //            HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-folder-open'></i><span class='title'>" +
        //             parent.DepartmentNameAR + "</span><span class='arrow '></span></a>";
        //        }
        //        else
        //        {
        //            if (parent.PageUrl != null)
        //            {
        //                HTMLString = "<li ><a href = '" + parent.PageUrl + "' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //            parent.DepartmentNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //            else
        //            {
        //                HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //             parent.DepartmentNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //        }
        //    }
        //    if (_List.Count > 1)
        //    {
        //        HTMLString += "<ul class='sub-menu'>";


        //        foreach (var item in _List.Where(x => x.DepartmentTreeID != _parentID))
        //        {

        //            HTMLString += GetChildDepartmentTreeSubMenu(item.DepartmentTreeID, HTMLString);
        //        }
        //        HTMLString += "</ul>";
        //    }
        //    return HTMLString;
        //}





    }
}