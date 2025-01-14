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
    public class AccountTreeHandler : IRepository<AccountTreeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();


        //-------------------------------Interface-Implementation--------------------------------------------//

        public List<AccountTreeViewModel> GetAll()
        {
       
            return Context.AccountTrees.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(c => new AccountTreeViewModel
            {
                AccountTreeID = c.AccountTreeID,
                AccountNameAR = c.AccountNameAR,
                AccountNameEN = c.AccountNameEN,
                AccountTreeCode = c.AccountTreeCode,
                AccountTreeLevel = c.AccountTreeLevel,
                AccountTreeParentID = c.AccountTreeParentID,
                AccountTreeParentNameEN =  c.AccountTree2.AccountNameEN,
                AccountTreeParentNameAr =  c.AccountTree2.AccountNameAR,
                AccountTreeParent = c.AccountTree2,
                Description =c.Description,
                OpenBalance=c.OpenBalance,
                 CreditDebitFlag = c.CreditDebitFlag



            }).ToList();

        }

        public List<AccountTreeViewModel> Getlevel3_4_5()
        {
           
            List<AccountTreeViewModel> account = new List<AccountTreeViewModel>();


            var list3_4_5 = Context.AccountTrees.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID && x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4 || x.AccountTreeLevel == 5).Select(x => new AccountTreeViewModel
            {
                AccountTreeID = x.AccountTreeID,
                AccountNameAR = x.AccountNameAR,
                AccountNameEN = x.AccountNameEN,
                AccountTreeCode = x.AccountTreeCode,
                AccountTreeParentID = x.AccountTreeParentID,
                AccountTreeLevel=x.AccountTreeLevel
            });

            var level3 = list3_4_5.Where(x => x.AccountTreeLevel == 3);

            foreach (var item in level3)
            {
                var level4 = list3_4_5.Where(x => x.AccountTreeParentID == item.AccountTreeID).ToList();

                if (level4.Count() > 0)
                {
                    account.Add(item);
                    foreach (var item1 in level4)
                    {
                        var level5 = list3_4_5.Where(x => x.AccountTreeParentID == item1.AccountTreeID).ToList();


                        if (level5.Count() > 0)
                        {
                            account.Add(item1);

                            foreach (var item2 in level5)
                            {
                                account.Add(item2);
                            }
                        }
                        else
                        {
                            account.Add(item1);
                        }
                    }
                }

                else
                {
                    account.Add(item);
                }

            }
            return account;
        } 
  

        public AccountTreeViewModel GetById(int ID)
        {
         
            return Context.AccountTrees.Where(c => c.AccountTreeID == ID && c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID).Select(c => new AccountTreeViewModel
            {
                AccountTreeID = c.AccountTreeID,
                AccountNameAR = c.AccountNameAR,
                AccountNameEN = c.AccountNameEN,
                AccountTreeCode = c.AccountTreeCode,
                AccountTreeLevel = c.AccountTreeLevel,
                AccountTreeParentID = c.AccountTreeParentID,
                //AccountTreeParentNameEN = c.AccountTree2 == null ? "" : c.AccountTree2.AccountNameEN,
                //AccountTreeParentNameAr = c.AccountTree2 == null ? "" : c.AccountTree2.AccountNameAR,
                AccountTreeParentNameEN =  c.AccountTree2.AccountNameEN,
                AccountTreeParentNameAr =  c.AccountTree2.AccountNameAR,
                Description = c.Description,
                OpenBalance = c.OpenBalance,
                AccountTreeParent = c.AccountTree2,
                CreditDebitFlag = c.CreditDebitFlag
               

            }).FirstOrDefault();
        }

        public AccountTreeViewModel Create()
        {
           
            AccountTreeViewModel model = new AccountTreeViewModel();
            var lastID = Context.AccountTrees.Where(x=>x.SchoolID == _dbUser.SchoolID).OrderByDescending(c => c.AccountTreeID).Select(c => c.AccountTreeID).FirstOrDefault();
            model.check = true;
            model.check1 = true;
            return model;
        }

        public void Create(AccountTreeViewModel model)
        {
           
            //generate level
            AccountTreeViewModel _parent = GetById(int.Parse(model.AccountTreeParentID.ToString()));

            model.AccountTreeLevel = _parent.AccountTreeLevel + 1;

            //generate code 
            model.AccountTreeCode = GetCode(_parent);

            AccountTree _AccountTree = new AccountTree()
            {            
                AccountNameAR = model.AccountNameAR,
                AccountNameEN = model.AccountNameEN,
                AccountTreeCode = model.AccountTreeCode,
                AccountTreeLevel = model.AccountTreeLevel,
                AccountTreeParentID = model.AccountTreeParentID,
                Description = model.Description,
                OpenBalance = model.OpenBalance,
                CreditDebitFlag = model.CreditDebitFlag,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID
                
            };
            Context.AccountTrees.Add(_AccountTree);
            Context.AccountTrees.Add(_AccountTree);
            Context.SaveChanges();

           // return model;
        }
        public void Create(AccountTreeViewModel model,out int AccountTreeID)
        {

            //generate level
            AccountTreeViewModel _parent = GetById(int.Parse(model.AccountTreeParentID.ToString()));

            model.AccountTreeLevel = _parent.AccountTreeLevel + 1;

            //generate code 
            model.AccountTreeCode = GetCode(_parent);

            AccountTree _AccountTree = new AccountTree()
            {
                AccountNameAR = model.AccountNameAR,
                AccountNameEN = model.AccountNameEN,
                AccountTreeCode = model.AccountTreeCode,
                AccountTreeLevel = model.AccountTreeLevel,
                AccountTreeParentID = model.AccountTreeParentID,
                Description = model.Description,
                OpenBalance = model.OpenBalance,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreditDebitFlag = model.CreditDebitFlag,
                SchoolID = _dbUser.SchoolID

            };
            Context.AccountTrees.Add(_AccountTree);
            Context.SaveChanges();
            AccountTreeID = _AccountTree.AccountTreeID;



        }


        public string GetCode(AccountTreeViewModel _tree)
        {
           
            var count = Context.AccountTrees.Where(x => x.AccountTreeParentID == _tree.AccountTreeID && x.SchoolID == _dbUser.SchoolID).Count();



            return _tree.AccountTreeCode + (count + 1).ToString();
        }


        public bool Delete(int ID)
        {
            
            var _AccountTree = Context.AccountTrees.Where(c => c.AccountTreeID == ID && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountTree != null)
            {
                if (Context.AccountDailyJournalDetails.Where(c => c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID && c.AccountTreeID == ID).Count()==0)
                {

                    _AccountTree.DeletedbyID = _dbUser.ID;
                    _AccountTree.IsDeleted = true;
                    _AccountTree.DeletedDate = DateTime.Now;
                    Context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public AccountTreeViewModel Update(AccountTreeViewModel model)
        {
           
            //generate level
            //AccountTreeViewModel _parent = GetById(int.Parse(model.AccountTreeParentID.ToString()));
            //model.AccountTreeLevel = _parent.AccountTreeLevel + 1;

            ////generate code 
            //model.AccountTreeCode = GetCode(_parent);


            var _AccountTree = Context.AccountTrees.Where(c => c.AccountTreeID == model.AccountTreeID).FirstOrDefault();
            //_AccountTree.AccountNameAR = model.AccountNameAR;
            //_AccountTree.AccountNameEN = model.AccountNameEN;
            //_AccountTree.AccountTreeCode = model.AccountTreeCode;
           // _AccountTree.AccountTreeParentID = model.AccountTreeParentID;
           // _AccountTree.AccountTreeLevel = model.AccountTreeLevel;
            _AccountTree.OpenBalance = model.OpenBalance;
            _AccountTree.Description = model.Description;
            _AccountTree.ModifiedDate = DateTime.Now;
            _AccountTree.CreditDebitFlag = model.CreditDebitFlag;
            _AccountTree.ModifiedbyID = _dbUser.ID;

            Context.SaveChanges();
            return model;
        }

        public List<quickAccountTreeListModel> quickAccountTreeListFull()
        {
            List<quickAccountTreeListModel> list;

            list = Context.Database.SqlQuery<quickAccountTreeListModel>("select * from AccountTreeDropdownListFull(@p0,1)", _dbUser.SchoolID).ToList();


            return list;
        }


        public class quickAccountTreeListModel
        {

            public int value { get; set; }
            public string name { get; set; }
            public string text { get; set; }
        }





        //--------------------------------- End-Interface-Implementation--------------------------------------//


        //----------------------------------------start-menu--------------------------------------------------//

        public string GetChildAccountTreeMenu(bool side)
        {
        
            string List1 = "";
            string List2 = "";
            string List3 = "";
            string List4 = "";
            string List5 = "";
            if (side)
            {
                //List1 = GetChildAccountTreeSubSideMenu(1, List1);
                //List2 = GetChildAccountTreeSubSideMenu(2, List2);
                //List3 = GetChildAccountTreeSubSideMenu(3, List3);
                //List4 = GetChildAccountTreeSubSideMenu(4, List4);
                //List5 = GetChildAccountTreeSubSideMenu(5, List5);
            }
            else
            {
                List1 = GetChildAccountTreeSubMenu(1, List1);
                List2 = GetChildAccountTreeSubMenu(2, List2);
                List3 = GetChildAccountTreeSubMenu(3, List3);
                List4 = GetChildAccountTreeSubMenu(4, List4);
                List5 = GetChildAccountTreeSubMenu(5, List5);
            }

            string _menu = "<div id = 'tree_1' class='tree-demo'><ul  >" + List1 + List2 + List3 + List4 + List5;
            _menu = _menu + "</ul></div>";
            return _menu;
        }

        public string GetChildAccountTreeSubMenu(int _parentID, string HTMLString)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
            {
                List<AccountTree> _List = GetChildAccountTree(_parentID);

                var parent = _List == null ? null : _List.Where(x => x.AccountTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.AccountTreeID + ")' >" +
                         parent.AccountNameAR + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder yellow icon" + '"' + " }'><a onclick='GetChild(" + parent.AccountTreeID + ")' >" +
                     parent.AccountNameAR + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul>";


                    foreach (var item in _List.Where(x => x.AccountTreeID != _parentID))
                    {

                        HTMLString += GetChildAccountTreeSubMenu(item.AccountTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
              
            }
            else
            {
                List<AccountTree> _List = GetChildAccountTree(_parentID);
                var parent = _List == null ? null : _List.Where(x => x.AccountTreeID == _parentID).FirstOrDefault();
                if (parent != null)
                {
                    if (_List.Count > 1)
                    {
                        HTMLString = "<li data-jstree='{" + '"' + "opened" + '"' + " : false }' style='color:black'><a onclick='GetChild(" + parent.AccountTreeID + ")' >" +
                         parent.AccountNameEN + "</a>";
                    }
                    else
                    {

                        HTMLString = " <li data-jstree='{" + '"' + "icon" + '"' + " : " + '"' + "folder  icon" + '"' + " }'><a onclick='GetChild(" + parent.AccountTreeID + ")' >" +
                     parent.AccountNameEN + "</a></li>";

                    }
                }
                if (_List.Count > 1)
                {
                    HTMLString += "<ul >";


                    foreach (var item in _List.Where(x => x.AccountTreeID != _parentID))
                    {

                        HTMLString += GetChildAccountTreeSubMenu(item.AccountTreeID, HTMLString);
                    }
                    HTMLString += "</ul></li>";
                }
            }

            return HTMLString;
        }


        public List<AccountTree> GetChildAccountTree(int _parentID)
        {
         
            List<AccountTree> _List = new List<AccountTree>();
            var parent = Context.AccountTrees.Where(x => x.AccountTreeID == _parentID &&  x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).ToList().FirstOrDefault();

            _List.Add(parent);


            var child = parent.AccountTree1.Where(x => x.IsDeleted == false).ToList();
            foreach (var item in child)
            {
                _List.Add(item);

            }
            return _List;
        }

        //---------------------------------------End-menu------------------------------------------------------//




        //----------------------------------used in  AccountDailyJournalDetailHandler-------------------------//

        public List<int> GetChildAccountTreeID(int _parentID)
        {
            
            List<int> _List = new List<int>();

            var child = Context.AccountTrees.Where(x => x.AccountTreeParentID == _parentID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x=> x.AccountTreeID ).ToList();
            foreach (var item in child)
            {
                _List.Add(item);
                GetChildAccountTreeID(item);
            }
            return _List;
        }

     



      

        //--------------------------------------unused-code--------------------------------------------------//

        //public string GetChildAccountTreeSubSideMenu(int _parentID, string HTMLString)
        //{

        //    List<AccountTree> _List = GetChildAccountTree(_parentID);
        //    var parent = _List == null ? null : _List.Where(x => x.AccountTreeID == _parentID).FirstOrDefault();
        //    if (parent != null)
        //    {
        //        if (_List.Count > 1)
        //        {
        //            HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-folder-open'></i><span class='title'>" +
        //             parent.AccountNameAR + "</span><span class='arrow '></span></a>";
        //        }
        //        else
        //        {
        //            if (parent.PageUrl != null)
        //            {
        //                HTMLString = "<li ><a href = '" + parent.PageUrl + "' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //            parent.AccountNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //            else
        //            {
        //                HTMLString = "<li ><a href = 'javascript:;' ><i class='glyphicon glyphicon-file'></i><span class='title'>" +
        //             parent.AccountNameAR + "</span><span class='arrow '></span></a>";
        //            }
        //        }
        //    }
        //    if (_List.Count > 1)
        //    {
        //        HTMLString += "<ul class='sub-menu'>";


        //        foreach (var item in _List.Where(x => x.AccountTreeID != _parentID))
        //        {

        //            HTMLString += GetChildAccountTreeSubMenu(item.AccountTreeID, HTMLString);
        //        }
        //        HTMLString += "</ul>";
        //    }
        //    return HTMLString;
        //}




        
    }
}
