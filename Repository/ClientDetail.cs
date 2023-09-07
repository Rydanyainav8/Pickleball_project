using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace Pickleball_project.Repository
{
    public class ClientDetail : IClientRepository
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        public ClientDetail(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public DataTable ClientDataTable(string path)
        {
            var conStr = configuration.GetConnectionString("excelconnection");
            DataTable dt = new DataTable();
            conStr = string.Format(conStr, path);
            using (OleDbConnection excelconn = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(conStr))
                {
                    using (OleDbDataAdapter adapterexcel = new OleDbDataAdapter())
                    {

                        excelconn.Open();
                        cmd.Connection = excelconn;
                        DataTable excelschema;
                        excelschema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var sheetname = excelschema.Rows[0]["Table_name"].ToString();
                        excelconn.Close();

                        excelconn.Open();
                        cmd.CommandText = "Select * from [" + sheetname + "]";
                        adapterexcel.SelectCommand = cmd;
                        adapterexcel.Fill(dt);
                        excelconn.Close();
                    }
                }
            }
            return dt;
        }
        public string DocumentUpload(IFormFile formFile)
        {
            string uploadPath = webHostEnvironment.WebRootPath;
            string dest_path = Path.Combine(uploadPath, "uploaded_doc");
            if(!Directory.Exists(dest_path)) 
            { 
                Directory.CreateDirectory(dest_path);
            }
            string sourceFile = Path.GetFileName(formFile.FileName);
            string path = Path.Combine(dest_path, sourceFile);
            using(FileStream fileStream = new FileStream(path, FileMode.Create)) 
            {
                formFile.CopyTo(fileStream);
            }
            return path;
        }
        public void ImportClient(DataTable client)
        {
            var sqlconn = configuration.GetConnectionString("sqlconnection");
            using (SqlConnection scon = new SqlConnection(sqlconn)) 
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(scon))
                {
                    sqlBulkCopy.DestinationTableName = "Clients";
                    sqlBulkCopy.ColumnMappings.Add("Firstname", "Firstname");
                    sqlBulkCopy.ColumnMappings.Add("Lastname", "Lastname");
                    sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                    sqlBulkCopy.ColumnMappings.Add("Birthdate", "Birthdate");
                    sqlBulkCopy.ColumnMappings.Add("Email", "Email");
                    sqlBulkCopy.ColumnMappings.Add("Adress", "Adress");
                    sqlBulkCopy.ColumnMappings.Add("City", "City");
                    sqlBulkCopy.ColumnMappings.Add("Zip", "Zip");
                    sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                    sqlBulkCopy.ColumnMappings.Add("PhoneNumber", "PhoneNumber");
                    sqlBulkCopy.ColumnMappings.Add("FirstNameEmergencyContact", "FirstNameEmergencyContact");
                    sqlBulkCopy.ColumnMappings.Add("LastNameEmergencyContact", "LastNameEmergencyContact");
                    sqlBulkCopy.ColumnMappings.Add("EmergencyPhone", "EmergencyPhone");
                    sqlBulkCopy.ColumnMappings.Add("PlayedCategory", "PlayedCategory");
                    sqlBulkCopy.ColumnMappings.Add("Group", "Group");
                    scon.Open();
                    sqlBulkCopy.WriteToServer(client);
                    scon.Close();

                }
            }
        }
    }
}
