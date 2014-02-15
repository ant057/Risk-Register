using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Data;
using DataAccess.DataUtility;

public partial class RiskHistory : System.Web.UI.Page
{
    public int EntityRiskId
    {
        get
        {
            if (ViewState["entityRiskId"] == null)
                return 0;
            else
                return (int)ViewState["entityRiskId"];
        }
        set
        {
            ViewState["entityRiskId"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EntityRiskId = int.Parse(Request.QueryString["entityRiskId"].ToString());

            BindRiskLog();
        }
    }

    private void BindRiskLog()
    {
        EntityRiskLog entityRiskLog = new EntityRiskLog();
        List<EntityRiskLogDetail> entityLogDetails = new List<EntityRiskLogDetail>();

        entityLogDetails = entityRiskLog.SelectEntityRiskLogAll(EntityRiskId);

        foreach(EntityRiskLogDetail log in entityLogDetails)
        {
            ListItem item = new ListItem();
            item.Value = log.EntityRiskId.ToString();
            item.Text = "Risk Update - " + log.DateModified.ToString() + " - " + log.ModifiedUser.ToString();
            riskLogListBox.Items.Add(item);
        }
    }
}