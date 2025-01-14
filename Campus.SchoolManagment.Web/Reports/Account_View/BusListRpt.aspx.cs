using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Campus.SchoolManagment.Web.Reports.Account_View
{
    public partial class BusListRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadReport();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            loadReport();
        }



        void loadReport()
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Con.Open();
            ReportDocument rpt = new ReportDocument();
            DataSet ds = new DataSet();
            string query = "select * from    BusList_view";

            SqlDataAdapter _Adapter = new SqlDataAdapter(query, Con);
            _Adapter.Fill(ds, "BusList_view");
            rpt.Load(Server.MapPath("~/Reports/Account_RPT/BusListReport.rpt"));
            rpt.SetDataSource(ds);
            Con.Close();
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
        }

        protected void CrystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                loadReport();

            }

        }


    }
}