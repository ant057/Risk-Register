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
    }

}