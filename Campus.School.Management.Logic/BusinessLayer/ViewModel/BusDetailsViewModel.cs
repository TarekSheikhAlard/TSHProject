using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class BusDetailsViewModel
    {
        public int BusDetailsID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("LicenseExpiryDate")]
        public Nullable<System.DateTime> LicenseExpiryDate { get; set; }

        public int BusID { get; set; }

        public bool IsSuperVisor { get; set; }

        [Required]
        public int Employee_ID { get; set; }

        public SelectList Employee { get; set; }

    }
}
