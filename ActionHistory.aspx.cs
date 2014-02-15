using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class ActionHistory : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

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

    private void setConnectionStrings(string connString)
    {
        historyDataSource.ConnectionString = connString;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        setConnectionStrings(connectionString);

        if (!Page.IsPostBack)
        {

            try
            {
                RiskId = int.Parse(Request.QueryString["riskId"]);
            }
            catch (Exception ex)
            {

            }

        }
    }
}