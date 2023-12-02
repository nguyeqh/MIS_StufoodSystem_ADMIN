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
    public class SupplierController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public SupplierController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<Supplier> GetAllSupplier()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM Suppliers;";
            List<Supplier> suppliers = GetSuppliersFromDatabase(strConn, sSQL);
      
            conn.Close();
            return suppliers;

        }

        public static Supplier GetSupplierByID(String id)
        {
            String sSQL = "SELECT * FROM Suppliers;";

            List<Supplier> suppliers = GetSuppliersFromDatabase(strConn, sSQL);
            Supplier foundItem = suppliers.FirstOrDefault(item => item.supplierId == id);


            return foundItem;
        }

        public static void UpdateSupplier(String id, Supplier updateSupplier)
        {
            String ingredientProvidedsString = "";
            int count = 0;
            foreach (Ingredient ingredient in updateSupplier.ingredientProvided)
            {
                ingredientProvidedsString += ingredient.ingredientID;
                count++;
                if (count < updateSupplier.ingredientProvided.Count)
                {
                    ingredientProvidedsString += ",";
                }
            }

            String sSQL = "UPDATE Suppliers SET SupplierName  = @SupplierName , ADDRESS = @Address, Phone  = @Phone, IngredientProvided = @IngredientProvided, " +
                "EMAIL = @Email, Rate = @Rate WHERE SupplierID = @SupplierID";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@SupplierID", id));
            cmd.Parameters.Add(new SqlParameter("@SupplierName ", updateSupplier.supplierName));
            cmd.Parameters.Add(new SqlParameter("@Phone", updateSupplier.Phone));
            cmd.Parameters.Add(new SqlParameter("@Address", updateSupplier.Address));
            cmd.Parameters.Add(new SqlParameter("@Email", updateSupplier.Email));
            cmd.Parameters.Add(new SqlParameter("@Rate", updateSupplier.rate));
            cmd.Parameters.Add(new SqlParameter("@IngredientProvided", ingredientProvidedsString));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateSupplier(Supplier supplier)
        {
            String ingredientProvidedsString = "";
            int count = 0;
            foreach (Ingredient ingredient in supplier.ingredientProvided)
            {
                ingredientProvidedsString += ingredient.ingredientID;
                count++;
                if (count < supplier.ingredientProvided.Count)
                {
                    ingredientProvidedsString += ",";
                }
            }

            String sSQL = "INSERT INTO Suppliers (SupplierID, SupplierName, PHONE, ADDRESS, Rate, IngredientProvided, EMAIL) " +
                "VALUES (@SupplierID, @SupplierName, @Phone, @Address,  @Rate, @IngredientProvided, @Email)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@SupplierID", supplier.supplierId));
            cmd.Parameters.Add(new SqlParameter("@SupplierName ", supplier.supplierName));
            cmd.Parameters.Add(new SqlParameter("@Phone", supplier.Phone));
            cmd.Parameters.Add(new SqlParameter("@Address", supplier.Address));
            cmd.Parameters.Add(new SqlParameter("@Email", supplier.Email));
            cmd.Parameters.Add(new SqlParameter("@Rate", supplier.rate));
            cmd.Parameters.Add(new SqlParameter("@IngredientProvided", ingredientProvidedsString));
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

        static List<Supplier> GetSuppliersFromDatabase(string connectionString, string sSQL)
        {
            List<Supplier> result = new List<Supplier>();

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
                        String ingredientProvided = row["ingredientProvided"].ToString();
                        List<Ingredient> supplierIngredients = IngredientController.GetIngredientByIDs(ingredientProvided);
                        
                        Supplier supplier = new Supplier
                        {
                            supplierId = row["supplierId"].ToString(),
                            Address = row["Address"].ToString(),
                            Email = row["Email"].ToString(),
                            Phone = row["Phone"].ToString(),
                            rate = Convert.ToDouble(row["rate"]),
                            supplierName = row["supplierName"].ToString(),
                            ingredientProvided = supplierIngredients,
                           
                        };

                        result.Add(supplier);
                    }
                }
            }

            return result;
        }
    }
}
