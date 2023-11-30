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
    public class PurchaseController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public PurchaseController() 
        {
            conn = new SqlConnection(strConn);
            
        }

        public static List<Purchase> GetAllPurchaseInMonth(String month, string year)
        {
            List<Purchase> purchases = new List<Purchase>();
            using (conn)
            {
                conn.Open();

                string sqlQuery = "SELECT * FROM Purchase WHERE MONTH(DatePurchase) = @Month AND YEAR(DatePurchase) = @Year";

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@Month", month);
                    command.Parameters.AddWithValue("@Year", year);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String employeeID = reader["employeeId"].ToString();
                            String supplierID = reader["SupplierID"].ToString();
                            String purchaseID = reader["PurchaseNumber"].ToString();

                            Employee employee = EmployeeController.GetEmployeeByID(employeeID);
                            Supplier supplier = SupplierController.GetSupplierByID(supplierID);
                            List<PurchaseDetail> purchaseDetails = GetPurchaseDetailByPurchaseId(purchaseID);

                            Purchase purchase = new Purchase
                            {
                                PurchaseNumber = purchaseID,
                                PurchaseStatus = reader["PurchaseStatus"].ToString(),
                                purchaseDate = Convert.ToDateTime(reader["DatePurchase"]),
                                receivedDate = Convert.ToDateTime(reader["DateReceived"]),
                                purchaseTotal = Convert.ToInt32(reader["PurchaseTotal"]),

                                //Thêm các trường khác của bảng Order
                                supplier = supplier,
                                employee = employee,
                                purchaseDetails = purchaseDetails,
                            };


                            purchases.Add(purchase);
                        }
                    }
                }
            }

            conn.Close();
            return purchases;

        }

        public static Purchase GetPurchaseByID(String id)
        {
            String sSQL = "SELECT * FROM Purchase;";

            List<Purchase> purchase = GetPurchasesFromDatabase(strConn, sSQL);
            Purchase foundItem = purchase.FirstOrDefault(item => item.PurchaseNumber == id);


            return foundItem;
        }

        public static void UpdatePurchase(String id, Purchase updatePurchase)
        {
            String employeeIDString = updatePurchase.employee.employeeID;
            String supplierID = updatePurchase.supplier.supplierId;

            String sSQL = "UPDATE Purchase SET PurchaseStatus = @PurchaseStatus, DatePurchase = @DatePurchase, DateReceived = @DateReceived , PurchaseTotal = @PurchaseTotal, EmployeeID = @EmployeeID, " +
                "SupplierID = @SupplierID WHERE PurchaseNumber = @PurchaseNumber ";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@PurchaseNumber", id));
            cmd.Parameters.Add(new SqlParameter("@PurchaseStatus", updatePurchase.PurchaseStatus));
            cmd.Parameters.Add(new SqlParameter("@DatePurchase", updatePurchase.purchaseDate));
            cmd.Parameters.Add(new SqlParameter("@DateReceived", updatePurchase.receivedDate));
            cmd.Parameters.Add(new SqlParameter("@PurchaseTotal", updatePurchase.purchaseTotal));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeIDString));
            cmd.Parameters.Add(new SqlParameter("@SupplierID", supplierID));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreatePurchase(Purchase purchase)
        {
            String employeeIDString = purchase.employee.employeeID;
            String supplierID = purchase.supplier.supplierId;

            String sSQL = "INSERT INTO Purchase (PurchaseNumber, PurchaseStatus, DatePurchase, DateReceived, PurchaseTotal,EmployeeID, SupplierID) " +
                "VALUES (@PurchaseNumber, @PurchaseStatus, @DatePurchase, @DateReceived,  @PurchaseTotal, @EmployeeID, @SupplierID)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@PurchaseStatus", purchase.PurchaseStatus));
            cmd.Parameters.Add(new SqlParameter("@DatePurchase", purchase.purchaseDate));
            cmd.Parameters.Add(new SqlParameter("@DateReceived", purchase.receivedDate));
            cmd.Parameters.Add(new SqlParameter("@PurchaseTotal", purchase.purchaseTotal));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeIDString));
            cmd.Parameters.Add(new SqlParameter("@SupplierID", supplierID));
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

        static List<Purchase> GetPurchasesFromDatabase(string connectionString, string sSQL)
        {
            List<Purchase> result = new List<Purchase>();

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
                        String employeeID = row["employeeId"].ToString();
                        String supplierID = row["SupplierID"].ToString();
                        String purchaseID = row["PurchaseNumber"].ToString();

                        Employee employee = EmployeeController.GetEmployeeByID(employeeID);
                        Supplier supplier = SupplierController.GetSupplierByID(supplierID);
                        List<PurchaseDetail> purchaseDetails = GetPurchaseDetailByPurchaseId(purchaseID);

                        Purchase purchase = new Purchase
                        {
                            PurchaseNumber = purchaseID,
                            PurchaseStatus = row["PurchaseStatus"].ToString(),
                            purchaseDate = Convert.ToDateTime(row["DatePurchase"]),
                            receivedDate = Convert.ToDateTime(row["DateReceived"]),
                            purchaseTotal = Convert.ToInt32(row["PurchaseTotal"]),

                            //Thêm các trường khác của bảng Order
                            supplier = supplier,
                            employee = employee,
                            purchaseDetails = purchaseDetails,
                        };


                        result.Add(purchase);
                    }
                }
            }

            return result;
        }

        //----------------- ORDER DETAIL ZONE ------------------ //
        public static List<PurchaseDetail> GetPurchaseDetailByPurchaseId(String purchaseID)
        {
            List<PurchaseDetail> purchaseDetails = new List<PurchaseDetail>();
            using (conn)
            {
                conn.Open();

                //Get order Detail normally
                string sqlQuery = "SELECT * FROM PurchaseDetail WHERE PurchaserNumber = @PurchaseNumber";
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@PurchaseNumber", purchaseID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String ingredientID = reader["IngredientID"].ToString();

                            Ingredient ingredient1 = IngredientController.GetIngredientByID(ingredientID);


                            PurchaseDetail purchaseDetail = new PurchaseDetail
                            {
                                purchaseDetailNumber = reader["PurchaseDetailNumber"].ToString(),
                                purchaseID = reader["PurchaseNumber"].ToString(),
                                quantity = Convert.ToInt32(reader["Quantity"]),
                                ingredient = ingredient1,

                            };



                            purchaseDetails.Add(purchaseDetail);
                        }
                    }
                }

              
            }

            conn.Close();


            return purchaseDetails;
        }
    }
}
