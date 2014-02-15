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

public partial class Home : System.Web.UI.Page
{
    public int RiskId
    {
        get
        {
            if (ViewState["RiskId"] == null)
                return 0;
            else
                return (int)ViewState["RiskId"];
        }
        set
        {
            ViewState["RiskId"] = value;
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
                entityBind();
                bindRiskGrid();

                if (Request.QueryString["riskId"] != null)
                {
                    RiskId = int.Parse(Request.QueryString["riskId"]);
                }

                if (Request.QueryString["type"] != null)
                {
                    Type = int.Parse(Request.QueryString["type"]);
                }

                int idType = 0;
                idType = Type;

                if (Type == 1)
                {
                    updateLabel.Text = queryMessage(RiskId, Type);
                    updateLabel.Font.Size = 12;
                    updateLabel.ForeColor = System.Drawing.Color.Green;
                }
                else if (Type == 2)
                {
                    updateLabel.Text = queryMessage(RiskId, Type);
                    updateLabel.Font.Size = 12;
                    updateLabel.ForeColor = System.Drawing.Color.Red;
                }
                else if (Type == 3)
                {
                    updateLabel.Text = queryMessage(RiskId, Type);
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
    protected void riskDetailGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qryString = "";
        int riskId = int.Parse(riskDetailGridView.SelectedDataKey.Value.ToString());

        qryString += "RiskDetail.aspx?riskId=" + riskId;

        Response.Redirect(qryString);
    }

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

    private void entityBind()
    {
        List<EntityDetail> entityDetails = new List<EntityDetail>();
        Entity entity = new Entity();

        entityDetails = entity.SelectAllEntity();

        foreach (EntityDetail ent in entityDetails)
        {
            ListItem item = new ListItem();
            item.Text = ent.EntityName.ToString();
            item.Value = ent.EntityId.ToString();
            entityDDL.Items.Add(item);
        }

        entityDDL.Items.Insert(0, "Select All Risks");
    }

    private void bindRiskGrid()
    {
        string[] keys = new string[1];
        keys[0] = "RiskID";
        Risk risk = new Risk();
        DataTable allRisks = risk.RiskDetailAll();

        riskDetailGridView.DataSource = allRisks;
        riskDetailGridView.AutoGenerateColumns = true;
        riskDetailGridView.DataKeyNames = keys;
        riskDetailGridView.DataBind();
    }

    private void bindRiskGrid(int entityId)
    {
        Risk risk = new Risk();
        DataTable risks = risk.RiskDetailByEntity(entityId);
        string[] keys = new string[1];
        keys[0] = "RiskID";

        riskDetailGridView.DataSource = risks;
        riskDetailGridView.AutoGenerateColumns = true;
        riskDetailGridView.DataKeyNames = keys;
        riskDetailGridView.DataBind();
    }

    protected void entityDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] keys = new string[1];
            keys[0] = "RiskID";

            if (entityDDL.SelectedValue == "Select All Risks")
            {
                bindRiskGrid();

            }
            else
            {
                bindRiskGrid(int.Parse(entityDDL.SelectedValue));
            }
        }
        catch (Exception ex)
        {

        }
    }



    protected void riskDetailGridView_Sorting(object sender, GridViewSortEventArgs e)
    {

    }


    protected void sortButton_Click(object sender, EventArgs e)
    {
        string[] keys = new string[1];
        keys[0] = "RiskID";
        //riskDetailGridView.Sort(ddlSort1.SelectedValue + "," + ddlSort2.SelectedValue + "," + ddlSort3.SelectedValue, SortDirection.Descending);

        try
        {
            Risk risk = new Risk();

            if (entityDDL.SelectedIndex != 0)
            {
                DataTable entityRisks = risk.RiskDetailByEntity(int.Parse(entityDDL.SelectedValue));
                DataView view = new DataView(entityRisks);

                string exp1, exp2, exp3;
                exp1 = ddlSort1.SelectedValue;
                exp2 = ddlSort2.SelectedValue;
                exp3 = ddlSort3.SelectedValue;

                view.Sort = exp1 + "," + exp2 + "," + exp3;

                riskDetailGridView.DataSource = view;
                riskDetailGridView.AutoGenerateColumns = true;
                riskDetailGridView.DataKeyNames = keys;
                riskDetailGridView.Sort(exp1 + "," + exp2 + "," + exp3, SortDirection.Ascending);
                riskDetailGridView.DataBind();
            }

            else if (entityDDL.SelectedValue == "Select All Risks")
            {
                DataTable allRisks = risk.RiskDetailAll();
                DataView view = new DataView(allRisks);
                view.Table = allRisks;

                string exp1, exp2, exp3;
                exp1 = ddlSort1.SelectedValue;
                exp2 = ddlSort2.SelectedValue;
                exp3 = ddlSort3.SelectedValue;

                view.Sort = exp1 + "," + exp2 + "," + exp3;

                riskDetailGridView.DataSource = view;
                riskDetailGridView.AutoGenerateColumns = true;
                riskDetailGridView.DataKeyNames = keys;
                riskDetailGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            
        }


    }

    protected void addFilterButton_Click(object sender, EventArgs e)
    {

        if ((int)ViewState["ControlCounter"] == 0)
        {
            sort2Label.Visible = true;
            ddlSort2.Visible = true;

            ViewState["ControlCounter"] = (int)ViewState["ControlCounter"] + 1;
        }
        else if ((int)ViewState["ControlCounter"] == 1)
        {
            sort3Label.Visible = true;
            ddlSort3.Visible = true;
            ViewState["ControlCounter"] = (int)ViewState["ControlCounter"] + 1;
        }

    }

    protected void removeFilterButton_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ControlCounter"] == 2)
        {
            sort3Label.Visible = false;
            ddlSort3.Visible = false;
            ddlSort3.SelectedIndex = 0;
            ViewState["ControlCounter"] = (int)ViewState["ControlCounter"] - 1;
        }
        else if ((int)ViewState["ControlCounter"] == 1)
        {
            sort2Label.Visible = false;
            ddlSort2.Visible = false;
            ddlSort2.SelectedIndex = 0;
            ViewState["ControlCounter"] = (int)ViewState["ControlCounter"] - 1;
        }
    }

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

    protected void searchButton_Click(object sender, EventArgs e)
    {
        if (!searchButton.Text.Equals(""))
        {
            string[] keys = new string[1];
            keys[0] = "RiskID";
            Risk risk = new Risk();
            DataTable quickRisks = risk.RiskDetailAllQuickSearch(searchCriteriaText.Text);

            riskDetailGridView.DataSource = quickRisks;
            riskDetailGridView.AutoGenerateColumns = true;
            riskDetailGridView.DataKeyNames = keys;
            riskDetailGridView.DataBind();
        }
    }

}