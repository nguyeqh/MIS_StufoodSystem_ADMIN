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
    public class OrderController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public OrderController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<Order> GetAllOrderInMonth(String month, string year)
        {
            List<Order> orders = new List<Order>();
            using (conn)
            {
                conn.Open();

                string sqlQuery = "SELECT * FROM Order1 WHERE MONTH(DateOrdered) = @Month AND YEAR(DateOrdered) = @Year";

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@Month", month);
                    command.Parameters.AddWithValue("@Year", year);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String employeeID = reader["employeeId"].ToString();
                            String schoolID = reader["SchoolID"].ToString();

                            Employee employee = EmployeeController.GetEmployeeByID(employeeID);
                            AffiliatedSchool school = AffiliatedSchoolController.GetAffliatedSchoolByID(schoolID);

                            Order order = new Order
                            {
                                orderNumber = reader["orderNumber"].ToString(),
                                dateOrdered = Convert.ToDateTime(reader["DateOrdered"]),
                                dateReceived = Convert.ToDateTime(reader["DateReceived"]),
                                orderStatus = reader["orderStatus"].ToString(),
                                orderTotal = Convert.ToInt32(reader["orderTotal"]),

                                //Thêm các trường khác của bảng Order
                                affiliatedSchool = school,
                                employee = employee,
                            };

                            

                            orders.Add(order);
                        }
                    }
                }
            }

            conn.Close();
            return orders;

        }

        public static Order GetOrderByID(String id)
        {
            String sSQL = "SELECT * FROM ORDER1;";

            List<Order> order = GetOrdersFromDatabase(strConn, sSQL);
            Order foundItem = order.FirstOrDefault(item => item.orderNumber == id);


            return foundItem;
        }

        public static void UpdateOrder(String id, Order updateOrder)
        {
            String employeeIDString = updateOrder.employee.employeeID;
            String schoolID = updateOrder.affiliatedSchool.schoolId;

            String sSQL = "UPDATE EMPLOYEE SET OrderStatus = @OrderStatus, DateOrdered = @DateOrdered, DateReceived = @DateReceived , OrderTotal = @OrderTotal, EmployeeID = @EmployeeID, " +
                "SchoolID = @SchoolID WHERE OrderNumber = @OrderNumber";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderNumber", id));
            cmd.Parameters.Add(new SqlParameter("@OrderStatus", updateOrder.orderStatus));
            cmd.Parameters.Add(new SqlParameter("@DateOrdered", updateOrder.dateOrdered));
            cmd.Parameters.Add(new SqlParameter("@DateReceived", updateOrder.dateReceived));
            cmd.Parameters.Add(new SqlParameter("@OrderTotal", updateOrder.orderTotal));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeIDString));
            cmd.Parameters.Add(new SqlParameter("@SchoolID", schoolID));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateEmployee(Employee employee)
        {
           
            String sSQL = "INSERT INTO EMPLOYEE (EMPOLYEEID, EMPLOYEENAME, PHONE, ADDRESS, JOB, POSITION, EMAIL, SALARY) " +
                "VALUES (@EmpID, @EmployeeName, @Phone, @Adress,  @Job, @Position, @Email, @Salary)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderNumber", updateOrder.orderNumber));
            cmd.Parameters.Add(new SqlParameter("@OrderStatus", updateOrder.orderStatus));
            cmd.Parameters.Add(new SqlParameter("@DateOrdered", updateOrder.dateOrdered));
            cmd.Parameters.Add(new SqlParameter("@DateReceived", updateOrder.dateReceived));
            cmd.Parameters.Add(new SqlParameter("@OrderTotal", updateOrder.orderTotal));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeIDString));
            cmd.Parameters.Add(new SqlParameter("@SchoolID", schoolID));
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

        static List<Employee> GetEmployeesFromDatabase(string connectionString, string sSQL)
        {
            List<Employee> result = new List<Employee>();

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
                        Employee employee = new Employee
                        {
                            employeeID = row["employeeID"].ToString(),
                            employeeName = row["employeeName"].ToString(),
                            phone = row["phone"].ToString(),
                            address = row["address"].ToString(),
                            job = row["job"].ToString(),
                            position = row["position"].ToString(),
                            email = row["email"].ToString(),
                            salary = Convert.ToDouble(row["salary"])
                        };

                        result.Add(employee);
                    }
                }
            }

            return result;
        }
    }
}
