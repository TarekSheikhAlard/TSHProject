using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class StockPurchaseOrderController : Controller
    {
        PurchaseOrderHandler PurchaseOrder = new PurchaseOrderHandler();
      
        // GET: StockPurchaseOrder
        public ActionResult Index()
        {     
            return PartialView(PurchaseOrder.Create());
        }
        public ActionResult GetAllOrder()
        {
            return PartialView("_List",PurchaseOrder.GetAll());
        }
    }
}