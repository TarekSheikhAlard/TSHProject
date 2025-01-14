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
    public class InventoryTreeHandler : IRepository<InventoryTreeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        //-------------------------------Interface-Implementation--------------------------------------------//

        public List<InventoryTreeViewModel> GetAll()
        {
            
            return Context.InventoryTrees.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(c => new InventoryTreeViewModel
            {
                InventoryTreeID = c.InventoryTreeID,
                InventoryNameAR = c.InventoryNameAR,
                InventoryNameEN = c.InventoryNameEN,
                InventoryTreeCode = c.InventoryTreeCode,
                InventoryTreeLevel = c.InventoryTreeLevel,
                InventoryTreeParentID = c.InventoryTreeParentID,
                InventoryTreeParentNameEN = c.InventoryTree2.InventoryNameEN,
                InventoryTreeParentNameAr = c.InventoryTree2.InventoryNameAR,
                InventoryTreeParent = c.InventoryTree2,
                Notes = c.Notes

            }).ToList();

        }

        public List<InventoryTreeViewModel> Getlevel3_4_5()
        {
            List<InventoryTreeViewModel> Inventory = new List<InventoryTreeViewModel>();


            var list3_4_5 = Context.InventoryTrees.Where(x => x.IsDeleted == false && x.InventoryTreeLevel == 3 || x.InventoryTreeLevel == 4 || x.InventoryTreeLevel == 5 && x.SchoolID == _dbUser.SchoolID).Select(x => new InventoryTreeViewModel
            {
                InventoryTreeID = x.InventoryTreeID,
                InventoryNameAR = x.InventoryNameAR,
                InventoryNameEN = x.InventoryNameEN,
                InventoryTreeCode = x.InventoryTreeCode,
                InventoryTreeParentID = x.InventoryTreeParentID,
                InventoryTreeLevel = x.InventoryTreeLevel
            });

            var level3 = list3_4_5.Where(x => x.InventoryTreeLevel == 3);

            foreach (var item in level3)
            {
                var level4 = list3_4_5.Where(x => x.InventoryTreeParentID == item.InventoryTreeID).ToList();

                if (level4.Count() > 0)
                {
                    Inventory.Add(item);
                    foreach (var item1 in level4)
                    {
                        var level5 = list3_4_5.Where(x => x.InventoryTreeParentID == item1.InventoryTreeID).ToList();


                        if (level5.Count() > 0)
                        {
                            Inventory.Add(item1);

                            foreach (var item2 in level5)
                            {
                                Inventory.Add(item2);
                            }
                        }
                        else
                        {
                            Inventory.Add(item1);
                        }
                    }
                }

                else
                {
                    Inventory.Add(item);
                }

            }
            return Inventory;
        }

        public InventoryTreeViewModel GetById(int ID)
        {
            return Context.InventoryTrees.Where(c => c.InventoryTreeID == ID && c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID).Select(c => new InventoryTreeViewModel
            {
                InventoryTreeID = c.InventoryTreeID,
                InventoryNameAR = c.InventoryNameAR,
                InventoryNameEN = c.InventoryNameEN,
                InventoryTreeCode = c.InventoryTreeCode,
                InventoryTreeLevel = c.InventoryTreeLevel,
                InventoryTreeParentID = c.InventoryTreeParentID,
                //InventoryTreeParentNameEN = c.InventoryTree2 == null ? "" : c.InventoryTree2.InventoryNameEN,
                //InventoryTreeParentNameAr = c.InventoryTree2 == null ? "" : c.InventoryTree2.InventoryNameAR,
                InventoryTreeParentNameEN = c.InventoryTree2.InventoryNameEN,
                InventoryTreeParentNameAr = c.InventoryTree2.InventoryNameAR,
                Notes = c.Notes,
                InventoryTreeParent = c.InventoryTree2

            }).FirstOrDefault();
        }

        public InventoryTreeViewModel Create()
        {
            InventoryTreeViewModel model = new InventoryTreeViewModel();
            var lastID = Context.InventoryTrees.Where(x=>x.SchoolID == _dbUser.SchoolID).OrderByDescending(c => c.InventoryTreeID).Select(c => c.InventoryTreeID).FirstOrDefault();
            model.InventoryTreeID = lastID;
            model.check = true;
            return model;
        }

        public void Create(InventoryTreeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            model.InventoryTreeParentID = model.InventoryTreeParentID == null ? 0 : model.InventoryTreeParentID;


            if (model.InventoryTreeParentID == 0)
            {

                model.InventoryTreeParentID = null;
                model.InventoryTreeLevel = 1;

                model.InventoryTreeCode = GetCodeForLevel1(model);

            }
            else
            {
                InventoryTreeViewModel _parent = GetById(int.Parse(model.InventoryTreeParentID.ToString()));
                model.InventoryTreeLevel = _parent.InventoryTreeLevel + 1;
                //generate code 
                model.InventoryTreeCode = GetCode(_parent);
            }



            InventoryTree _InventoryTree = new InventoryTree()
            {
                InventoryNameAR = model.InventoryNameAR,
                InventoryNameEN = model.InventoryNameEN,
                InventoryTreeCode = model.InventoryTreeCode,
                InventoryTreeLevel = model.InventoryTreeLevel,
                InventoryTreeParentID = model.InventoryTreeParentID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                Notes = model.Notes
            };
            Context.InventoryTrees.Add(_InventoryTree);
            Context.SaveChanges();
        }

        public string GetCodeForLevel1(InventoryTreeViewModel _tree)
        {

            var count = Context.InventoryTrees.Where(x => x.InventoryTreeParentID == null && x.SchoolID == _dbUser.SchoolID).Count();



            return _tree.InventoryTreeCode + (count + 1).ToString();
        }

        public string GetCode(InventoryTreeViewModel _tree)
        {

            var count = Context.InventoryTrees.Where(x => x.InventoryTreeParentID == _tree.InventoryTreeID && x.SchoolID == _dbUser.SchoolID).Count();



            return _tree.InventoryTreeCode + (count + 1).ToString();
        }


        public bool Delete(int ID)
        {
            var _InventoryTree = Context.InventoryTrees.Where(c => c.InventoryTreeID == ID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_InventoryTree != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _InventoryTree.DeletedbyID = _dbUser.ID;
                _InventoryTree.IsDeleted = true;
                _InventoryTree.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public InventoryTreeViewModel Update(InventoryTreeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            //InventoryTreeViewModel _parent = GetById(int.Parse(model.InventoryTreeParentID.ToString()));
            //model.InventoryTreeLevel = _parent.InventoryTreeLevel + 1;

            //generate code 
            //model.InventoryTreeCode = GetCode(_parent);


            var _InventoryTree = Context.InventoryTrees.Where(c => c.InventoryTreeID == model.InventoryTreeID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            _InventoryTree.InventoryNameAR = model.InventoryNameAR;
            _InventoryTree.InventoryNameEN = model.InventoryNameEN;
            _InventoryTree.Notes = model.Notes;
            //_InventoryTree.InventoryTreeCode = model.InventoryTreeCode;
            //_InventoryTree.InventoryTreeParentID = model.InventoryTreeParentID;
            //_InventoryTree.InventoryTreeLevel = model.InventoryTreeLevel;
            _InventoryTree.ModifiedDate = DateTime.Now;
            _InventoryTree.ModifiedbyID = _dbUser.ID;

            Context.SaveChanges();
            return model;
        }

        //--------------------------------- End-Interface-Implementation--------------------------------------//


        //----------------------------------------start-menu--------------------------------------------------//

        public string GetChildInventoryTreeMenu(bool side)
        {
            string List1 = "";
            string html = "";

            List<int> _List = Context.InventoryTrees.Where(c => c.InventoryTreeParentID == null && c.SchoolID == _dbUser.SchoolID).Select(x => x.InventoryTreeID).ToList();

            foreach (var item in _List)
            {
                html += GetChildInventoryTreeSubMenu(item, List1);
            }


            string _menu = "<div id = 'tree_1' class='tree-demo'><ul><li data-jstree='{" + '"' + "opened" + '"' + " : true }' style='color:black'><a onclick='GetRoot()' >....</a><ul>" + html;//+ List2 + List3 ;
            _menu = _menu + "</ul></li></ul></div>";
            return _menu;
        }

        public string GetChildInventoryTreeSubMenu(int _parentID, string HTMLString)
        {

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
            {
                List<InventoryTree> _List = GetChildInventoryTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.InventoryTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.InventoryTreeID + ")' >" +
                         parent.InventoryNameAR + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder yellow icon" + '"' + " }'><a onclick='GetChild(" + parent.InventoryTreeID + ")' >" +
                     parent.InventoryNameAR + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.InventoryTreeID != _parentID))
                    {

                        HTMLString += GetChildInventoryTreeSubMenu(item.InventoryTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }

            }
            else
            {
                List<InventoryTree> _List = GetChildInventoryTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.InventoryTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.InventoryTreeID + ")' >" +
                         parent.InventoryNameEN + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder  icon" + '"' + " }'><a onclick='GetChild(" + parent.InventoryTreeID + ")' >" +
                     parent.InventoryNameEN + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.InventoryTreeID != _parentID))
                    {

                        HTMLString += GetChildInventoryTreeSubMenu(item.InventoryTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
            }

            return HTMLString;
        }


        public List<InventoryTree> GetChildInventoryTree(int _parentID)
        {
            List<InventoryTree> _List = new List<InventoryTree>();
            var parent = Context.InventoryTrees.Where(x => x.InventoryTreeID == _parentID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).ToList().FirstOrDefault();


            _List.Add(parent);


            var child = parent.InventoryTree1.Where(x => x.IsDeleted == false).ToList();
            foreach (var item in child)
            {
                _List.Add(item);

            }
            return _List;
        }

        //---------------------------------------End-menu------------------------------------------------------//




        //----------------------------------used in  InventoryDailyJournalDetailHandler-------------------------//

        public List<int> GetChildInventoryTreeID(int _parentID)
        {
            List<int> _List = new List<int>();

            var child = Context.InventoryTrees.Where(x => x.InventoryTreeParentID == _parentID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => x.InventoryTreeID).ToList();
            foreach (var item in child)
            {
                _List.Add(item);
                GetChildInventoryTreeID(item);
            }
            return _List;
        }







        //--------------------------------------unused-code--------------------------------------------------//

        //public string GetChildInventoryTreeSubSideMenu(int _parentID, string HTMLString)
        //{

        //    List<InventoryTree> _List = GetChildInventoryTree(_parentID);
        //    var parent = _List == null ? null : _List.Where(x => x.InventoryTreeID == _parentID).FirstOrDefault();
        //    if (parent != null)
        //    {
        //        if (_List.Count > 1)
        //        {
        //            HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-folder-open'></i><span class='title'>" +
        //             parent.InventoryNameAR + "</span><span class='arrow '></span></a>";
        //        }
        //        else
        //        {
        //            if (parent.PageUrl != null)
        //            {
        //                HTMLString = "<li ><a href = '" + parent.PageUrl + "' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //            parent.InventoryNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //            else
        //            {
        //                HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //             parent.InventoryNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //        }
        //    }
        //    if (_List.Count > 1)
        //    {
        //        HTMLString += "<ul class='sub-menu'>";


        //        foreach (var item in _List.Where(x => x.InventoryTreeID != _parentID))
        //        {

        //            HTMLString += GetChildInventoryTreeSubMenu(item.InventoryTreeID, HTMLString);
        //        }
        //        HTMLString += "</ul>";
        //    }
        //    return HTMLString;
        //}





    }
}
