using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class GenericVModel
    {

        [DisplayName("Name ")]
        public string ItemName { get; set; }


        [DisplayName("Value")]
        public decimal Value { get; set; }


        [DisplayName("Type ")]
        public string Type { get; set; }

        public string AccountTree { get; set; }

        //1=>""
        [DisplayName("TypeID ")]
        public int TypeID { get; set; }

        public List<GenericVModel> _DetailList { get; set; }

    }
}
