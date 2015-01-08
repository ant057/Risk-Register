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
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;
using System.IO;

public partial class Documents : System.Web.UI.Page
{

    public int RiskId
    {
        get
        {
//            return (int)ViewState["RiskId"];
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
        if (!Page.IsPostBack)
        {
            RiskId = int.Parse(Request.QueryString["riskId"]);
        }

        docsGridViewDS.ConnectionString = connectionString;
    }

    protected void uploadButton_Click(object sender, EventArgs e)
    {
        if (FileUpload.PostedFile != null)
        {
            // Read the file and convert it to Byte Array
            string filePath = FileUpload.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension

            switch (ext)
            {
                case ".doc":
                    contenttype = "application/vnd.ms-word";
                    break;
                case ".rtf":
                    contenttype = "application/vnd.ms-word";
                    break;
                case ".docx":
                    contenttype = "application/vnd.ms-word";
                    break;
                case ".xls":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".pdf":
                    contenttype = "application/pdf";
                    break;
                case ".msg":
                    contenttype = "application/vnd.ms-outlook";
                    break;
            }

            if (contenttype != String.Empty)
            {

                Stream fs = FileUpload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                int imageId = 0;
                string imageName = FileUpload.FileName.ToString();
                string imageDesc = contenttype;
                // byte[] data = FileUpload.FileBytes;
                string user;
                user = User.Identity.Name;
                imageId = UploadFile(RiskId, DateTime.Now, imageName, imageDesc, bytes, user);

                sucesssFailLabel.Text = "File uploaded to Risk ID " + RiskId.ToString() + " successfully";
                sucesssFailLabel.ForeColor = System.Drawing.Color.Green;

                Response.Redirect("~/Documents.aspx?riskId=" + RiskId);
            }
            else
            {
                sucesssFailLabel.Text = "File not uploaded - Must be Image/Word/PDF/Excel/Msg formats";
                sucesssFailLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private int UploadFile(int riskId, DateTime addedDate, string imageName, string imageDesc, Byte[] binary, string userId)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand("[dbo].[DocumentsInsert]", connection);
        command.CommandType = CommandType.StoredProcedure;

        try
        {
            command.Parameters.Add(new SqlParameter("@Risk_Id", SqlDbType.Int, 8));
            command.Parameters["@Risk_Id"].Value = riskId;
            command.Parameters.Add(new SqlParameter("@Added_Date", SqlDbType.DateTime));
            command.Parameters["@Added_Date"].Value = addedDate;
            command.Parameters.Add(new SqlParameter("@Image_Name", SqlDbType.NVarChar));
            command.Parameters["@Image_Name"].Value = imageName;
            command.Parameters.Add(new SqlParameter("@Image_Description", SqlDbType.NVarChar));
            command.Parameters["@Image_Description"].Value = imageDesc;
            command.Parameters.Add(new SqlParameter("@Image_Binary", SqlDbType.Image));
            command.Parameters["@Image_Binary"].Value = binary;
            command.Parameters.Add(new SqlParameter("@Uploaded_By", SqlDbType.NVarChar));
            command.Parameters["@Uploaded_By"].Value = userId;
            command.Parameters.Add(new SqlParameter("@Image_Id", SqlDbType.Int , 8));
            command.Parameters["@Image_Id"].Direction = ParameterDirection.Output;

            connection.Open();
            command.ExecuteNonQuery();
            return (int)command.Parameters["@Image_Id"].Value;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data error..", ex);
        }
        finally
        {
            connection.Close();  
        }
    }

    protected void documentsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

        // Get the ID for this request.
        string id = documentsGridView.SelectedDataKey[0].ToString();

        if (id == null) throw new ApplicationException("Must specify ID.");

        // Create a parameterized command for this record.
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("[dbo].[DocumentsOpenImage]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Image_ID", int.Parse(id));

        try
        {

            Response.Clear();
            Response.Charset = "";
            con.Open();
            SqlDataReader r = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            string contentType = string.Empty;

            int bufferSize = 100; // Size of the buffer.
            byte[] bytes = new byte[bufferSize]; // The buffer.
            long bytesRead; // The # of bytes read.
            long readFrom = 0; // The starting index.

            if (r.Read())
            {
                // Read the field 100 bytes at a time.
                do
                {
                    bytesRead = r.GetBytes(0, readFrom, bytes, 0, bufferSize);
                    Response.BinaryWrite(bytes);
                    readFrom += bufferSize;

                } while (bytesRead == bufferSize);

            }

            r.Close();

            Response.AddHeader("content-disposition", "attachment;filename=" + documentsGridView.Rows[documentsGridView.SelectedIndex].Cells[3].Text.ToString());
            Response.ContentType = documentsGridView.Rows[documentsGridView.SelectedIndex].Cells[4].Text.ToString();
           // Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
    
    }


    protected void deleteDocsButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataAccess.Documents document = new DataAccess.Documents();
            bool deleted = false;

            foreach (GridViewRow row in documentsGridView.Rows)
            {
                CheckBox chkBox = (CheckBox)row.Cells[6].Controls[1];

                if (chkBox.Checked)
                {
                    if (row.Cells[5].Text.ToString().Equals(User.Identity.Name.ToString()))
                    {
                        int imageId = int.Parse(row.Cells[1].Text.ToString());
                        document.DeleteDocument(imageId);
                        deleted = true;
                    }
                }

            }

            if (deleted)
                Response.Redirect("Documents.aspx?riskId=" + RiskId);
            else
            {
                sucesssFailLabel.Text = "You cannot delete an attachment that was uploaded by" +
                    " someone else";
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting document");
        }

    }
}