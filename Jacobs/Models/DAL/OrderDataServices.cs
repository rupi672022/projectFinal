using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace System.Models.DAL
{
    public class OrderDataServices
    {

        public int Insert(Orders order) //insert new company
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand - check if the order in table [Order] 
                SqlCommand insertCommand = CreateInsertCommandCheck(order, con);


                // Execute the command
                SqlDataReader dataReader = insertCommand.ExecuteReader();


                if (dataReader.Read())//if yes insert product on order in table [ProductOnOrder]
                {
                    dataReader.Close();
                    SqlCommand insertCommandProduct = CreateInsertCommandProductOrder(order, con);
                    //E Execute
                    numEffected = insertCommandProduct.ExecuteNonQuery();
                }
                else//if not
                {
                    dataReader.Close();
                    //C Create the Insert SqlCommand insert order int table [Order]
                    SqlCommand insertCommandOrder = CreateInsertCommandOrder(order, con);
                    numEffected = insertCommandOrder.ExecuteNonQuery();

                    //insert compant on order in table[CompanyOnOrder]
                    SqlCommand insertCommandCompany = CreateInsertCommandCompanyOrder(order, con);
                    //E Execute
                    numEffected = insertCommandCompany.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {

                    // this code needs to write the error to a log file
                throw new Exception("Failed to insert a order", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


            // num effected
            return numEffected;


        }


        public List<Orders> Read(int idOrder)//get the product on order
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = CreateSelectCommandOrder(con, idOrder);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Orders> listOrder = new List<Orders>();

                while (dataReader.Read())//if user on table
                {
                    Orders order = new Orders();
                    order.OrderNum = Convert.ToInt32(dataReader["orderNum"]);
                    order.Productnum = Convert.ToInt32(dataReader["barcodNum"]);
                    order.Weight = (double)dataReader["weight"];
                    order.Quantity = Convert.ToInt32(dataReader["quantity"]);
                    order.Total = (double)dataReader["total"];

                    listOrder.Add(order);
                }

                dataReader.Close();

                return listOrder;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of order", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Orders> ReadgetImage(int idOrder)//get the imant in this order 
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = CreateSelectCommandOrderImage(con, idOrder);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Orders> listOrder = new List<Orders>();

                while (dataReader.Read())//if user on table
                {
                    Orders order = new Orders();
                    order.OrderNum = Convert.ToInt32(dataReader["orderNum"]);
                    order.Productnum = Convert.ToInt32(dataReader["barcodNum"]);
                    order.Weight = (double)dataReader["weight"];
                    order.Quantity = Convert.ToInt32(dataReader["quantity"]);
                    order.Total = (double)dataReader["total"];
                    order.Image = (string)dataReader["image"];

                    listOrder.Add(order);
                }

                dataReader.Close();

                return listOrder;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of order", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Orders> Read(string preparationDate,int id)//get the order with this date - app
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = CreateSelectCommandEmployeOrder(con, preparationDate,id);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Orders> listOrder = new List<Orders>();

                while (dataReader.Read())//if user on table
                {
                    Orders order = new Orders();
                    order.OrderNum = Convert.ToInt32(dataReader["orderNum"]);
                    order.EmployeeNum = Convert.ToInt32(dataReader["employNum"]);
                    order.PreprationDate =(string)dataReader["preparationDate"];
                    order.Status = Convert.ToInt32(dataReader["status"]);

                    listOrder.Add(order);
                }

                dataReader.Close();

                return listOrder;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of order", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public bool Update(Orders order) //updae the order - status + image - app
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database 
                con = Connect("ProjDB");

                SqlCommand updatecommand = CreateUpdateCommandStatusOrder(con, order);


                int numEffectedUser = updatecommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a order", ex);
            }

            finally
            {
                // close the db connection
                con.Close();
            }

            return true;
        }

        public List<Orders> Delete(int id, int Norder) //delepe product from table
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database 
                con = Connect("ProjDB");

                SqlCommand deletecommand = createDeleteCommand(con, id, Norder);


                // Execute the command
                SqlDataReader dataReader = deletecommand.ExecuteReader();

                List<Orders> listOrder = new List<Orders>();
                dataReader.Close();

                return listOrder;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a order", ex);
            }

            finally
            {
                // close the db connection
                con.Close();
            }

        }

        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = Web.Configuration.WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }

        SqlCommand CreateInsertCommandCheck(Orders order, SqlConnection con)//check if this order in the table
        {
            string artstr = "SELECT * FROM [Order] WHERE orderNum = '" + order.OrderNum + "'";


            SqlCommand cmd = createCommand(con, artstr);

            return cmd;

        }

        SqlCommand CreateInsertCommandOrder(Orders order, SqlConnection con)//insert order
        {
            string commandStr = "INSERT INTO [Order] ([orderNum]) VALUES (@orderNum)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@orderNum", SqlDbType.Int);
            cmd.Parameters["@orderNum"].Value = order.OrderNum;

            return cmd;
        }

        SqlCommand CreateInsertCommandCompanyOrder(Orders order, SqlConnection con)//insert company on order
        {

            string commandStr = "INSERT INTO CompanyOnOrder ([companyNum],[orderNum],[startDate],[dateArrivel]) VALUES (@companyNum,@orderNum,@startDate,@dateArrivel)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@companyNum", SqlDbType.Int);
            cmd.Parameters["@companyNum"].Value = order.Companynum;
            cmd.Parameters.Add("@orderNum", SqlDbType.Int);
            cmd.Parameters["@orderNum"].Value = order.OrderNum;
            cmd.Parameters.Add("@startDate", SqlDbType.Char);
            cmd.Parameters["@startDate"].Value = order.StartDate;
            cmd.Parameters.Add("@dateArrivel", SqlDbType.Char);
            cmd.Parameters["@dateArrivel"].Value = order.DateArrival;

            return cmd;
        }

        SqlCommand CreateInsertCommandProductOrder(Orders order, SqlConnection con)//insert product on order
        {

            string commandStr = "INSERT INTO ProductOnOrder ([barcodNum],[orderNum],[weight],[quantity],[total]) VALUES (@barcodNum,@orderNum,@weight,@quantity,@total)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@barcodNum", SqlDbType.Int);
            cmd.Parameters["@barcodNum"].Value = order.Productnum;
            cmd.Parameters.Add("@orderNum", SqlDbType.Int);
            cmd.Parameters["@orderNum"].Value = order.OrderNum;
            cmd.Parameters.Add("@weight", SqlDbType.Float);
            cmd.Parameters["@weight"].Value = order.Weight;
            cmd.Parameters.Add("@quantity", SqlDbType.Int);
            cmd.Parameters["@quantity"].Value = order.Quantity;
            cmd.Parameters.Add("@total", SqlDbType.Float);
            cmd.Parameters["@total"].Value = order.Total;

            return cmd;
        }

        SqlCommand CreateSelectCommandOrder(SqlConnection con, int idOrder)//get the prodct in this order
        {

            string commandStr = "SELECT * FROM ProductOnOrder WHERE orderNum="+ idOrder;

           SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }

        SqlCommand CreateSelectCommandOrderImage(SqlConnection con, int idOrder)//get employe with this order - app
        {

            string commandStr = "SELECT * FROM EmployeeOnOrder WHERE orderNum=" + idOrder;

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }

        SqlCommand CreateSelectCommandEmployeOrder(SqlConnection con, string preparationDate,int id)//get the order with the date - app
        {

            string commandStr = "SELECT * FROM EmployeeOnOrder WHERE preparationDate LIKE @preparationDate AND employNum=" + id;

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.AddWithValue("@preparationDate", "%" + preparationDate + "%");

            return cmd;

        }

        SqlCommand CreateUpdateCommandStatusOrder(SqlConnection con, Orders order)//update order - status + image - app
        {
            string commandStr = "UPDATE EmployeeOnOrder SET status = 0,image ='" + order.Image + "' WHERE orderNum='" + order.OrderNum + "' ";
            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;
        }

        SqlCommand createDeleteCommand(SqlConnection con, int id, int Norder)//delete product from order
        {
            string commandStr = "DELETE from [ProductOnOrder] WHERE orderNum='" + Norder + "' And barcodNum='"+id+"' ";
            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;
        }

        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {

            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 20; // seconds

            return cmd;
        }
    }
}
