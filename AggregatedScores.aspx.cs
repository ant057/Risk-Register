using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.DataUtility;
using DataAccess.Data;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;

public partial class AggregatedScores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAggregateScores();
        }

        if (editCheckBox.Checked == true) 
        {
            editGridView.CssClass = "";
        }
        else 
        {
            editGridView.CssClass = "invisible";
        }
        
    }

    private void BindAggregateScores()
    {
        Risk risk = new Risk();

        DataTable scores = risk.GetAggregateScores();

        AggregateScores.DataSource = scores;
        AggregateScores.DataBind();

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["RiskRegister"].ToString());
        SqlCommand comm = new SqlCommand("dbo.GetConsolidatedAggregateScore", conn);
        SqlDataAdapter ad = new SqlDataAdapter(comm);

        DataTable consolScores = new DataTable();

        try
        {
            conn.Open();

            ad.Fill(consolScores);

            if (scores != null)
            {
                consolGridView.DataSource = consolScores;
                consolGridView.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }
    }

}