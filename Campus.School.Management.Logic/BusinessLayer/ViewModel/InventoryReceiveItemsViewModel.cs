using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class InventoryReceiveItemsViewModel
    {
        public int IncomingLot { get; set; }
        public int LotId { get; set; }
        public DateTime LotDate { get; set; }
        public int DocumentNumber { get; set; }
        public int Store { get; set; }
        public string StoreName { get; set; }
        public int? PONumber { get; set; }
        public DateTime PODate { get; set; }
        public decimal Credit { get; set; }
        public decimal Suppiler { get; set; }
        public decimal Currency { get; set; }
        public List<ItemsList> ItemsList { get; set; }
  
    }
    public class ItemsList {
        public int ItemId { get; set; }
        public int ItemBarcode { get; set; }
        public int ReceivedQty { get; set; }
        public int Discount { get; set; }
    }
}

