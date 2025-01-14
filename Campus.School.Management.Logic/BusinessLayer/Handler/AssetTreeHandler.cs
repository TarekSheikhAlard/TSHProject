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
    public class AssetTreeHandler : IRepository<AssetTreeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        //-------------------------------Interface-Implementation--------------------------------------------//

        public List<AssetTreeViewModel> GetAll()
        {
            return Context.AssetTrees.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(c => new AssetTreeViewModel
            {
                AssetTreeID = c.AssetTreeID,
                AssetNameAR = c.AssetNameAR,
                AssetNameEN = c.AssetNameEN,
                AssetTreeCode = c.AssetTreeCode,
                AssetTreeLevel = c.AssetTreeLevel,
                AssetTreeParentID = c.AssetTreeParentID,
                AssetTreeParentNameEN =  c.AssetTree2.AssetNameEN,
                AssetTreeParentNameAr =  c.AssetTree2.AssetNameAR,
                AssetTreeParent = c.AssetTree2,
                Notes =c.Notes

            }).ToList();

        }

        public List<AssetTreeViewModel> Getlevel3_4_5()
        {
            List<AssetTreeViewModel> Asset = new List<AssetTreeViewModel>();


            var list3_4_5 = Context.AssetTrees.Where(x => x.IsDeleted == false && x.AssetTreeLevel == 3 || x.AssetTreeLevel == 4 || x.AssetTreeLevel == 5 && x.SchoolID == _dbUser.SchoolID).Select(x => new AssetTreeViewModel
            {
                AssetTreeID = x.AssetTreeID,
                AssetNameAR = x.AssetNameAR,
                AssetNameEN = x.AssetNameEN,
                AssetTreeCode = x.AssetTreeCode,
                AssetTreeParentID = x.AssetTreeParentID,
                AssetTreeLevel=x.AssetTreeLevel
            });

            var level3 = list3_4_5.Where(x => x.AssetTreeLevel == 3);

            foreach (var item in level3)
            {
                var level4 = list3_4_5.Where(x => x.AssetTreeParentID == item.AssetTreeID).ToList();

                if (level4.Count() > 0)
                {
                    Asset.Add(item);
                    foreach (var item1 in level4)
                    {
                        var level5 = list3_4_5.Where(x => x.AssetTreeParentID == item1.AssetTreeID).ToList();


                        if (level5.Count() > 0)
                        {
                            Asset.Add(item1);

                            foreach (var item2 in level5)
                            {
                                Asset.Add(item2);
                            }
                        }
                        else
                        {
                            Asset.Add(item1);
                        }
                    }
                }

                else
                {
                    Asset.Add(item);
                }

            }
            return Asset;
        } 
  
        public AssetTreeViewModel GetById(int ID)
        {
            return Context.AssetTrees.Where(c => c.AssetTreeID == ID && c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID).Select(c => new AssetTreeViewModel
            {
                AssetTreeID = c.AssetTreeID,
                AssetNameAR = c.AssetNameAR,
                AssetNameEN = c.AssetNameEN,
                AssetTreeCode = c.AssetTreeCode,
                AssetTreeLevel = c.AssetTreeLevel,
                AssetTreeParentID = c.AssetTreeParentID,
                //AssetTreeParentNameEN = c.AssetTree2 == null ? "" : c.AssetTree2.AssetNameEN,
                //AssetTreeParentNameAr = c.AssetTree2 == null ? "" : c.AssetTree2.AssetNameAR,
                AssetTreeParentNameEN =  c.AssetTree2.AssetNameEN,
                AssetTreeParentNameAr =  c.AssetTree2.AssetNameAR,
                Notes = c.Notes,
                AssetTreeParent = c.AssetTree2

            }).FirstOrDefault();
        }

        public AssetTreeViewModel Create()
        {
            AssetTreeViewModel model = new AssetTreeViewModel();
            var lastID = Context.AssetTrees.Where(x=> x.SchoolID == _dbUser.SchoolID).OrderByDescending(c => c.AssetTreeID).Select(c => c.AssetTreeID).FirstOrDefault();
            model.AssetTreeID = lastID;
            model.check = true;
            return model;
        }

        public void Create(AssetTreeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            model.AssetTreeParentID = model.AssetTreeParentID == null ? 0 : model.AssetTreeParentID;
          

            if (model.AssetTreeParentID==0) {

                model.AssetTreeParentID = null;
                model.AssetTreeLevel = 1;

                model.AssetTreeCode = GetCodeForLevel1(model);

            } else {
                AssetTreeViewModel  _parent  = GetById(int.Parse(model.AssetTreeParentID.ToString()));
                model.AssetTreeLevel = _parent.AssetTreeLevel + 1;
                //generate code 
                model.AssetTreeCode = GetCode(_parent);
            }

         

            AssetTree _AssetTree = new AssetTree()
            {
                AssetNameAR = model.AssetNameAR,
                AssetNameEN = model.AssetNameEN,
                AssetTreeCode = model.AssetTreeCode,
                AssetTreeLevel = model.AssetTreeLevel,
                AssetTreeParentID = model.AssetTreeParentID,
                CreatedbyID =_dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                Notes = model.Notes
            };
            Context.AssetTrees.Add(_AssetTree);
            Context.SaveChanges();
        }

        public string GetCodeForLevel1(AssetTreeViewModel _tree)
        {

            var count = Context.AssetTrees.Where(x => x.AssetTreeParentID == null && x.SchoolID == _dbUser.SchoolID).Count();



            return _tree.AssetTreeCode + (count + 1).ToString();
        }

        public string GetCode(AssetTreeViewModel _tree)
        {

            var count = Context.AssetTrees.Where(x => x.AssetTreeParentID == _tree.AssetTreeID && x.SchoolID == _dbUser.SchoolID).Count();



            return _tree.AssetTreeCode + (count + 1).ToString();
        }


        public bool Delete(int ID)
        {
            var _AssetTree = Context.AssetTrees.Where(c => c.AssetTreeID == ID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AssetTree != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AssetTree.DeletedbyID = _dbUser.ID;
                _AssetTree.IsDeleted = true;
                _AssetTree.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AssetTreeViewModel Update(AssetTreeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //generate level
            //AssetTreeViewModel _parent = GetById(int.Parse(model.AssetTreeParentID.ToString()));
            //model.AssetTreeLevel = _parent.AssetTreeLevel + 1;

            //generate code 
            //model.AssetTreeCode = GetCode(_parent);


            var _AssetTree = Context.AssetTrees.Where(c => c.AssetTreeID == model.AssetTreeID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            _AssetTree.AssetNameAR = model.AssetNameAR;
            _AssetTree.AssetNameEN = model.AssetNameEN;
            _AssetTree.Notes = model.Notes;
            //_AssetTree.AssetTreeCode = model.AssetTreeCode;
            //_AssetTree.AssetTreeParentID = model.AssetTreeParentID;
            //_AssetTree.AssetTreeLevel = model.AssetTreeLevel;
            _AssetTree.ModifiedDate = DateTime.Now;
            _AssetTree.ModifiedbyID = _dbUser.ID;

            Context.SaveChanges();
            return model;
        }

       //--------------------------------- End-Interface-Implementation--------------------------------------//


       //----------------------------------------start-menu--------------------------------------------------//

        public string GetChildAssetTreeMenu(bool side)
        {
            string List1 = "";
            string html = "";

            List<int> _List = Context.AssetTrees.Where(c => c.AssetTreeParentID == null && c.IsDeleted==false && c.SchoolID == _dbUser.SchoolID).Select(x => x.AssetTreeID).ToList();

            foreach (var item in _List)
            {
                html += GetChildAssetTreeSubMenu(item, List1);
            }
          

            string _menu = "<div id = 'tree_1' class='tree-demo'><ul><li data-jstree='{" + '"' + "opened" + '"' + " : true }' style='color:black'><a onclick='GetRoot()' >....</a><ul>" + html;//+ List2 + List3 ;
            _menu = _menu + "</ul></li></ul></div>";
            return _menu;
        }

        public string GetChildAssetTreeSubMenu(int _parentID, string HTMLString)
        {

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
            {
                List<AssetTree> _List = GetChildAssetTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.AssetTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.AssetTreeID + ")' >" +
                         parent.AssetNameAR + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder yellow icon" + '"' + " }'><a onclick='GetChild(" + parent.AssetTreeID + ")' >" +
                     parent.AssetNameAR + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.AssetTreeID != _parentID))
                    {

                        HTMLString += GetChildAssetTreeSubMenu(item.AssetTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
              
            }
            else
            {
                List<AssetTree> _List = GetChildAssetTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.AssetTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.AssetTreeID + ")' >" +
                         parent.AssetNameEN + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder  icon" + '"' + " }'><a onclick='GetChild(" + parent.AssetTreeID + ")' >" +
                     parent.AssetNameEN + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.AssetTreeID != _parentID))
                    {

                        HTMLString += GetChildAssetTreeSubMenu(item.AssetTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
            }

            return HTMLString;
        }


        public List<AssetTree> GetChildAssetTree(int _parentID)
        {
            List<AssetTree> _List = new List<AssetTree>();
            var parent = Context.AssetTrees.Where(x => x.AssetTreeID == _parentID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).ToList().FirstOrDefault();

           
            _List.Add(parent);


            var child = parent.AssetTree1.Where(x => x.IsDeleted == false).ToList();
            foreach (var item in child)
            {
                _List.Add(item);

            }
            return _List;
        }

        //---------------------------------------End-menu------------------------------------------------------//




        //----------------------------------used in  AssetDailyJournalDetailHandler-------------------------//

        public List<int> GetChildAssetTreeID(int _parentID)
        {
            List<int> _List = new List<int>();

            var child = Context.AssetTrees.Where(x => x.AssetTreeParentID == _parentID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x=> x.AssetTreeID ).ToList();
            foreach (var item in child)
            {
                _List.Add(item);
                GetChildAssetTreeID(item);
            }
            return _List;
        }

     



      

        //--------------------------------------unused-code--------------------------------------------------//

        //public string GetChildAssetTreeSubSideMenu(int _parentID, string HTMLString)
        //{

        //    List<AssetTree> _List = GetChildAssetTree(_parentID);
        //    var parent = _List == null ? null : _List.Where(x => x.AssetTreeID == _parentID).FirstOrDefault();
        //    if (parent != null)
        //    {
        //        if (_List.Count > 1)
        //        {
        //            HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-folder-open'></i><span class='title'>" +
        //             parent.AssetNameAR + "</span><span class='arrow '></span></a>";
        //        }
        //        else
        //        {
        //            if (parent.PageUrl != null)
        //            {
        //                HTMLString = "<li ><a href = '" + parent.PageUrl + "' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //            parent.AssetNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //            else
        //            {
        //                HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //             parent.AssetNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //        }
        //    }
        //    if (_List.Count > 1)
        //    {
        //        HTMLString += "<ul class='sub-menu'>";


        //        foreach (var item in _List.Where(x => x.AssetTreeID != _parentID))
        //        {

        //            HTMLString += GetChildAssetTreeSubMenu(item.AssetTreeID, HTMLString);
        //        }
        //        HTMLString += "</ul>";
        //    }
        //    return HTMLString;
        //}




        
    }
}
