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
    public class ProductController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public ProductController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<Product> GetAllProduct()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM Product";
            List<Product> products = GetProductsFromDatabase(strConn, sSQL);
      
            conn.Close();
            return products;

        }

        public static Product GetProductByID(String id)
        {
            String sSQL = "SELECT * FROM Product;";

            List<Product> products = GetProductsFromDatabase(strConn, sSQL);
            Product foundItem = products.FirstOrDefault(item => item.ProductId == id);


            return foundItem;
        }

        public static void UpdateProduct(String id, Product updateProduct)
        {
            String sSQL = "UPDATE Product SET ProductName = @ProductName, ProductDescription = @ProductDescription, ProductCategory = @ProductCategory, ProductRating  = @ProductRating, " +
                "Price  = @Price, QuantityAvailable = @QuantityAvailable WHERE ProductNumber = @ProductNumber";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@ProductNumber", id));
            cmd.Parameters.Add(new SqlParameter("@ProductName", updateProduct.ProductName));
            cmd.Parameters.Add(new SqlParameter("@ProductDescription", updateProduct.ProductDescription));
            cmd.Parameters.Add(new SqlParameter("@ProductCategory", updateProduct.ProductCategory));
            cmd.Parameters.Add(new SqlParameter("@ProductRating", updateProduct.ProductRating));
            cmd.Parameters.Add(new SqlParameter("@Price", updateProduct.ProductPrice));
            cmd.Parameters.Add(new SqlParameter("@QuantityAvailable", updateProduct.quantityAvailable));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateProduct(Product product)
        {
           
            String sSQL = "INSERT INTO Product (ProductNumber, ProductName, ProductDescription, ProductCategory, ProductRating, Price, QuantityAvailable) " +
                "VALUES (@ProductNumber, @ProductName, @ProductDescription, @ProductCategory,  @ProductRating, @Price, @QuantityAvailable)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@ProductNumber", product.ProductId));
            cmd.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
            cmd.Parameters.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            cmd.Parameters.Add(new SqlParameter("@ProductCategory", product.ProductCategory));
            cmd.Parameters.Add(new SqlParameter("@ProductRating", product.ProductRating));
            cmd.Parameters.Add(new SqlParameter("@Price", product.ProductPrice));
            cmd.Parameters.Add(new SqlParameter("@QuantityAvailable", product.quantityAvailable));
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

        static List<Product> GetProductsFromDatabase(string connectionString, string sSQL)
        {
            List<Product> result = new List<Product>();

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
                        string productID = row["ProductId"].ToString();
                        List<IngredientPerProduct> ingredientList = IngredientController.GetIngredientsByProduct(productID);

                        Product product = new Product
                        {
                            ProductId = row["ProductId"].ToString(),
                            ProductDescription = row["ProductDescription"].ToString(),
                            ProductPrice = Convert.ToDouble(row["ProductPrice"]),
                            ProductRating = Convert.ToDouble(row["ProductRating"]),
                            ProductCategory = row["ProductCategory"].ToString(),
                            ProductName = row["ProductName"].ToString(),
                            quantityAvailable = Convert.ToInt32(row["quantityAvailable"]),
                            ingredients = ingredientList,
                        };

                        result.Add(product);
                    }
                }
            }

            return result;
        }
    }
}
