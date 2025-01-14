using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class AssetDepreciationViewModel
    {

        [Required]
        [Display(Name = "Depreciation Date")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "AssetTree")]
        public int? AssetTreeID { get; set; }
        
        public string AssetNameEN { get; set; }


        public string SerialNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public int dailyjournalID { get; set; }
        public bool IsPost { get; set; }




    }
}
