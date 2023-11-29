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
    public class IngredientController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public IngredientController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<Ingredient> GetAllIgredient()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM Ingredient";
            List<Ingredient> ingredients = GetIngredientsFromDatabase(strConn, sSQL);
      
            conn.Close();
            return ingredients;

        }

        public static Ingredient GetIngredientByID(String id)
        {
            String sSQL = "SELECT * FROM Ingredient;";

            List<Ingredient> ingredients = GetIngredientsFromDatabase(strConn, sSQL);
            Ingredient foundItem = ingredients.FirstOrDefault(item => item.ingredientID == id);


            return foundItem;
        }

        public static List<Ingredient> GetIngredientByIDs(String idsList)
        {
            String sSQL = "SELECT * FROM Ingredient;";

            List<Ingredient> ingredients = GetIngredientsFromDatabase(strConn, sSQL);
            List<Ingredient> result = new List<Ingredient>();
            // Split the motherString into an array of substrings
            string[] substrings = idsList.Split(',');
            foreach (String id in substrings)
            {
                Ingredient foundItem = ingredients.FirstOrDefault(item => item.ingredientID == id);
                result.Add(foundItem);
            }
            return result;
        }

        public static void UpdateIngredient (String id, Ingredient updateIngredient)
        {
            String sSQL = "UPDATE Ingredient SET IngredientID = @IngredientID, IngredientName = @IngredientName, IngredientDescription = @IngredientDescription, IngredientCategory = @IngredientCategory, IngredientPreservation = @IngredientPreservation, Price = @Price, QuantityAvailable = @QuantityAvailable";


            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@IngredientID", updateIngredient.ingredientID));
            cmd.Parameters.Add(new SqlParameter("@IngredientName", updateIngredient.ingredientName));
            cmd.Parameters.Add(new SqlParameter("@IngredientDescription", updateIngredient.ingredientDescription));
            cmd.Parameters.Add(new SqlParameter("@IngredientCategory", updateIngredient.ingredientCategory));
            cmd.Parameters.Add(new SqlParameter("@IngredientPreservation", updateIngredient.ingredientPreservation));
            cmd.Parameters.Add(new SqlParameter("@Price", updateIngredient.price));
            cmd.Parameters.Add(new SqlParameter("@QuantityAvailable", updateIngredient.quantityAvailable));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateIngredient(Ingredient ingredient)
        {

            String sSQL = "INSERT INTO Ingredient (IngredientID, IngredientName, IngredientDescription, IngredientCategory, IngredientPreservation, Price, QuantityAvailable)" +
                "VALUES (@IngredientID, @IngredientName, @IngredientDescription, @IngredientCategory, @IngredientPreservation, @Price, @QuantityAvailable);";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@IngredientID", ingredient.ingredientID));
            cmd.Parameters.Add(new SqlParameter("@IngredientName", ingredient.ingredientName));
            cmd.Parameters.Add(new SqlParameter("@IngredientDescription", ingredient.ingredientDescription));
            cmd.Parameters.Add(new SqlParameter("@IngredientCategory", ingredient.ingredientCategory));
            cmd.Parameters.Add(new SqlParameter("@IngredientPreservation", ingredient.ingredientPreservation));
            cmd.Parameters.Add(new SqlParameter("@Price", ingredient.price));
            cmd.Parameters.Add(new SqlParameter("@QuantityAvailable", ingredient.quantityAvailable));
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

        static List<Ingredient> GetIngredientsFromDatabase(string connectionString, string sSQL)
        {
            List<Ingredient> result = new List<Ingredient>();

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
                        Ingredient ingredient = new Ingredient
                        {
                            ingredientID = row["ingredientID"].ToString(),
                            ingredientCategory = row["ingredientCategory"].ToString(),
                            ingredientDescription = row["ingredientDescription"].ToString(),
                            ingredientName = row["ingredientName"].ToString(),
                            ingredientPreservation = row["ingredientPreservation"].ToString(),
                            quantityAvailable = Convert.ToInt32(row["quantityAvailable"]),
                            price = Convert.ToDouble(row["price"]),

                        };

                        result.Add(ingredient);
                    }
                }
            }

            return result;
        }
    }
}
