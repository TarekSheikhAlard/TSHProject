using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class ItemViewModel
    {
        public long ItemId { get; set; }
        public int ItemInventoryStructureId { get; set; }
        public string ItemInventoryStructureName { get; set; }
        public int ItemNumber { get; set; }
       // public string alternativeNumber { get; set; }
        public string Supplier { get; set; }
        public string PlaceofManufacture { get; set; }
        public int? material { get; set; }
        public string nameEn { get; set; }
        public string nameAr { get; set; }
        public string ItemModel { get; set; }
        public int maximumLimit { get; set; }
        public int minimumLimit { get; set; }
        public int requestLimit { get; set; }
        public bool status { get; set; }
        public decimal currentBalance { get; set; }
        public decimal TotalCost { get; set; }
        public decimal purchasingCost { get; set; }
        public decimal lastPurchasingCost { get; set; }

        public string unitNumber { get; set; }

        public string unitName { get; set; }
        public string barcode { get; set; }

        public decimal exchangeRate { get; set; }
        public decimal unitPrice { get; set; }
        public decimal wholeSalePrice { get; set; }


        public int storeNumber { get; set; }
        public string storeName { get; set; }







    }
}
