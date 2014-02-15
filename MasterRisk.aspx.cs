using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.DataUtility;
using DataAccess.Data;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System.Web.Configuration;
using System.Text;

public partial class MasterRisk : System.Web.UI.Page
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

    string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        setConnectionStrings(connectionString);

        if (!Page.IsPostBack)
        {      

            

                RiskId = int.Parse(Request.QueryString["riskId"]);
                bindOwners();
                bindRiskSource(13);
                bindRiskDetails();

           

        }
        else
        {

        }
        
    }

    private void setConnectionStrings(string connString)
    {
        string connectionString = connString;

        riskTypeDS.ConnectionString = connectionString;

    }

    private void bindRiskDetails()
    {
        //entities bind
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

        //risk details
        Risk risk = new Risk();
        DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();

        riskDetail = risk.SelectRisk(RiskId);

        if (riskDetail != null)
        {
            ddlRiskType.SelectedValue = riskDetail.RiskTypeID.ToString();
            bindRiskSource(riskDetail.RiskTypeID, riskDetail.RiskSourceID);
            primaryOwnerDD.SelectedValue = riskDetail.PrimaryOwnerID.ToString();
            oversightPartyDD.SelectedValue = riskDetail.OversightPartyID.ToString();
            textRiskScenario.Text = riskDetail.RiskScenario.ToString();
            dateTextBox.Text = riskDetail.RiskEnteredDate.ToShortDateString();
            enteredByLabel.Text = riskDetail.UserID.ToString();
            riskIdLabel.Text = riskDetail.RiskID.ToString();
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
          
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        bool entitySelected = false;

        foreach (ListItem item in entityCheckBoxList.Items)
        {
            if (item.Selected)
                entitySelected = true;
        }

        if (ddlRiskSource.SelectedIndex != 0 && primaryOwnerDD.SelectedIndex != 0 && oversightPartyDD.SelectedIndex != 0 && entitySelected == true && !textRiskScenario.Text.Equals(""))
        {
            try
            {
                DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();
                Risk risk = new Risk();

                riskDetail = risk.SelectRisk(RiskId);

                //UPDATE master risk record
                riskDetail.RiskTypeID = int.Parse(ddlRiskType.SelectedValue);
                riskDetail.RiskSourceID = int.Parse(ddlRiskSource.SelectedValue);
                riskDetail.RiskScenario = textRiskScenario.Text;
                riskDetail.ModifiedUserID = User.Identity.Name.ToString();
                riskDetail.RiskModifiedDate = DateTime.Now;
                riskDetail.PrimaryOwnerID = int.Parse(primaryOwnerDD.SelectedValue);
                riskDetail.OversightPartyID = int.Parse(oversightPartyDD.SelectedValue);

                risk.UpdateRisk(riskDetail);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Update SUCCESSFUL : ");

                //update EntityRisk table
                List<EntityRiskDetail> entityRiskDetail = new List<EntityRiskDetail>();
                DataAccess.Data.EntityRisk entityRisk = new DataAccess.Data.EntityRisk();
                entityRiskDetail = entityRisk.SelectEntityRisks(RiskId);

                EntityRisk thisEntityRisk = new EntityRisk();

                if (entityRiskDetail != null)
                {
                    foreach (ListItem item in entityCheckBoxList.Items)
                    {

                        if (item.Selected)
                        {
                            bool isPresent = false;

                            for (int i = 0; i < entityRiskDetail.Count; i++)
                            {
                                //already in there, need to update w/ any master risk updates
                                if (int.Parse(item.Value) == entityRiskDetail[i].EntityId)
                                {
                                    isPresent = true;
                                }
                            }

                            //insert new risk / entity_risk
                            if (isPresent == false)
                            {
                                stringBuilder.Append(item.Text.ToString() + " risk has been CREATED. ");

                                EntityRiskDetail newEntityRisk = new EntityRiskDetail();
                                newEntityRisk.EntityId = int.Parse(item.Value);
                                newEntityRisk.RiskId = RiskId;
                                newEntityRisk.ModifiedUserID = User.Identity.Name.ToString();

                                int newEntityRiskId = thisEntityRisk.InsertEntityRisk(newEntityRisk);

                            }
                            else 
                            {
                                //
                            }

                        }
                        else
                        {

                            for (int i = 0; i < entityRiskDetail.Count; i++)
                            {
                                if (int.Parse(item.Value) == entityRiskDetail[i].EntityId)
                                {
                                    entityRisk.DeleteEntityRisk(entityRiskDetail[i].EntityRiskId);

                                    DataAccess.ActionPlan actionPlan = new DataAccess.ActionPlan();
                                    DataAccess.Documents document = new DataAccess.Documents();

                                    thisEntityRisk.DeleteEntityRisk(entityRiskDetail[i].EntityRiskId);
                                    //document.DeleteRiskDocuments(entityRiskDetail[i].RiskId);
                                    actionPlan.DeleteActionPlanByEntityRiskId(entityRiskDetail[i].RiskId);

                                    stringBuilder.Append(item.Text.ToString() + " risk has been DELETED. ");
                                }
                            }

                        }

                    }
                }
                else
                {
                    //foreach (ListItem item in entityCheckBoxList.Items)
                    //{
                    //    EntityRiskDetail newEntityRisk = new EntityRiskDetail();
                    //    newEntityRisk.EntityId = int.Parse(item.Value);
                    //    newEntityRisk.MasterRiskID = MasterRiskId;
                    //    newEntityRisk.RiskId = entityRiskDetail[
                    //    entityRisk.InsertEntityRisk(newEntityRisk);
                    //}
                }


                riskInsertLabel.Text = "Risk " + riskDetail.RiskID + " has been SUCCESSFULLY updated!";
                riskInsertLabel.ForeColor = System.Drawing.Color.Green;
                riskInsertLabel.Font.Size = 12;

                updateButton.Visible = false;
                editButton.Visible = true;

                entityCheckBoxList.Enabled = false;
                ddlRiskSource.Enabled = false;
                ddlRiskType.Enabled = false;
                primaryOwnerDD.Enabled = false;
                oversightPartyDD.Enabled = false;

                string javascript = "<script type=\"text/javascript\">" +
                            "alert(\"" + stringBuilder.ToString() + "\");" +
                            "</script>\"";

                ClientScript.RegisterStartupScript(this.GetType(), "ScrollPage", javascript);

            }

            catch (Exception ex)
            {
                riskInsertLabel.Text = "Error!";
                riskInsertLabel.Font.Size = 12;
                riskInsertLabel.ForeColor = System.Drawing.Color.Red;
            }

        }
        else
        {
            riskInsertLabel.Text = "Invalid or Missing Data - Master Risk update NOT Saved!";
            riskInsertLabel.Font.Size = 12;
            riskInsertLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterDash.aspx");
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

            primaryOwnerDD.DataSource = primaryOwners;
            primaryOwnerDD.DataTextField = "Owner_Desc";
            primaryOwnerDD.DataValueField = "Owner_Id";
            primaryOwnerDD.DataBind();
            primaryOwnerDD.Items.Insert(0, "--Select--");

            DataTable owners2 = new DataTable();
            Owner sightOwner = new Owner();
            owners2 = sightOwner.SelectAllOwners();

            DataView oversightParties = new DataView(owners2);
            oversightParties.RowFilter = "Owner_Type_Id = 501";

            oversightPartyDD.DataSource = oversightParties;
            oversightPartyDD.DataTextField = "Owner_Desc";
            oversightPartyDD.DataValueField = "Owner_Id";
            oversightPartyDD.DataBind();
            oversightPartyDD.Items.Insert(0, "--Select--");


        }
        catch(Exception ex)
        {

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
        catch(Exception ex)
        {
            
        }

    }

    private void bindRiskSource(int thisRiskTypeId)
    {
        try
        {
            ddlRiskSource.Items.Clear();

            int riskTypeId = 0;
            riskTypeId = thisRiskTypeId;

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

    protected void ddlRiskType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRiskSource();
    }


    protected void editButton_Click(object sender, EventArgs e)
    {
        riskInsertLabel.Text = "";

        updateButton.Visible = true;
        editButton.Visible = false;

        entityCheckBoxList.Enabled = true;
        ddlRiskSource.Enabled = true;
        ddlRiskType.Enabled = true;
        primaryOwnerDD.Enabled = true;
        oversightPartyDD.Enabled = true;

    }
}