using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccess.DataUtility;
using DataAccess.Data;
using DataAccess;

public partial class MasterDash : System.Web.UI.Page
{
    public int MasterRiskId
    {
        get
        {
            if (ViewState["MasterRiskId"] == null)
                return 0;
            else
                return (int)ViewState["MasterRiskId"];
        }
        set
        {
            ViewState["MasterRiskId"] = value;
        }
    }

    public int Type
    {
        get
        {
            if (ViewState["Type"] == null)
                return 0;
            else
                return (int)ViewState["Type"];
        }
        set
        {
            ViewState["Type"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                ViewState.Add("ControlCounter", 0);
                //bindMasterRisk();

                if (Request.QueryString["masterRiskId"] != null)
                {
                    MasterRiskId = int.Parse(Request.QueryString["masterRiskId"]);
                }

                if (Request.QueryString["type"] != null)
                {
                    Type = int.Parse(Request.QueryString["type"]);
                }

                int idType = 0;
                idType = Type;

                if (Type == 1)
                {
                    updateLabel.Text = queryMessage(MasterRiskId, Type);
                    updateLabel.Font.Size = 12;
                    updateLabel.ForeColor = System.Drawing.Color.Green;
                }
                else if (Type == 2)
                {
                    updateLabel.Text = queryMessage(MasterRiskId, Type);
                    updateLabel.Font.Size = 12;
                    updateLabel.ForeColor = System.Drawing.Color.Red;
                }
                else if (Type == 3)
                {
                    updateLabel.Text = queryMessage(MasterRiskId, Type);
                    updateLabel.Font.Size = 12;
                    updateLabel.ForeColor = System.Drawing.Color.Green;
                }

            }
            else
            {

            }

        }
        catch (Exception ex)
        {

        }

    }

    //protected void riskDetailGridView_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string qryString = "";
    //    int masterRiskId = int.Parse(riskDetailGridView.SelectedDataKey.Value.ToString());

    //    qryString += "MasterRisk.aspx?masterRiskId=" + masterRiskId;

    //    Response.Redirect(qryString);
    //}

    protected void riskDetailGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    string riskScore = (string)DataBinder.Eval(e.Row.DataItem, "InherentRiskScore");
        //    // change the item's colors.
        //    if (riskScore == "High")
        //    {
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
        //    }
        //}
    }


    protected void riskDetailGridView_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    //private void bindMasterRisk()
    //{
    //    Risk risk = new Risk();

    //    DataTable allRisks = risk.MasterRiskAll();

    //    string[] keys = new string[1];
    //    keys[0] = "MasterRiskID";

    //    riskDetailGridView.DataSource = allRisks;
    //    riskDetailGridView.DataKeyNames = keys;
    //    riskDetailGridView.DataBind();
    //}



    private string queryMessage(int id, int type)
    {
        string message;

        if (type == 1)
        {
            message = "RiskID " + id.ToString() + " has been SUCCESSFULLY saved!" ;
            return message;
        }

        if (type == 2)
        {
            message = "RiskID " + id.ToString() + " has been DELETED!";
            return message;
        }

        if (type == 3)
        {
            message = "Action Plan CREATED for RiskID " + id.ToString() + "!";
            return message;
        }

        return "";
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {   

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

            GridView gv = (GridView)e.Row.FindControl("GridView2");
            SqlDataSource dbSrc = new SqlDataSource();

            dbSrc.ConnectionString = connectionString;
            dbSrc.SelectCommand = "EXEC [dbo].[EntityRisksAll] " + e.Row.Cells[1].Text.ToString() + ",'" + dashButtonList.SelectedValue + "'";

            gv.DataSource = dbSrc;
            gv.DataBind();
        }
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        string searchCriteria = searchCriteriaText.Text.ToString();

        SqlDataSource1.SelectCommand = "EXEC [dbo].[RiskDetailAllQuickSearch] " + "'" + searchCriteria + "'";
        SqlDataSource1.Select(DataSourceSelectArguments.Empty);

        GridView1.DataBind();
    }
    protected void dashButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string searchCriteria = dashButtonList.SelectedValue;

        SqlDataSource1.SelectCommand = "EXEC [dbo].[MasterRiskAll]" + "'" + searchCriteria + "'";
        SqlDataSource1.Select(DataSourceSelectArguments.Empty);

        GridView1.DataBind();

    }


}