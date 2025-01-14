using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
   public class ReceiptHandler
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public bool SaveReceipt(string html)
        {
            string query = "INSERT INTO [AccReceipts] ([Data],[CreatedBy]) VALUES (@p0,@p1);";

            var result = Context.Database.ExecuteSqlCommand(query, html, _dbUser.CreatedbyID);

            return true;

        }
        public string GetReceipt(long id) {

            string query = "SELECT [DATA] AS HTML  FROM [campuserp].[dbo].[AccReceipts] WHERE ID = @p0";

            var result = Context.Database.SqlQuery<string>(query,id).SingleOrDefault();

            return result;

        }
    }
}
