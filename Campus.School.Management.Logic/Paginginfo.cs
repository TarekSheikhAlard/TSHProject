using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic
{
   public class Paginginfo
    {
        public int Totalitems { get; set; }
        public int itemsperpage { get; set; }
        public int currentpage { get; set; }
        public int totalpage
        {
            get { return (int)(Math.Ceiling(((double)Totalitems / (double)itemsperpage))); }
        }
    }
}
