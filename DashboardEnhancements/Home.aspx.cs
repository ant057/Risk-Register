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

    public int Row
    {
        get
        {
            if (ViewState["Row"] == null)
                return 0;
            else
                return (int)ViewState["Row"];
        }
        set
        {
            ViewState["Row"] = value;
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
                ViewState.Add("Row", 0);
                //entityBind();
                //bindRiskGrid();

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string masterId = DataBinder.Eval(e.Row.DataItem, "MasterRiskID").ToString();
            //updateLabel.Text += "Parent index:" + e.Row.RowIndex + "," + "(Row:" + Row + ")";
            //Row = Row + 1;
            SQLOverLimitDetail.SelectParameters.Clear();
            SQLOverLimitDetail.SelectParameters.Add("Master_Risk_ID", masterId);

            GridView gv = new GridView();
            gv.DataSource = SQLOverLimitDetail;
            gv.ID = "grdSQLOverLimitDetail" + e.Row.RowIndex;
            gv.AutoGenerateColumns = false;
            gv.CssClass = "subgridview";

            gv.RowDataBound += new GridViewRowEventHandler(grdOverLimitDetail_RowDataBound);

            BoundField bf1 = new BoundField();
            bf1.DataField = "RiskID";
            bf1.HeaderText = "RiskID";
            gv.Columns.Add(bf1);

            BoundField bf10 = new BoundField();
            bf10.DataField = "MasterRiskID";
            bf10.HeaderText = "MasterRiskID";
            gv.Columns.Add(bf10);

            BoundField bf2 = new BoundField();
            bf2.DataField = "Entity";
            bf2.HeaderText = "Entity";
            gv.Columns.Add(bf2);

            BoundField bf3 = new BoundField();
            bf3.DataField = "InherentRiskScore";
            bf3.HeaderText = "InherentRiskScore";
            gv.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "MitigatingDetails";
            bf4.HeaderText = "MitigatingDetails";
            gv.Columns.Add(bf4);

            BoundField bf5 = new BoundField();
            bf5.DataField = "ResidualRiskScore";
            bf5.HeaderText = "ResidualRiskScore";
            gv.Columns.Add(bf5);

            BoundField bf6 = new BoundField();
            bf6.DataField = "ModifiedDate";
            bf6.HeaderText = "ModifiedDate";
            gv.Columns.Add(bf6);

            BoundField bf7 = new BoundField();
            bf7.DataField = "ModifiedUser";
            bf7.HeaderText = "ModifiedUser";
            gv.Columns.Add(bf7);

            BoundField bf8 = new BoundField();
            bf8.DataField = "HasActionPlan";
            bf8.HeaderText = "HasActionPlan";
            gv.Columns.Add(bf8);

            BoundField bf9 = new BoundField();
            bf9.DataField = "RiskStrategy";
            bf9.HeaderText = "RiskStrategy";
            gv.Columns.Add(bf9);

            Image button = new Image();
            button.ID = "btnDetail";
            button.ImageUrl = "~/Images/details.jpg";
            button.Attributes.Add("onclick", "javascript: gvrowtoggle(" + e.Row.RowIndex + (e.Row.RowIndex + 2) + ")");

            Table table = (Table)e.Row.Parent;
            GridViewRow tr = new GridViewRow(e.Row.RowIndex + 1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
            tr.CssClass = "hidden";
            TableCell tc = new TableCell();
            tc.ColumnSpan = riskDetailGridView.Columns.Count;
            tc.BorderStyle = BorderStyle.None;
            tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Controls.Add(gv);
            tr.Cells.Add(tc);
            table.Rows.Add(tr);
            e.Row.Cells[0].Controls.Add(button);

            gv.DataBind();

        }
    }

    void grdOverLimitDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //throw new NotImplementedException();
        //Row = Row + 1;
        //updateLabel.Text += "Child index:" + e.Row.RowIndex + ",(Row:" + Row + ")";
    }


    //private void entityBind()
    //{
    //    List<EntityDetail> entityDetails = new List<EntityDetail>();
    //    Entity entity = new Entity();

    //    entityDetails = entity.SelectAllEntity();

    //    foreach (EntityDetail ent in entityDetails)
    //    {
    //        ListItem item = new ListItem();
    //        item.Text = ent.EntityName.ToString();
    //        item.Value = ent.EntityId.ToString();
    //        entityDDL.Items.Add(item);
    //    }

    //    entityDDL.Items.Insert(0, "Select All Risks");
    //}

    //private void bindRiskGrid()
    //{
    //    string[] keys = new string[1];
    //    keys[0] = "RiskID";
    //    Risk risk = new Risk();
    //    DataTable allRisks = risk.RiskDetailAll();

    //    riskDetailGridView.DataSource = allRisks;
    //    riskDetailGridView.AutoGenerateColumns = true;
    //    riskDetailGridView.DataKeyNames = keys;
    //    riskDetailGridView.DataBind();
    //}

    //private void bindRiskGrid(int entityId)
    //{
    //    Risk risk = new Risk();
    //    DataTable risks = risk.RiskDetailByEntity(entityId);
    //    string[] keys = new string[1];
    //    keys[0] = "RiskID";

    //    riskDetailGridView.DataSource = risks;
    //    riskDetailGridView.AutoGenerateColumns = true;
    //    riskDetailGridView.DataKeyNames = keys;
    //    riskDetailGridView.DataBind();
    //}

    protected void entityDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] keys = new string[1];
            keys[0] = "RiskID";

            if (entityDDL.SelectedValue == "Select All Risks")
            {
                //bindRiskGrid();

            }
            else
            {
                //bindRiskGrid(int.Parse(entityDDL.SelectedValue));
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
        //string[] keys = new string[1];
        //keys[0] = "RiskID";
        ////riskDetailGridView.Sort(ddlSort1.SelectedValue + "," + ddlSort2.SelectedValue + "," + ddlSort3.SelectedValue, SortDirection.Descending);

        //try
        //{
        //    Risk risk = new Risk();

        //    if (entityDDL.SelectedIndex != 0)
        //    {
        //        DataTable entityRisks = risk.RiskDetailByEntity(int.Parse(entityDDL.SelectedValue));
        //        DataView view = new DataView(entityRisks);

        //        string exp1, exp2, exp3;
        //        exp1 = ddlSort1.SelectedValue;
        //        exp2 = ddlSort2.SelectedValue;
        //        exp3 = ddlSort3.SelectedValue;

        //        view.Sort = exp1 + "," + exp2 + "," + exp3;

        //        riskDetailGridView.DataSource = view;
        //        riskDetailGridView.AutoGenerateColumns = true;
        //        riskDetailGridView.DataKeyNames = keys;
        //        riskDetailGridView.Sort(exp1 + "," + exp2 + "," + exp3, SortDirection.Ascending);
        //        riskDetailGridView.DataBind();
        //    }

        //    else if (entityDDL.SelectedValue == "Select All Risks")
        //    {
        //        DataTable allRisks = risk.RiskDetailAll();
        //        DataView view = new DataView(allRisks);
        //        view.Table = allRisks;

        //        string exp1, exp2, exp3;
        //        exp1 = ddlSort1.SelectedValue;
        //        exp2 = ddlSort2.SelectedValue;
        //        exp3 = ddlSort3.SelectedValue;

        //        view.Sort = exp1 + "," + exp2 + "," + exp3;

        //        riskDetailGridView.DataSource = view;
        //        riskDetailGridView.AutoGenerateColumns = true;
        //        riskDetailGridView.DataKeyNames = keys;
        //        riskDetailGridView.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{

        //}


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



}