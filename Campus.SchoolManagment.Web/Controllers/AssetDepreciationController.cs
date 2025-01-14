using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Campus.School.Management.Logic.BusinessLayer;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class AssetDepreciationController : BaseController
    {

        AccountAssetHandler _AccountAssetHandler = new AccountAssetHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        AssetTreeHandler _AssetTreeHandler = new AssetTreeHandler();
        DepartmentHandler _DepartmentHandler = new DepartmentHandler();

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        // GET: AssetDepreciation
        public ActionResult Index()
        {      
            return PartialView();
        }
        public ActionResult GetAsset()
        {
            ViewBag.MainassetTree = new SelectList(_AssetTreeHandler.GetAll().Where(x => x.AssetTreeLevel == 1).OrderBy(x => x.AssetTreeID), "AssetTreeID", "AssetName" + lang);

            return PartialView("_AssetTree");
        }

        [HttpPost]
        public object SubmitDepreciation(AssetDepreciationViewModel model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter MESG = new System.Data.Entity.Core.Objects.ObjectParameter("MESG", typeof(string));
            Context.Sp_Depreciation("6028", model.ToDate.ToString(), model.AssetTreeID, MESG);
            var obj = new  {
                MESG,
                IsError=false,
               model.AssetTreeID
            };
            return JsonConvert.SerializeObject(obj);
        }

        public ActionResult GetDepreciation(int id)
        {
            string query = @"select journal.DailyJournalID, journal.SerialNo,journal.CreatedDate,asset.Name,journal.Description,Depreciation.Amount,journal.IsPost from 
                            AccAssetDepreciation Depreciation
                            inner join 
                            AccountDailyJournal journal
                            on journal.DailyJournalID=Depreciation.DailyJournalID
                            inner join 
                            AccountAssets asset
                            on asset.ID=Depreciation.AssetID 
                      WHERE asset.AssetTreeID in (SELECT L3.AssetTreeID FROM AssetTree L1, AssetTree L2, AssetTree L3
                      WHERE(L1.AssetTreeID = '" + id.ToString() + "' OR L2.AssetTreeID = '" + id.ToString() + "' OR L3.AssetTreeID = '" + id.ToString() 
                      + @" ') AND L1.AssetTreeID = L2.AssetTreeParentID
                              AND L2.AssetTreeID = L3.AssetTreeParentID )
                              AND journal.IsDeleted !='1'
                              and  Depreciation.IsDeleted !='1'";

            ViewBag.AssetTreeID = id;
           var results= Context.Database.SqlQuery<AssetDepreciationViewModel>(query).ToList();

            return PartialView("_List", results);
        }


        public object PostDepreciation(int id) {

            string query = @"UPDATE journal 
                  SET  journal.IsPost = '1'
                  from [AccountDailyJournal] journal,
                      AccAssetDepreciation depreciation,
	                  AccountAssets Assets
                  where journal.DailyJournalID=depreciation.DailyJournalID
                   and Assets.ID=depreciation.AssetID
                  and Assets.AssetTreeID in ((SELECT L3.AssetTreeID FROM AssetTree L1, AssetTree L2, AssetTree L3
                     WHERE(L1.AssetTreeID =@p0 OR L2.AssetTreeID =@p0 OR L3.AssetTreeID =@p0)
                               AND L1.AssetTreeID = L2.AssetTreeParentID
                              AND L2.AssetTreeID = L3.AssetTreeParentID ))";

            var result =Context.Database.ExecuteSqlCommand(query,id);

            var obj = new
            {
               IsError =false,
               AssetTreeID=id
            };
            return JsonConvert.SerializeObject(obj);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        [HttpPost, ActionName("Delete")]
        public object ConfirmDelete(int id)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            System.Data.Entity.Core.Objects.ObjectParameter MESG = new System.Data.Entity.Core.Objects.ObjectParameter("MESG", typeof(string));

            Context.DeleteDepreciation(_dbUser.ID, id, MESG);

            var obj = new
            {
                MESG,
                IsError = false,
                delId=id
            };
            return JsonConvert.SerializeObject(obj);

        }


    }
}