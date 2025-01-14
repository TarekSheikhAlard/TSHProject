using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Campus.SchoolManagment.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class ExternalSchoolContext:DbContext
    {
        public ExternalSchoolContext(string connectionString)
          : base(connectionString)
        {
        }

    }
}