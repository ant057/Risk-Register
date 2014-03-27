using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Data;
using DataAccess.DataUtility;
using System.Data;
using System.Web.Configuration;

public partial class RiskDetail : System.Web.UI.Page
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

    public int EntityRiskId
    {
        get
        {
            if (ViewState["EntityRiskId"] == null)
                return 0;
            else
                return (int)ViewState["EntityRiskId"];
        }
        set
        {
            ViewState["EntityRiskId"] = value;
        }
    }

    public int ActionPlanId
    {
        get
        {
            if (ViewState["ActionPlanId"] == null)
                return 0;
            else
                return (int)ViewState["ActionPlanId"];
        }
        set
        {
            ViewState["ActionPlanId"] = value;
        }
    }

    string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        setConnectionStrings(connectionString);

        if (!Page.IsPostBack)
        {

            try
            {
                EntityRiskId = int.Parse(Request.QueryString["entityRiskId"]);

                //bindControls();
                BindRiskDetails();

            }
            catch (Exception ex)
            {

            }

        }
        else
        {

        }
    }

    private void setConnectionStrings(string connString)
    {

        riskTypeDS.ConnectionString = connString;
        riskSourceDS.ConnectionString = connString;

    }

    protected void createActionButton_Click(object sender, EventArgs e)
    {
        if (RiskId != null)
        {
            string qryString = "";
            qryString += "ActionPlan.aspx?entityRiskId=" + EntityRiskId;

            Response.Redirect(qryString);
        }
    }

    private void BindRiskDetails()
    {
        bindOwners();

        DataAccess.DataUtility.EntityRiskDetail enttiyRiskDetail = new DataAccess.DataUtility.EntityRiskDetail();
        EntityRisk entityRisk = new EntityRisk();
        enttiyRiskDetail = entityRisk.SelectEntityRisk(EntityRiskId);

        RiskId = enttiyRiskDetail.RiskId;

        DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();
        Risk risk = new Risk();
        riskDetail = risk.SelectRisk(RiskId);

        //risk details
        ddlRiskType.SelectedValue = riskDetail.RiskTypeID.ToString();
        ddlOversightParty.SelectedValue = riskDetail.OversightPartyID.ToString();
        bindRiskSource(riskDetail.RiskTypeID, riskDetail.RiskSourceID);
        riskScenarioTextBox.Text = riskDetail.RiskScenario.ToString();
        riskDateTextBox.Text = riskDetail.RiskEnteredDate.ToShortDateString();
        ddlPrimaryOwner.SelectedValue = riskDetail.PrimaryOwnerID.ToString();

        //entity risk details
        riskIdLabel.Text = enttiyRiskDetail.EntityRiskId.ToString();
        string frequency = enttiyRiskDetail.Frequency.ToString();
        if (frequency == "0") {ListItem frequencyListItem = new ListItem("N/A", "0"); frequencyTextBox.Items.Clear(); frequencyTextBox.Items.Add(frequencyListItem); }
        else 
           frequencyTextBox.SelectedValue = enttiyRiskDetail.Frequency.ToString();

        string severity = enttiyRiskDetail.Severity.ToString();
        ListItem severityListItem;
        if (severity == "1") { severityListItem = new ListItem("1 - Low", "1"); severityTextBox.Items.Add(severityListItem); }
        else if (severity == "2") { severityListItem = new ListItem("2 - Moderate", "2"); severityTextBox.Items.Add(severityListItem); }
        else if (severity == "3") { severityListItem = new ListItem("3 - Significant", "3"); severityTextBox.Items.Add(severityListItem); }
        else if (severity == "4") { severityListItem = new ListItem("4 - Catastrophe", "4"); severityTextBox.Items.Add(severityListItem); }
        else if (severity == "0") { severityListItem = new ListItem("N/A", "0"); severityTextBox.Items.Add(severityListItem); riskScoreTextBox.Text = "N/A"; }

        riskScoreTextBox.Text = enttiyRiskDetail.InherentRiskScore.ToString();
        userLabel.Text = enttiyRiskDetail.ModifiedUserID.ToString();

        string adjustment = enttiyRiskDetail.ResidualAdjustment.ToString();
        ListItem adjustmentListItem;
        if (adjustment == "0") { adjustmentListItem = new ListItem("0", "0"); residualAdjustmentDDL.Items.Add(adjustmentListItem); }
        else if (adjustment == "-1") { adjustmentListItem = new ListItem("-1", "-1"); residualAdjustmentDDL.Items.Add(adjustmentListItem); }
        else if (adjustment == "-2") { adjustmentListItem = new ListItem("-2", "-2"); residualAdjustmentDDL.Items.Add(adjustmentListItem); }
        else if (adjustment == "-1000") { adjustmentListItem = new ListItem("N/A", "-1000"); residualAdjustmentDDL.Items.Add(adjustmentListItem); residualScoreText.Text = "N/A"; }

        residualScoreText.Text = enttiyRiskDetail.ResidualRiskScore.ToString();
        SetScoreColors();
        mitigatingControlsFTB.Text = enttiyRiskDetail.MitigatingActionsDetail.ToString();

        if (riskScoreTextBox.Text == "N/A" && residualScoreText.Text == "N/A")
        {
            naCheckBox.Checked = true;
        }

        //action plan

        DataAccess.DataUtility.ActionPlanDetail actionDetail = new DataAccess.DataUtility.ActionPlanDetail();
        DataAccess.ActionPlan actionPlan = new DataAccess.ActionPlan();
        actionDetail = actionPlan.SelectActionFromRiskId(EntityRiskId);

     // bindRiskOwnerListBox(RiskId);
        bindEntityCBList();

        if(actionDetail != null)
        {
            ActionPlanId = actionDetail.ActionPlanID;

            createActionButton.Visible = false;
            actionDetailTextBox.Text = actionDetail.ActionDetail.ToString();
            actionUserLabel.Text = actionDetail.UserID.ToString();
            actionCreatedTextBox.Text = actionDetail.ActionEnteredDate.ToShortDateString();
            ddlActionOwner.SelectedValue = actionDetail.ActionOwnerID.ToString();
            implementDateTextBox.Value = actionDetail.ImplementationDate.ToShortDateString();
            ddlFollowUpParty.SelectedValue = actionDetail.FollowUpID.ToString();
            completedDateTextBox.Value = actionDetail.CompletionDate.ToShortDateString();
            ddlActionStatus.SelectedValue = actionDetail.ActionStatus;

        }
        else//no action
        {
            actionDetailTextBox.Text = "No Action Plan";
            actionHistoryButton.Visible = false;
            ddlActionStatus.Enabled = false;
        }

    }

    private void bindRiskSource()
    {
        try
        {
            ddlRiskSource.Items.Clear();

            int riskTypeId = 0;
            riskTypeId = int.Parse(ddlRiskType.SelectedValue);

            RiskSource riskSource = new RiskSource();
            DataTable sources = new DataTable();

            sources = riskSource.SelectRiskSource(riskTypeId);

            ddlRiskSource.DataSource = sources;
            ddlRiskSource.DataTextField = "Risk_Source_Desc";
            ddlRiskSource.DataValueField = "Risk_Source_ID";
            ddlRiskSource.DataBind();
            ddlRiskSource.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {

        }

    }

    private void bindRiskSource(int riskTypeId, int riskSourceId)
    {
        try
        {
            ddlRiskSource.Items.Clear();

            RiskSource riskSource = new RiskSource();
            DataTable sources = new DataTable();

            sources = riskSource.SelectRiskSource(riskTypeId);

            ddlRiskSource.DataSource = sources;
            ddlRiskSource.DataTextField = "Risk_Source_Desc";
            ddlRiskSource.DataValueField = "Risk_Source_ID";
            ddlRiskSource.DataBind();
            ddlRiskSource.Items.Insert(0, "--Select--");

            ddlRiskSource.SelectedValue = riskSourceId.ToString();
        }
        catch (Exception ex)
        {

        }

    }

    private void bindEntityCBList()
    {
        //entities 
        List<DataAccess.DataUtility.EntityDetail> entities = new List<DataAccess.DataUtility.EntityDetail>();
        Entity entity = new Entity();
        entities = entity.SelectAllEntity();

        foreach (EntityDetail entDetail in entities)
        {
            ListItem item = new ListItem();
            item.Text = entDetail.EntityName.ToString();
            item.Value = entDetail.EntityId.ToString();
            entityCheckBoxList.Items.Add(item);
        }


        DataAccess.Data.EntityRisk entityRisk = new DataAccess.Data.EntityRisk();
        List<DataAccess.DataUtility.EntityRiskDetail> erDetail = new List<DataAccess.DataUtility.EntityRiskDetail>();

        erDetail = entityRisk.SelectEntityRisks(RiskId);

        foreach (EntityRiskDetail item in erDetail)
        {
            for (int i = 0; i < entityCheckBoxList.Items.Count; i++)
            {
                if (item.EntityId.ToString() == entityCheckBoxList.Items[i].Value)
                {
                    entityCheckBoxList.Items[i].Selected = true;
                }
            }
        }

        DataTable entityName = new DataTable();
        Risk risk = new Risk();

        entityName = risk.RiskEntity(EntityRiskId);

        string theEntity = entityName.Rows[0]["Entity_Name"].ToString();
        entityTitleLabel.Text = theEntity;
        entityTitleLabel0.Text = theEntity;
        entityTitleLabel1.Text = theEntity;
        entityLabel.Text = theEntity;

    }

    private void bindOwners()
    {
        try
        {
            DataTable owners = new DataTable();
            Owner primOwner = new Owner();
            owners = primOwner.SelectAllOwners();

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

            ddlActionOwner.DataSource = primaryOwners;
            ddlActionOwner.DataTextField = "Owner_Desc";
            ddlActionOwner.DataValueField = "Owner_Id";
            ddlActionOwner.DataBind();
            ddlActionOwner.Items.Insert(0, "--Select--");

            DataTable owners2 = new DataTable();
            Owner sightOwner = new Owner();
            owners2 = sightOwner.SelectAllOwners();

            DataView oversightParties = new DataView(owners2);
            oversightParties.RowFilter = "Owner_Type_Id = 501";

            ddlOversightParty.DataSource = oversightParties;
            ddlOversightParty.DataTextField = "Owner_Desc";
            ddlOversightParty.DataValueField = "Owner_Id";
            ddlOversightParty.DataBind();
            ddlOversightParty.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {

        }
    }

    protected void editButton_Click(object sender, EventArgs e)
    {
        resetScrollBack();

        riskUpdateLabel.Text = "";
        editButton.Visible = false;
        cancelButton.Visible = true;
        saveButton.Visible = true;
        frequencyTextBox.Enabled = true;
        severityTextBox.Enabled = true;
        residualAdjustmentDDL.Enabled = true;
        mitigatingControlsFTB.ReadOnly = false;
        naCheckBox.Enabled = true;

        if (actionDetailTextBox.Text != "No Action Plan")
        {
            ddlActionStatus.Enabled = true;
            ddlFollowUpParty.Enabled = true;
            ddlActionOwner.Enabled = true;
            actionDetailTextBox.ReadOnly = false;
        }

        else
        {
            
        }
      
    }

   
    protected void saveButton_Click(object sender, EventArgs e)
    {
        DateTime completion = DateTime.Today;
        DateTime implement = DateTime.Today;

        if (completedDateTextBox.Value != "" & implementDateTextBox.Value != "")
        {
            completion = DateTime.Parse(completedDateTextBox.Value);
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
            if (validatePage())
            {   //update Risk Table

                try
                {
                    DataAccess.DataUtility.EntityRiskDetail entityRiskDetail = new DataAccess.DataUtility.EntityRiskDetail();
                    EntityRisk entityRisk = new EntityRisk();
                    entityRiskDetail = entityRisk.SelectEntityRisk(EntityRiskId);

                    DataAccess.DataUtility.EntityRiskDetail entityRiskDetailOld = new DataAccess.DataUtility.EntityRiskDetail();
                    entityRiskDetailOld = entityRisk.SelectEntityRisk(EntityRiskId);

                    entityRiskDetail.ModifiedUserID = User.Identity.Name.ToString();
                    entityRiskDetail.Frequency = int.Parse(frequencyTextBox.Text);
                    entityRiskDetail.Severity = int.Parse(severityTextBox.Text);
                    entityRiskDetail.InherentRiskScore = riskScoreTextBox.Text.ToString();
                    entityRiskDetail.MitigatingActionsDetail = mitigatingControlsFTB.Text.ToString();
                    entityRiskDetail.ResidualAdjustment = int.Parse(residualAdjustmentDDL.SelectedValue);
                    entityRiskDetail.ResidualRiskScore = residualScoreText.Text.ToString();
                    entityRiskDetail.ModifiedDate = DateTime.Now;
                    entityRiskDetail.PrimaryOwnerId = ddlPrimaryOwner.SelectedIndex == 0 ? 0 : int.Parse(ddlPrimaryOwner.SelectedValue);
                    entityRiskDetail.OversightPartyId = ddlOversightParty.SelectedIndex == 0 ? 0 : int.Parse(ddlOversightParty.SelectedValue);

                    entityRisk.UpdateEntityRisk(entityRiskDetail);

                    //check if something updated, insert to risk log
                    if(verifyChange(entityRiskDetail, entityRiskDetailOld))
                    {
                        EntityRiskLog log = new EntityRiskLog();
                        EntityRiskLogDetail logDetail = new EntityRiskLogDetail();

                        logDetail.Adjustment = entityRiskDetailOld.ResidualAdjustment.ToString();
                        logDetail.DateModified = DateTime.Now;
                        logDetail.EntityRiskId = EntityRiskId;
                        logDetail.Frequency = entityRiskDetailOld.Frequency.ToString();
                        logDetail.InherentRiskScore = entityRiskDetailOld.InherentRiskScore.ToString();
                        logDetail.MitigatingControls = entityRiskDetailOld.MitigatingActionsDetail.ToString();
                        logDetail.ModifiedUser = User.Identity.Name.ToString();
                        logDetail.ResidualRiskScore = entityRiskDetailOld.ResidualRiskScore;
                        logDetail.Severity = entityRiskDetailOld.Severity.ToString();

                        Owner owner = new Owner();
                        DataTable oldOwner = owner.SelectOwner(entityRiskDetailOld.OversightPartyId);

                        logDetail.OversightParty = oldOwner.Rows[0]["Owner_Desc"].ToString();

                        oldOwner.Rows.Clear();
                        oldOwner = owner.SelectOwner(entityRiskDetailOld.PrimaryOwnerId);

                        logDetail.PrimaryOwner = oldOwner.Rows[0]["Owner_Desc"].ToString();

                        log.InsertEntityRisk(logDetail);
                    }

                     //algorithm to detect what is a new entity association and what was removed
                
                    /*//update EntityRisk table
                    List<EntityRiskDetail> entityRiskDetail = new List<EntityRiskDetail>();
                    DataAccess.Data.EntityRisk entityRisk = new DataAccess.Data.EntityRisk();
                    entityRiskDetail = entityRisk.SelectEntityRisk(RiskId);

                    if (entityRiskDetail != null)
                    {
                        foreach (ListItem item in entityCheckBoxList.Items)
                        {

                            if (item.Selected)
                            {
                                bool isPresent = false;

                                for (int i = 0; i < entityRiskDetail.Count; i++)
                                {
                                    if (int.Parse(item.Value) == entityRiskDetail[i].EntityId)
                                    {
                                        isPresent = true;
                                    }
                                }

                                if (isPresent == false)
                                {
                                    EntityRiskDetail newEntityRisk = new EntityRiskDetail();
                                    newEntityRisk.EntityId = int.Parse(item.Value);
                                    newEntityRisk.RiskId = RiskId;
                                    entityRisk.InsertEntityRisk(newEntityRisk);
                                }
                            }
                            else
                            {

                                for (int i = 0; i < entityRiskDetail.Count; i++)
                                {
                                    if (int.Parse(item.Value) == entityRiskDetail[i].EntityId)
                                    {
                                        entityRisk.DeleteEntityRisk(entityRiskDetail[i].EntityRiskId);
                                    }
                                }

                            }

                        }
                    }
                    else
                    {
                        foreach (ListItem item in entityCheckBoxList.Items)
                        {
                            EntityRiskDetail newEntityRisk = new EntityRiskDetail();
                            newEntityRisk.EntityId = int.Parse(item.Value);
                            newEntityRisk.RiskId = RiskId;
                            entityRisk.InsertEntityRisk(newEntityRisk);
                        }
                    }*/


                    //update action items - only if there exists an action plan
                    if (actionDetailTextBox.Text != "No Action Plan")
                    {

                        //update Action Plan table
                        ActionPlanDetail actionDetail = new ActionPlanDetail();
                        DataAccess.ActionPlan action = new DataAccess.ActionPlan();
                        actionDetail = action.SelectActionFromRiskId(EntityRiskId);

                        if (actionDetail != null)
                        {

                            //insert into history
                            if (!actionDetail.ActionDetail.Equals(actionDetailTextBox.Text.ToString()))
                            {
                                ActionPlanHistoryDetail historyDetail = new ActionPlanHistoryDetail();
                                ActionPlanHistory history = new ActionPlanHistory();

                                string oldActionDetails = actionDetail.ActionDetail.ToString();

                                historyDetail.ActionDetailNew = actionDetailTextBox.Text.ToString();
                                historyDetail.ActionDetailOld = oldActionDetails;
                                historyDetail.ModifiedBy = User.Identity.Name.ToString();
                                historyDetail.ModifiedDate = DateTime.Now;
                                historyDetail.RiskID = EntityRiskId;

                                history.InsertActionHistory(historyDetail);
                            }

                            actionDetail.ActionDetail = actionDetailTextBox.Text.ToString();
                            actionDetail.ModifiedUserID = User.Identity.Name;
                            actionDetail.ActionModifiedDate = DateTime.Now;
                            //actionDetail.ActionDueDate = DateTime.Parse(dueDateTextBox.Text.ToString());
                            actionDetail.ActionOwnerID = int.Parse(ddlActionOwner.SelectedValue);
                            actionDetail.ImplementationDate = DateTime.Parse(implementDateTextBox.Value);
                            actionDetail.FollowUpID = int.Parse(ddlFollowUpParty.SelectedValue);
                            actionDetail.CompletionDate = DateTime.Parse(completedDateTextBox.Value);
                            actionDetail.ActionStatus = ddlActionStatus.Text.ToString();

                            action.UpdateActionPlan(actionDetail);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error updating risk/action");
                }


                saveButton.Visible = false;
                editButton.Enabled = false;
                cancelButton.Visible = false;

                riskUpdateLabel.Text = "Risk " + EntityRiskId.ToString() + " has been SUCCESSFULLY updated!";
                riskUpdateLabel.ForeColor = System.Drawing.Color.Green;
                riskUpdateLabel.Font.Size = 12;
                editButton.Enabled = true;
                resetScrollBack();
                lockDownControls();
            }
            else
            {
                riskUpdateLabel.Text = "Risk NOT updated - Invalid data input(s)";
                riskUpdateLabel.ForeColor = System.Drawing.Color.Red;
                riskUpdateLabel.Font.Size = 12;
                editButton.Enabled = true;
                resetScrollBack();
            }
        }

    }


    protected void frequencyTextBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            riskScoreTextBox.Text = "";
            residualScoreText.Text = "";
            SetSeverityItems(frequencyTextBox.SelectedValue.ToString());
        }
        catch (Exception ex)
        {

        }
    }

    private void SetSeverityItems(string frequency)
    {
        severityTextBox.Items.Clear();
        residualAdjustmentDDL.Items.Clear();

        if (frequency == "1")
        {
            severityTextBox.Items.Add(new ListItem("4 - Catastrophic", "4"));
        }
        else if(frequency == "2")
        {
            severityTextBox.Items.Add(new ListItem("1 - Low", "1"));
            severityTextBox.Items.Add(new ListItem("2 - Moderate", "2"));
            severityTextBox.Items.Add(new ListItem("3 - Significant", "3"));
        }
        else if(frequency == "3")
        {
            severityTextBox.Items.Add(new ListItem("1 - Low", "1"));
            severityTextBox.Items.Add(new ListItem("2 - Moderate", "2"));
        }
        else if (frequency == "4")
        {
            severityTextBox.Items.Add(new ListItem("1 - Low", "1"));
        }

        severityTextBox.Items.Insert(0, "--Select--");
    }

    protected void residualAdjustmentDDL_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (riskScoreTextBox.Text == "MEDIUM")
            {
                if (residualAdjustmentDDL.SelectedValue == "0")
                    residualScoreText.Text = "MEDIUM";
                if (residualAdjustmentDDL.SelectedValue == "-1")
                    residualScoreText.Text = "LOW";
            }
            if (riskScoreTextBox.Text == "HIGH")
            {
                if (residualAdjustmentDDL.SelectedValue == "0")
                    residualScoreText.Text = "HIGH";
                if (residualAdjustmentDDL.SelectedValue == "-1")
                    residualScoreText.Text = "MEDIUM";
                if (residualAdjustmentDDL.SelectedValue == "-2")
                    residualScoreText.Text = "LOW";
            }

            SetScoreColors();
        }
        catch (Exception ex)
        {

        }
    }


    protected void excelButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtRisk= new DataTable();
            Risk risk = new Risk();

            dtRisk = risk.RiskDetailExport(RiskId);

            DataTable dtexcel;
            dtexcel = dtRisk.Copy();

            if (dtexcel.Rows.Count > 0)
            {
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("content-disposition", "attachment;filename=RiskDetail.xls");
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                DetailsView view = new DetailsView();

                view.DataSource = dtexcel;
                view.DataBind();

                view.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
            }
            Response.End();
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void documentsButton_Click(object sender, EventArgs e)
    {
        try
        {
            string url = "";
            url = "Documents.aspx?riskId=" + RiskId.ToString();
            Response.Redirect(url);
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlRiskType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRiskSource();
    }


    private bool validatePage()
    {
        bool ddlValid = false;
        bool actionDetailsDDL = false;
        bool riskTextBoxes = false;
        bool actionTextBoxes = true;

        //risk ddl's
        if (ddlRiskSource.SelectedIndex != 0 && frequencyTextBox.SelectedValue != "--Select--" && severityTextBox.Items.Count != 0 && residualAdjustmentDDL.Items.Count != 0)
        {
            ddlValid = true;
        }

        //textboxes
        if (!riskScenarioTextBox.Text.Equals("") &&  !riskScoreTextBox.Text.Equals("") && !residualScoreText.Text.Equals(""))
        {
            riskTextBoxes = true;
        }


        if (!actionDetailTextBox.Text.Equals("No Action Plan"))
        {

            if (ddlFollowUpParty.SelectedIndex != 0 && ddlActionOwner.SelectedIndex != 0 && !actionDetailTextBox.Text.Equals(""))
            {
                actionDetailsDDL = true;
            }
            if (!actionDetailTextBox.Text.Equals(""))
            {
                actionTextBoxes = true;
            }
        }
        else
        {
            actionDetailsDDL = true; actionTextBoxes = true;
        }

        if (!ddlValid || !riskTextBoxes || !actionDetailsDDL || !actionTextBoxes)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    protected void deleteRiskButton_Click(object sender, EventArgs e)
    {
        EntityRisk entityRisk = new EntityRisk();
        DataAccess.ActionPlan actionPlan = new DataAccess.ActionPlan();
        DataAccess.Documents document = new DataAccess.Documents();

        document.DeleteRiskDocuments(RiskId);
        actionPlan.DeleteActionPlan(ActionPlanId);
        entityRisk.DeleteEntityRisk(EntityRiskId);

        int type = 2;
        Response.Redirect("~/MasterDash.aspx?masterRiskId=" + EntityRiskId + "&type=" + type);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterDash.aspx");
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

    private void lockDownControls()
    {

        editButton.Visible = true;
        cancelButton.Visible = false;
        saveButton.Visible = false;
        frequencyTextBox.Enabled = false;
        severityTextBox.Enabled = false;
        residualAdjustmentDDL.Enabled = false;
        //mitigatingControlsFTB.ReadOnly = true;
        naCheckBox.Enabled = false;

        if (actionDetailTextBox.Text != "No Action Plan")
        {
            ddlActionStatus.Enabled = false;
            ddlFollowUpParty.Enabled = false;
            ddlActionOwner.Enabled = false;
            actionDetailTextBox.ReadOnly = true;
        }

        else
        {

        }
    }
    protected void actionHistoryButton_Click(object sender, EventArgs e)
    {
        string javascript = "<script type=\"text/javascript\">" +
                            "window.open('ActionHistory.aspx?riskId=" + EntityRiskId + "', 'OpenHistory', 'toolbar=0, height=500, width=800, resizable=1, scrollbars=1');" +
                            "window.focus();" +
                            "</script>\"";

        ClientScript.RegisterStartupScript(this.GetType(), "OpenHistory", javascript);
        
    }


    protected void severityTextBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (severityTextBox.SelectedIndex == 0)
            {
                riskScoreTextBox.Text = "";
            }

            if (frequencyTextBox.SelectedValue == "1")
            {
                if (severityTextBox.SelectedValue == "4")
                {
                    riskScoreTextBox.Text = "CATASTROPHE";
                }
            }
            if (frequencyTextBox.SelectedValue == "2")
            {
                if (severityTextBox.SelectedValue == "1")
                {
                    riskScoreTextBox.Text = "LOW";
                }
                else if (severityTextBox.SelectedValue == "2")
                {
                    riskScoreTextBox.Text = "MEDIUM";
                }
                else if (severityTextBox.SelectedValue == "3")
                {
                    riskScoreTextBox.Text = "HIGH";
                }
            }
            if (frequencyTextBox.SelectedValue == "3")
            {
                if (severityTextBox.SelectedValue == "1")
                {
                    riskScoreTextBox.Text = "LOW";
                }
                else if (severityTextBox.SelectedValue == "2")
                {
                    riskScoreTextBox.Text = "MEDIUM";
                }

            }
            if (frequencyTextBox.SelectedValue == "4")
            {
                if (severityTextBox.SelectedValue == "1")
                {
                    riskScoreTextBox.Text = "LOW";
                }
            }

            SetAdjustmentValues();
            SetScoreColors();
        }
        catch (Exception ex)
        {

        }
    }

    private void SetAdjustmentValues()
    {
        residualAdjustmentDDL.Items.Clear();
        residualScoreText.Text = "";

        string riskScore = riskScoreTextBox.Text;

        if (riskScore == "LOW")
        {
            residualAdjustmentDDL.Items.Add(new ListItem("0","0"));
            residualAdjustmentDDL.SelectedIndex = 0;
            residualScoreText.Text = "LOW";
        }

        if (riskScore == "MEDIUM")
        {
            residualAdjustmentDDL.Items.Add(new ListItem("0", "0"));
            residualAdjustmentDDL.Items.Add(new ListItem("-1", "-1"));
        }

        if (riskScore == "HIGH")
        {
            residualAdjustmentDDL.Items.Add(new ListItem("0", "0"));
            residualAdjustmentDDL.Items.Add(new ListItem("-1", "-1"));
            residualAdjustmentDDL.Items.Add(new ListItem("-2", "-2"));
        }

        if (riskScore == "CATASTROPHE")
        {
            residualAdjustmentDDL.Items.Add(new ListItem("0", "0"));
            residualAdjustmentDDL.SelectedIndex = 0;
            residualScoreText.Text = "CATASTROPHE";
        }
    }

    private void SetScoreColors()
    {
        string riskScore = riskScoreTextBox.Text;
        string residualScore = residualScoreText.Text;

        if (riskScore != "")
        {
            if (riskScore == "LOW")
                riskScoreTextBox.ForeColor = System.Drawing.Color.Green;
            else if (riskScore == "MEDIUM")
                riskScoreTextBox.ForeColor = System.Drawing.Color.Orange;
            else if (riskScore == "HIGH")
                riskScoreTextBox.ForeColor = System.Drawing.Color.DarkOrange;
            else if (riskScore == "CATASTROPHE")
                riskScoreTextBox.ForeColor = System.Drawing.Color.Red;
            else
                riskScoreTextBox.ForeColor = System.Drawing.Color.Black;
        }

        if (residualScore != "")
        {
            if (residualScore == "LOW")
                residualScoreText.ForeColor = System.Drawing.Color.Green;
            else if (residualScore == "MEDIUM")
                residualScoreText.ForeColor = System.Drawing.Color.Orange;
            else if (residualScore == "HIGH")
                residualScoreText.ForeColor = System.Drawing.Color.DarkOrange;
            else if (residualScore == "CATASTROPHE")
                residualScoreText.ForeColor = System.Drawing.Color.Red;
            else
                residualScoreText.ForeColor = System.Drawing.Color.Black;
        }
    }
    protected void naCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (naCheckBox.Checked == true)
        {
            riskScoreTextBox.Text = "";
            residualScoreText.Text = "";
            frequencyTextBox.Items.Clear();
            severityTextBox.Items.Clear();
            residualAdjustmentDDL.Items.Clear();

            ListItem naListItem = new ListItem("N/A", "0");
            ListItem residualListItem = new ListItem("N/A", "-1000");

            frequencyTextBox.Items.Add(naListItem);
            frequencyTextBox.Enabled = false;
            severityTextBox.Items.Add(naListItem);
            severityTextBox.Enabled = false;
            residualAdjustmentDDL.Items.Add(residualListItem);
            residualAdjustmentDDL.Enabled = false;

            riskScoreTextBox.Text = "N/A";
            residualScoreText.Text = "N/A";

            SetScoreColors();
        }
        else
        {
            frequencyTextBox.Items.Clear();
            severityTextBox.Items.Clear();
            residualAdjustmentDDL.Items.Clear();

            riskScoreTextBox.Text = "";
            residualScoreText.Text = "";

            frequencyTextBox.Enabled = true;
            severityTextBox.Enabled = true;
            residualAdjustmentDDL.Enabled = true;

            frequencyTextBox.Items.Add(new ListItem("--Select--", "--Select--"));
            frequencyTextBox.Items.Add(new ListItem("1 - Remote", "1"));
            frequencyTextBox.Items.Add(new ListItem("2 - Occasional", "2"));
            frequencyTextBox.Items.Add(new ListItem("3 - Probable", "3")); 
            frequencyTextBox.Items.Add(new ListItem("4 - Frequent", "4"));

            SetScoreColors();
        }
    }
    protected void historyLinkBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RiskHistory.aspx?entityRiskId=" + EntityRiskId);
    }

    private bool verifyChange(EntityRiskDetail risk1, EntityRiskDetail risk2)
    {
        bool change = false;

        string a = risk1.ToString();
        string b = risk2.ToString();

        if (!risk2.Frequency.Equals(risk1.Frequency))
            change = true;
        if (!risk2.InherentRiskScore.Equals(risk1.InherentRiskScore))
            change = true;
        if (!risk2.MitigatingActionsDetail.Equals(risk1.MitigatingActionsDetail))
            change = true;
        if (!risk2.ModifiedUserID.Equals(risk1.ModifiedUserID))
            change = true;
        if (!risk2.OversightPartyId.Equals(risk1.OversightPartyId))
            change = true;
        if (!risk2.PrimaryOwnerId.Equals(risk1.PrimaryOwnerId))
            change = true;
        if (!risk2.ResidualAdjustment.Equals(risk1.ResidualAdjustment))
            change = true;
        if (!risk2.ResidualRiskScore.Equals(risk1.ResidualRiskScore))
            change = true;
        if (!risk2.Severity.Equals(risk1.Severity))
            change = true;

        return change;
    }
}