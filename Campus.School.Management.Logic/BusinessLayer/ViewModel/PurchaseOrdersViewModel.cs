using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class PurchaseOrdersViewModel
    {

        public long ID { get; set; }
        public string PONo { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Tax { get; set; }
        public int Supplier { get; set; }
        public int Currency { get; set; }
        public long POId { get; set; }
        public System.DateTime PODate { get; set; }
        public List<PurchaseOrderItemsViewModel> POItemsList { get; set; }
    }
    public class PurchaseOrderItemsViewModel
    {
        public long ID { get; set; }
        public string ItemBarcodeNo { get; set; }
     public string ItemDesc { get; set; }
        public long POId { get; set; }
        public long ItemId { get; set; }
        public string UnitName { get; set; }
        public int POQty { get; set; }
        public decimal Cost { get; set; }
       public decimal TotalCost { get; set; }
    }
}
