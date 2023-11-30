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
    public class AffiliatedSchoolController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public AffiliatedSchoolController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<AffiliatedSchool> GetAllSchool()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM AFFILIATEDSCHOOL";
            List<AffiliatedSchool> schools = GetSchoolsFromDatabase(strConn, sSQL);
      
            conn.Close();
            return schools;

        }

        public static AffiliatedSchool GetAffliatedSchoolByID(String id)
        {
            String sSQL = "SELECT * FROM AFFILIATEDSCHOOL;";

            List<AffiliatedSchool> schools = GetSchoolsFromDatabase(strConn, sSQL);
            AffiliatedSchool foundItem = schools.FirstOrDefault(item => item.schoolId == id);


            return foundItem;
        }

        public static void UpdateAffiliatedSchool(String id, AffiliatedSchool updateSchool)
        {
            String sSQL = "UPDATE AffiliatedSchool SET SchoolName = @SchoolName, Phone = @Phone, Email = @Email, Address = @Address, NumberOfStudents = @NumberOfStudents WHERE SchoolID = @SchoolID;";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@SchoolId", id));
            cmd.Parameters.Add(new SqlParameter("@SchoolName", updateSchool.schoolName));
            cmd.Parameters.Add(new SqlParameter("@Phone", updateSchool.schoolPhone));
            cmd.Parameters.Add(new SqlParameter("@Address", updateSchool.schoolAddress));
            cmd.Parameters.Add(new SqlParameter("@Email", updateSchool.email));
            cmd.Parameters.Add(new SqlParameter("@NumberOfStudents", updateSchool.numberOfStudents));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateAffiliatedSchool(AffiliatedSchool school)
        {

            String sSQL = "INSERT INTO AffiliatedSchool (SchoolID, SchoolName, Phone, Email, Address, NumberOfStudents)" +
                "VALUES (@SchoolId, @SchoolName, @Phone, @Email, @Address, @NumberOfStudents);";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn); 
            cmd.Parameters.Add(new SqlParameter("@SchoolId", school.schoolId));
            cmd.Parameters.Add(new SqlParameter("@SchoolName", school.schoolName));
            cmd.Parameters.Add(new SqlParameter("@Phone", school.schoolPhone));
            cmd.Parameters.Add(new SqlParameter("@Address", school.schoolAddress));
            cmd.Parameters.Add(new SqlParameter("@Email", school.email));
            cmd.Parameters.Add(new SqlParameter("@NumberOfStudents", school.numberOfStudents));
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

        static List<AffiliatedSchool> GetSchoolsFromDatabase(string connectionString, string sSQL)
        {
            List<AffiliatedSchool> result = new List<AffiliatedSchool>();

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
                        AffiliatedSchool school = new AffiliatedSchool
                        {
                            schoolId = row["schoolId"].ToString(),
                            schoolAddress = row["Address"].ToString(),
                            schoolName = row["schoolName"].ToString(),
                            schoolPhone = row["Phone"].ToString(),
                            email = row["Email"].ToString(),
                            numberOfStudents = Convert.ToInt32(row["numberOfStudents"]),
                        };

                        result.Add(school);
                    }
                }
            }

            return result;
        }
    }
}
