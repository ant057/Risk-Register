using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Data;
using DataAccess.DataUtility;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class ActionPlan : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

    public int RiskId
    {
        get
        {
            return (int)ViewState["RiskId"];
        }
        set
        {
            ViewState["RiskId"] = value;
        }
    }

    public int EntityRiskId
    {
        get
        {
            return (int)ViewState["EntityRiskId"];
        }
        set
        {
            ViewState["EntityRiskId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["entityRiskId"] != null)
                {

                    EntityRiskId = int.Parse(Request.QueryString["entityRiskId"]);
                    DataAccess.DataUtility.EntityRiskDetail entityRiskDetail = new DataAccess.DataUtility.EntityRiskDetail();
                    EntityRisk entityRisk = new EntityRisk();

                    entityRiskDetail = entityRisk.SelectEntityRisk(EntityRiskId);
                    RiskId = entityRiskDetail.RiskId;

                    DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();
                    Risk risk = new Risk();

                    riskDetail = risk.SelectRisk(RiskId);

                    DataAccess.DataUtility.RiskTypeDetail riskTypeDetail = new DataAccess.DataUtility.RiskTypeDetail();
                    RiskType riskType = new RiskType();
                    riskTypeDetail = riskType.SelectRiskType(riskDetail.RiskTypeID);

                    riskIdLabel.Text = EntityRiskId.ToString();
                    riskScenarioTextBox.Text = riskDetail.RiskScenario.ToString();

                    bindOwners();

                    DataTable entityName = new DataTable();

                    entityName = risk.RiskEntity(EntityRiskId);
                    string theEntity = entityName.Rows[0]["Entity_Name"].ToString();
                    entityTitleLab.Text = theEntity;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        DateTime completion = DateTime.Today;
        DateTime implement = DateTime.Today;

        if (completionDateTextBox.Value != "" & implementDateTextBox.Value != "")
        {
            completion = DateTime.Parse(completionDateTextBox.Value);
            implement = DateTime.Parse(implementDateTextBox.Value);
        }

        if (completion < implement)
        {
            string javascript = "<script type=\"text/javascript\">" +
                            "alert(\"Completion date cannot be before implementation date\");" +
                            "</script>\"";

            ClientScript.RegisterStartupScript(this.GetType(), "ScrollPage", javascript);
        }
        else
        {

            if (ddlPrimaryOwner.SelectedIndex != 0 && ddlFollowUpParty.SelectedIndex != 0 && !implementDateTextBox.Value.Equals("") && !completionDateTextBox.Value.Equals(""))
            {
                try
                {

                    DataAccess.DataUtility.ActionPlanDetail actionDetail = new DataAccess.DataUtility.ActionPlanDetail();
                    DataAccess.ActionPlan actionPlan = new DataAccess.ActionPlan();

                    actionDetail.ActionDetail = actionDetailTextBox.Text.ToString();
                    actionDetail.UserID = User.Identity.Name.ToString();
                    actionDetail.ActionEnteredDate = DateTime.Now;
                    actionDetail.ModifiedUserID = User.Identity.Name.ToString();
                    actionDetail.ActionModifiedDate = DateTime.Now;

                    actionDetail.ActionDueDate = DateTime.Today;

                    actionDetail.ActionOwnerID = int.Parse(ddlPrimaryOwner.SelectedValue);
                    actionDetail.ImplementationDate = DateTime.Parse(implementDateTextBox.Value);
                    actionDetail.FollowUpID = int.Parse(ddlFollowUpParty.SelectedValue);
                    actionDetail.CompletionDate = DateTime.Parse(completionDateTextBox.Value);
                    actionDetail.ActionStatus = ddlActionStatus.SelectedValue;
                    actionDetail.EntityRiskID = EntityRiskId;

                    //to update modified user and date with addition of action plan
                    //Risk risk = new Risk();
                    //DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();
                    //riskDetail = risk.SelectRisk(RiskId);

                    //riskDetail.ModifiedUserID = User.Identity.Name.ToString();
                    //riskDetail.RiskModifiedDate = DateTime.Now;

                    //risk.UpdateRisk(riskDetail);

                    int newActionPlanId = 0;
                    newActionPlanId = actionPlan.InsertActionPlan(actionDetail);

                    //insert into history
                    ActionPlanHistoryDetail historyDetail = new ActionPlanHistoryDetail();
                    ActionPlanHistory history = new ActionPlanHistory();

                    historyDetail.ActionDetailNew = actionDetailTextBox.Text.ToString();
                    historyDetail.ActionDetailOld = string.Empty;
                    historyDetail.ModifiedBy = User.Identity.Name.ToString();
                    historyDetail.ModifiedDate = DateTime.Now;
                    historyDetail.RiskID = EntityRiskId;

                    history.InsertActionHistory(historyDetail);


                    actionSaveLabel.Text = "Action Plan has been saved!";
                    actionSaveLabel.ForeColor = System.Drawing.Color.Green;
                    saveButton.Enabled = false;

                    if (newActionPlanId != 0)
                    {
                        Response.Redirect("~/MasterDash.aspx?masterRiskId=" + EntityRiskId + "&type=" + 3);
                    }
                }
                catch (Exception ex)
                {
                    actionSaveLabel.Text = "Invalid or Missing Data - Action Plan NOT Saved!";
                    actionSaveLabel.ForeColor = System.Drawing.Color.Red;
                    actionSaveLabel.Font.Size = 12;
                    resetScrollBack();
                }

            }
            else
            {
                actionSaveLabel.Text = "Invalid or Missing Data - Action Plan NOT Saved!";
                actionSaveLabel.ForeColor = System.Drawing.Color.Red;
                actionSaveLabel.Font.Size = 12;
                resetScrollBack();
            }
        }
        
    }

    //private void bindOwnerListBox()
    //{
    //    List<DataAccess.DataUtility.OwnerDetail> ownerDetails = new List<DataAccess.DataUtility.OwnerDetail>();
    //    Owner owner = new Owner();

    //    ownerDetails = owner.SelectAllOwners();

    //    foreach (OwnerDetail listOwner in ownerDetails)
    //    {
    //        ListItem item = new ListItem();
    //        item.Text = listOwner.LastName + "," + listOwner.FirstName;
    //        item.Value = listOwner.ownerID.ToString();

    //        ownersListBox.Items.Add(item);
    //    }

    //}

    private void bindOwners()
    {
        try
        {
            DataTable owners = new DataTable();
            Owner owner = new Owner();
            owners = owner.SelectAllOwners();

            DataView primaryOwners = new DataView(owners);
            primaryOwners.RowFilter = "Owner_Type_Id = 500";

            ddlPrimaryOwner.DataSource = primaryOwners;
            ddlPrimaryOwner.DataTextField = "Owner_Desc";
            ddlPrimaryOwner.DataValueField = "Owner_Id";
            ddlPrimaryOwner.DataBind();
            ddlPrimaryOwner.Items.Insert(0, "--Select--");

            ddlFollowUpParty.DataSource = primaryOwners;
            ddlFollowUpParty.DataTextField = "Owner_Desc";
            ddlFollowUpParty.DataValueField = "Owner_Id";
            ddlFollowUpParty.DataBind();
            ddlFollowUpParty.Items.Insert(0, "--Select--");   

        }
        catch (Exception ex)
        {

        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx");
    }

    private void resetScrollBack()
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);
        //RegisterStartupScript(this.GetType(), "ScrollPage", "ResetScrollPosition();", true);
        //RegisterStartupScript("ScrollPage", "ResetScrollPosition();");

        string javascript = "<script type=\"text/javascript\">" +
                            "window.scrollTo(0, 0);" +
                            "</script>\"";

        MaintainScrollPositionOnPostBack = false;
        ClientScript.RegisterStartupScript(this.GetType(), "ScrollPage", javascript);
    }
}