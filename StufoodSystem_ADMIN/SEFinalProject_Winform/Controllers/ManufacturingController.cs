using StufoodSystem_ADMIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StufoodSystem_ADMIN.Controllers
{
    public class ManufacturingController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public ManufacturingController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<ManufacturingRecord> GetAllManufacturingRecord()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM Manufacturing";
            List<ManufacturingRecord> schools = GetManufacturingRecordsFromDatabase(strConn, sSQL);
      
            conn.Close();
            return schools;

        }

        public static ManufacturingRecord GetManufacturingRecordByID(String id)
        {
            String sSQL = "SELECT * FROM Manufacturing;";

            List<ManufacturingRecord> records = GetManufacturingRecordsFromDatabase(strConn, sSQL);
            ManufacturingRecord foundItem = records.FirstOrDefault(item => item.manufacturingID == id);


            return foundItem;
        }

        public static void UpdateManufacturingRecord(String id, ManufacturingRecord manufacturingRecord)
        {
            String sSQL = "UPDATE Manufacturing SET ManufacturingDate = @ManufacturingDate, ProductID = @ProductID, Quantity = @Quantity, SupervisoryStaffID = @SupervisoryStaffID, FactoryStatus = @FactoryStatus WHERE ManufacturingID = @ManufacturingID;";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@ManufacturingID", id));
            cmd.Parameters.Add(new SqlParameter("@ManufacturingDate", manufacturingRecord.manufacturingDate));
            cmd.Parameters.Add(new SqlParameter("@ProductID", manufacturingRecord.product.ProductId));
            cmd.Parameters.Add(new SqlParameter("@Quantity", manufacturingRecord.quantity));
            cmd.Parameters.Add(new SqlParameter("@SupervisoryStaffID", manufacturingRecord.SupervisoryStaff.employeeID));
            cmd.Parameters.Add(new SqlParameter("@FactoryStatus", manufacturingRecord.Status));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateManufacturingRecord(ManufacturingRecord manufacturingRecord)
        {

            String sSQL = "INSERT INTO Manufacturing (ManufacturingID, ManufacturingDate, ProductID, Quantity, SupervisoryStaffID, FactoryStatus)" +
                "VALUES (@ManufacturingID, @ManufacturingDate, @ProductID, @Quantity, @SupervisoryStaffID, @FactoryStatus);";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@ManufacturingID", manufacturingRecord.manufacturingID));
            cmd.Parameters.Add(new SqlParameter("@ManufacturingDate", manufacturingRecord.manufacturingDate));
            cmd.Parameters.Add(new SqlParameter("@ProductID", manufacturingRecord.product.ProductId));
            cmd.Parameters.Add(new SqlParameter("@Quantity", manufacturingRecord.quantity));
            cmd.Parameters.Add(new SqlParameter("@SupervisoryStaffID", manufacturingRecord.SupervisoryStaff.employeeID));
            cmd.Parameters.Add(new SqlParameter("@FactoryStatus", manufacturingRecord.Status));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        
        }
        //--------------- PRIVATE FUNCTION -------------------- //

        static List<ManufacturingRecord> GetManufacturingRecordsFromDatabase(string connectionString, string sSQL)
        {
            List<ManufacturingRecord> result = new List<ManufacturingRecord>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(sSQL, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert DataTable rows to List<Employee>
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String ProductID = row["ProductID"].ToString();
                        String SupervisoryStaffID = row["SupervisoryStaffID"].ToString();

                        Product product = ProductController.GetProductByID(ProductID);
                        Employee SupervisoryStaff = EmployeeController.GetEmployeeByID(SupervisoryStaffID);

                        ManufacturingRecord record = new ManufacturingRecord
                        {
                            manufacturingID = row["ManufacturingID"].ToString(),
                            manufacturingDate = Convert.ToDateTime(row["ManufacturingDate"]),
                            product = product,
                            quantity = Convert.ToInt32(row["Quantity"]),
                            SupervisoryStaff = SupervisoryStaff,
                            Status = row["FactoryStatus"].ToString(),
                        };

                        result.Add(record);
                    }
                }
            }

            return result;
        }
    }
}
