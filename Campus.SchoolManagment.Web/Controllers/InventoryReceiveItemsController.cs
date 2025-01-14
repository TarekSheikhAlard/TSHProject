using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class InventoryReceiveItemsController : BaseController
    {
        ItemsHandler ItemsHandler = new ItemsHandler();
        InventoryReceiveItemsHandler ReceiveItems = new InventoryReceiveItemsHandler();
        // GET: InventoryReceiveItems
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(InventoryReceiveItemsViewModel model)
        {

            ReceiveItems.Create(model);

            return PartialView("Index");
        }
        public object GetItemDetails(string barcode) {
            var model = ItemsHandler.GetByBarcode(barcode);

            dynamic obj = new {
                ItemId =model.ItemId,
                ItemDesc = string.Format("{0} - {1}/{2}", model.ItemNumber, model.nameEn, model.material),
                UnitPrice = model.unitPrice
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj); 
        }



    }
}