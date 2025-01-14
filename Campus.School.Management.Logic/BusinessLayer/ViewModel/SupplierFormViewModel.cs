using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class SupplierFormViewModel
    {

        public long ID { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string Status { get; set; }
        public string Type { get; set; }
        public System.DateTime Validity { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public int SchoolID { get; set; }
    
        public bool IsClosed { get; set; }
        public List<FormDetails> ItemsList { get; set; }
    }

    public class FormDetails
    {
        public long ID { get; set; }
        public long FormId { get; set; }
        public long ItemId { get; set; }
        public string itemName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public Nullable<int> ReceiveQty { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> OpenBalance { get; set; }
        public bool IsClosed { get; set; }

    }
}
