using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Web.Configuration;

public partial class AuditReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["RiskRegister"].ToString());
        SqlCommand comm = new SqlCommand("dbo.rptEntityRiskLog", con);
        SqlDataAdapter ad = new SqlDataAdapter(comm);
        DataTable dtexcel = new DataTable();

        try
        {
            con.Open();

            ad.Fill(dtexcel);

            if(dtexcel != null)
            {
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("content-disposition", "attachment;filename=" + "EntityRiskLog.xls");
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                GridView view = new GridView();

                view.DataSource = dtexcel;
                view.DataBind();

                view.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
        }
        catch(Exception ex)
        {

        }
        finally
        {
            con.Close();
        }

    }
}