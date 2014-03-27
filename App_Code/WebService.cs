using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using DataAccess.Data;
using DataAccess.DataUtility;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod()]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod()]
    public EntityRiskLogDetail GetEntityRiskHistory(int entityRiskLogId)
    {
        //SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["RiskRegister"].ConnectionString);
        //SqlCommand cmd = new SqlCommand("[dbo].[Entity_Risk_LogSelect]", con);
        //cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //cmd.Parameters.Add(new SqlParameter("@Entity_Risk_Log_Id", SqlDbType.Int, 4));
        //cmd.Parameters["@Entity_Risk_Log_Id"].Value = entityRiskLogId;

        EntityRiskLogDetail log = new EntityRiskLogDetail();
        EntityRiskLog logGet = new EntityRiskLog();

        try
        {
            log = logGet.SelectEntityRiskLog(entityRiskLogId);
        }
        catch (SqlException err)
        {
            // Mask errors.
            throw new ApplicationException("Data error.");
        }

        return log;
    }

    //[WebMethod()]
    //public EntityRiskLogDetail SelectEntityRiskLog(int entityRiskLogId)
    //{
    //    string connString = "Data Source=Antdawg\\SQLExpress;Initial Catalog=RiskRegister;Integrated Security=True";
    //    SqlConnection connection = new SqlConnection(connString);
    //    SqlCommand command = new SqlCommand("[dbo].[Entity_Risk_LogSelect]", connection);
    //    command.CommandType = CommandType.StoredProcedure;
    //    command.Parameters.Add(new SqlParameter("@Entity_Risk_Log_Id", SqlDbType.Int, 8));
    //    command.Parameters["@Entity_Risk_Log_Id"].Value = entityRiskLogId;

    //    EntityRiskLogDetail entityRiskLog = new EntityRiskLogDetail();

    //    try
    //    {
    //        connection.Open();
    //        SqlDataReader reader = command.ExecuteReader();

    //        while (reader.Read())
    //        {
    //            entityRiskLog.EntityRiskLogId = int.Parse(reader["Entity_Risk_Log_Id"].ToString());
    //            //entityRiskLog.EntityRiskId = reader["Entity_Risk_Id"];
    //            entityRiskLog.DateModified = DateTime.Parse(reader["Date_Modified"].ToString());
    //            entityRiskLog.ModifiedUser = reader["Modified_User_Id"].ToString();
    //            entityRiskLog.PrimaryOwnerId = (int)reader["Primary_Owner_Id"];
    //            entityRiskLog.OversightPartyId = (int)reader["Oversight_Party_Id"];
    //            entityRiskLog.Frequency = reader["Frequency"].ToString();
    //            entityRiskLog.Severity = reader["Severity"].ToString();
    //            entityRiskLog.InherentRiskScore = reader["Inherent_Risk_Score"].ToString();
    //            entityRiskLog.MitigatingControls = reader["Mitigating_Controls"].ToString();
    //            entityRiskLog.Adjustment = reader["Adjustment"].ToString();
    //            entityRiskLog.ResidualRiskScore = reader["Residual_Risk_Score"].ToString();
    //        }

    //        reader.Close();

    //        return entityRiskLog;
    //    }
    //    catch (SqlException ex)
    //    {
    //        throw new ApplicationException("Data error");
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    //return fillDataSet(command, "actionPlan");

    //}

    [WebMethod()]
    public DataTable SelectEntityRiskLog(int entityRiskLogId)
    {

        EntityRiskLog riskLog = new EntityRiskLog();

        DataTable toReturn = riskLog.SelectEntityRiskLogTable(entityRiskLogId);

        return toReturn;

    }
}

//    public partial class EntityRiskLogDetail
//    {
//        public string primaryOwnerId;

//        public EntityRiskLogDetail(string primaryOwnerId)
//        {
//            this.primaryOwnerId = primaryOwnerId;
//        }

//        //public int EntityRiskLogId
//        //{
//        //    get;
//        //    set;
//        //}

//        //public int EntityRiskId
//        //{
//        //    get;
//        //    set;
//        //}  

//        //public DateTime DateModified
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string ModifiedUser
//        //{
//        //    get;
//        //    set;
//        //}

//        //public int PrimaryOwnerId
//        //{
//        //    get;
//        //    set;
//        //}

//        //public int OversightPartyId
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string Frequency
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string Severity
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string InherentRiskScore
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string MitigatingControls
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string Adjustment
//        //{
//        //    get;
//        //    set;
//        //}

//        //public string ResidualRiskScore
//        //{
//        //    get;
//        //    set;
//        //}

//    }
    
//}
