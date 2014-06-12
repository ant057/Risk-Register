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

public partial class RiskAssessment : System.Web.UI.Page
{

    string connectionString = WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        setConnectionStrings(connectionString);

        if (!Page.IsPostBack)
        {      
            bindOwners();
            bindRiskSource(13);
        }
        else
        {

        }
        
    }

    private void setConnectionStrings(string connString)
    {
        string connectionString = connString;

        entitiesDS.ConnectionString = connectionString;
        riskTypeDS.ConnectionString = connectionString;

    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        riskInsertLabel.Text = "";

        if (isGroupSelected())
        {

            if (ddlRiskSource.SelectedIndex != 0 && primaryOwnerDD.SelectedIndex != 0 && oversightPartyDD.SelectedIndex != 0 && !textRiskScenario.Text.Equals(""))
            {
                try
                {
                    DataAccess.DataUtility.RiskDetail riskDetail = new DataAccess.DataUtility.RiskDetail();
                    Risk risk = new Risk();

                    //insert new master risk record
                    riskDetail.RiskTypeID = int.Parse(ddlRiskType.SelectedValue);
                    riskDetail.RiskSourceID = int.Parse(ddlRiskSource.SelectedValue);
                    riskDetail.RiskScenario = textRiskScenario.Text;
                    riskDetail.UserID = User.Identity.Name.ToString();
                    riskDetail.RiskEnteredDate = DateTime.Now;
                    riskDetail.ModifiedUserID = User.Identity.Name.ToString();
                    riskDetail.RiskModifiedDate = DateTime.Now;
                    riskDetail.PrimaryOwnerID = int.Parse(primaryOwnerDD.SelectedValue);
                    riskDetail.OversightPartyID = int.Parse(oversightPartyDD.SelectedValue);

                    int newRiskId = 0;
                    newRiskId = risk.InsertRisk(riskDetail);

                    int type = 1;

                    //insert entity risk
                    EntityRisk entityRisk = new EntityRisk();

                    foreach (ListItem ent in entityCheckBoxList.Items)
                    {
                        if (ent.Selected)
                        {
                            DataAccess.DataUtility.EntityRiskDetail entityRiskDetail = new DataAccess.DataUtility.EntityRiskDetail();
                            entityRiskDetail.EntityId = int.Parse(ent.Value);
                            entityRiskDetail.RiskId = newRiskId;
                            entityRiskDetail.ModifiedUserID = User.Identity.Name.ToString();
                            entityRisk.InsertEntityRisk(entityRiskDetail);
                        }
                    }

                    if (newRiskId != 0)
                    {
                        Response.Redirect("~/MasterDash.aspx?masterRiskId=" + newRiskId + "&type=" + type);
                    }
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
                riskInsertLabel.Text = "Invalid or Missing Data - Risk Assessment NOT Saved!";
                riskInsertLabel.Font.Size = 12;
                riskInsertLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            string javascript = "<script type=\"text/javascript\">" +
                            "alert(\"Argo Group must be an entity selection.\");" +
                            "</script>\"";

            ClientScript.RegisterStartupScript(this.GetType(), "ScrollPage", javascript);
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

    protected void ddlRiskType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRiskSource();
    }

   
    protected void selectAllButton_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in entityCheckBoxList.Items)
            item.Selected = true;
    }

    private bool isGroupSelected()
    {

        if (entityCheckBoxList.Items.FindByValue("43").Selected == true)
            return true;
        else
            return false;

    }

}