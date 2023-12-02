using StufoodSystem_ADMIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                            String orderID = reader["orderNumber"].ToString();

                            Employee employee = EmployeeController.GetEmployeeByID(employeeID);
                            AffiliatedSchool school = AffiliatedSchoolController.GetAffliatedSchoolByID(schoolID);
                            List<OrderDetail> orderDetails = GetOrderDetailByOrderId(orderID);

                            Order order = new Order
                            {
                                orderNumber = orderID,
                                dateOrdered = Convert.ToDateTime(reader["DateOrdered"]),
                                dateReceived = Convert.ToDateTime(reader["DateReceived"]),
                                orderStatus = reader["orderStatus"].ToString(),
                                orderTotal = Convert.ToInt32(reader["orderTotal"]),

                                //Thêm các trường khác của bảng Order
                                affiliatedSchool = school,
                                employee = employee,
                                orderDetails = orderDetails,
                            };

                            
                            orders.Add(order);
                        }
                    }
                }
            }

            conn.Close();
            return orders;

        }

        public static List<Order> GetAllOrder()
        {
            String sSQL = "SELECT * FROM ORDER1;";

            List<Order> order = GetOrdersFromDatabase(strConn, sSQL);
            return order;
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

            String sSQL = "UPDATE Order1 SET OrderStatus = @OrderStatus, DateOrdered = @DateOrdered, DateReceived = @DateReceived , OrderTotal = @OrderTotal, EmployeeID = @EmployeeID, " +
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

        public static void UpdateOrderDetail(String id, OrderDetail updateOrderDetail)
        {
            String productID = updateOrderDetail.product.ProductId;

            String sSQL = "UPDATE OrderDetail SET ProductNumber = @ProductNumber, Quantity = @Quantity " +
                "WHERE OrderDetailID = @OrderDetailID";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderDetailID", id));
            cmd.Parameters.Add(new SqlParameter("@ProductNumber", updateOrderDetail.product.ProductId));
            cmd.Parameters.Add(new SqlParameter("@Quantity", updateOrderDetail.quantity));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateOrder(Order order)
        {
            String employeeIDString = order.employee.employeeID;
            String schoolID = order.affiliatedSchool.schoolId;

            String sSQL = "INSERT INTO Order1 (OrderNumber, OrderStatus, DateOrdered, DateReceived, OrderTotal, EmployeeID, SchoolID) " +
                "VALUES (@OrderNumber, @OrderStatus, @DateOrdered, @DateReceived,  @OrderTotal, @EmployeeID, @SchoolID)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderNumber", order.orderNumber));
            cmd.Parameters.Add(new SqlParameter("@OrderStatus", order.orderStatus));
            cmd.Parameters.Add(new SqlParameter("@DateOrdered", order.dateOrdered));
            cmd.Parameters.Add(new SqlParameter("@DateReceived", order.dateReceived));
            cmd.Parameters.Add(new SqlParameter("@OrderTotal", order.orderTotal));
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

        public static void CreateOrderDetail(OrderDetail orderDetail)
        {
            String productId = orderDetail.product.ProductId;

            String sSQL = "INSERT INTO OrderDetail (OrderDetailID, OrderNumber, ProductNumber, Quantity) " +
                "VALUES (@OrderDetailID, @OrderNumber, @ProductNumber, @Quantity)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderNumber", orderDetail.orderNumber));
            cmd.Parameters.Add(new SqlParameter("@ProductNumber", productId));
            cmd.Parameters.Add(new SqlParameter("@Quantity", orderDetail.quantity));
            cmd.Parameters.Add(new SqlParameter("@OrderDetailID", orderDetail.orderDetailNumber));
            
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

        static List<Order> GetOrdersFromDatabase(string connectionString, string sSQL)
        {
            List<Order> result = new List<Order>();

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
                        String schoolID = row["SchoolID"].ToString();

                        Employee employee = EmployeeController.GetEmployeeByID(employeeID);
                        AffiliatedSchool school = AffiliatedSchoolController.GetAffliatedSchoolByID(schoolID);


                        Order order = new Order
                        {
                            orderNumber = row["orderNumber"].ToString(),
                            dateOrdered = Convert.ToDateTime(row["DateOrdered"]),
                            dateReceived = Convert.ToDateTime(row["DateReceived"]),
                            orderStatus = row["orderStatus"].ToString(),
                            orderTotal = Convert.ToInt32(row["orderTotal"]),

                            //Thêm các trường khác của bảng Order
                            affiliatedSchool = school,
                            employee = employee,
                        };

                        result.Add(order);
                    }
                }
            }

            return result;
        }

        //----------------- ORDER DETAIL ZONE ------------------ //
        public static List<OrderDetail> GetOrderDetailByOrderId(String orderID)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();

                //Get order Detail normally
                string sqlQuery = "SELECT * FROM OrderDetail WHERE OrderNumber = @OrderNumber";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@OrderNumber", orderID);
                SqlDataReader reader = command.ExecuteReader();
                    
                while (reader.Read())
                {
                    String productID = reader["ProductNumber"].ToString();

                    Product product1 = ProductController.GetProductByID(productID);


                    OrderDetail orderDetail = new OrderDetail
                    {
                        orderDetailNumber = reader["OrderDetailID"].ToString(),
                        orderNumber = reader["OrderNumber"].ToString(),
                        quantity = Convert.ToInt32(reader["Quantity"]),
                        discountPercent = 0,
                        product = product1,

                    };



                    orderDetails.Add(orderDetail);
                }
                reader.Close();


                connection.Close();
            }

            return orderDetails;
        }
    }
}
