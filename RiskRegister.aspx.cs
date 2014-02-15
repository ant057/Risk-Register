using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using DataAccess.Data;
using DataAccess.DataUtility;
using DataAccess;

public partial class RiskRegister : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        riskRegDS.ConnectionString = connectionString;
        entitiesDS.ConnectionString = connectionString;

        if (!Page.IsPostBack)
        {
            ViewState.Add("ControlCounter", 0);
        }
    }

    private DataTable fillDataTable(SqlCommand command, string tableName)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        command.Connection = connection;
        SqlDataAdapter adapter = new SqlDataAdapter(command);

        DataTable dataTable = new DataTable();

        try
        {
            connection.Open();
            adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }

        return dataTable;
    }

    protected void reportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //// Look for data items.
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    // Retrieve the GridView control in the second column.
        //    DataList listViewChild = (DataList)e.Row.Cells[7].Controls[1];
        //    // Set the CategoryID parameter so you get the products
        //    // in the current category only.
        //    string RiskID = reportGridView.DataKeys[e.Row.DataItemIndex].Value.ToString();
        //    entitiesDS.SelectParameters[0].DefaultValue = RiskID;
            
        //    // Get the data object from the data source.
        //    object eData = entitiesDS.Select(DataSourceSelectArguments.Empty);
        //    // Bind the grid.
        //    listViewChild.DataSource = eData;
        //    listViewChild.DataBind();
        //}
    }

    protected void excelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //DataTable dtRisk = new DataTable();
            //Risk risk = new Risk();

            //DataTable dtexcel;
            //dtexcel = dtRisk.Copy();

            if (reportGridView.Rows.Count > 0)
            {
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("content-disposition", "attachment;filename=RiskRegister.xls");
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                GridView view = new GridView();

                view.DataSource = reportGridView;
               // view.DataBind();

                view.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
            }
            Response.End();
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

    protected void sortButton_Click(object sender, EventArgs e)
    {
        string exp1, exp2, exp3;
        exp1 = ddlSort1.SelectedValue;
        exp2 = ddlSort2.SelectedValue;
        exp3 = ddlSort3.SelectedValue;

        reportGridView.Sort(exp1 + "," + exp2 + "," + exp3, SortDirection.Descending);
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
}