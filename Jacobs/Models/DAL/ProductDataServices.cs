using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using GoogleApi.Test.Maps.DistanceMatrix;

namespace Jacobs.Models.DAL
{
    public class ProductDataServices
    {
        public int Insert(Products product) //insert new product
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertCommand(product, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                // this code needs to write the error to a log file
                throw new Exception("Failed to insert a product", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


            // num effected
            return numEffected;


        }

        public List<Products> Read()//get the all product
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandProducts(con);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Products> listProdcut = new List<Products>();
                while (dataReader.Read())//if user on table
                {
                    Products product = new Products();
                    product.Barcod = Convert.ToInt32(dataReader["barcod"]);
                    product.NameProduct = (string)dataReader["productName"];
                    product.Type = (string)dataReader["type"];
                    product.WeightperOne = (double)dataReader["weightperOne"];
                    product.DownfromTotal = (double)dataReader["downfromTotal"];

                    listProdcut.Add(product);
                }
                dataReader.Close();

                return listProdcut;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of product", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Products> getproducts()//get the all product
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandProductsonOrder(con);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Products> listProdcut = new List<Products>();
                while (dataReader.Read())//if user on table
                {
                    Products product = new Products();
                    product.Barcod = Convert.ToInt32(dataReader["barcod"]);
                    product.NameProduct = (string)dataReader["productName"];
                    product.OrderNum= Convert.ToInt32(dataReader["orderNum"]);

                    listProdcut.Add(product);
                }
                dataReader.Close();

                return listProdcut;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of product", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Products> Read(string name)//get info to this product
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandproductNAME(con, name);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Products> listProdcut = new List<Products>();
                while (dataReader.Read())//if user on table
                {
                    Products product = new Products();
                    product.Barcod = Convert.ToInt32(dataReader["barcod"]);
                    product.NameProduct = (string)dataReader["productName"];
                    product.Type = (string)dataReader["type"];
                    product.WeightperOne = (double)dataReader["weightperOne"];
                    product.DownfromTotal = (double)dataReader["downfromTotal"];

                    listProdcut.Add(product);
                }
                dataReader.Close();

                return listProdcut;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of product", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Products> Read(int id)//get the products to this order - app
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandproductID(con, id);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Products> listProdcut = new List<Products>();
                while (dataReader.Read())//if user on table
                {
                    Products product = new Products();
                    product.Barcod = Convert.ToInt32(dataReader["barcod"]);
                    product.NameProduct = (string)dataReader["productName"];
                    product.Type = (string)dataReader["type"];
                    product.Quantity = Convert.ToInt32(dataReader["quantity"]);
                    product.WeightAll = (double)dataReader["weight"];
                    product.WeightperOne=(double)dataReader["weightperOne"];
                    product.DownfromTotal= (double)dataReader["downfromTotal"];
                    product.Status = Convert.ToInt32(dataReader["status"]);

                    listProdcut.Add(product);
                }
                dataReader.Close();

                return listProdcut;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of product", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public bool Update(Products product) //update the product - status :  - app
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database 
                con = Connect("ProjDB");

                SqlCommand updatecommand = CreateUpdateCommandStatusProduct(con, product);


                int numEffectedUser = updatecommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a product", ex);
            }

            finally
            {
                // close the db connection
                con.Close();
            }

            return true;
        }
        SqlConnection Connect(string connectionStringName)
        {

            string connectionString =WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }

        SqlCommand CreateInsertCommand(Products product, SqlConnection con)//insert new product
        {
            string commandStr = "INSERT INTO Product ([barcod],[productName],[type],[weightperOne],[downfromTotal]) VALUES (@barcod,@productName,@type,@weightperOne,@downfromTotal)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@barcod", SqlDbType.Int);
            cmd.Parameters["@barcod"].Value = product.Barcod;
            cmd.Parameters.Add("@productName", SqlDbType.Char);
            cmd.Parameters["@productName"].Value = product.NameProduct;
            cmd.Parameters.Add("@type", SqlDbType.Char);
            cmd.Parameters["@type"].Value = product.Type;
            cmd.Parameters.Add("@weightperOne", SqlDbType.Float);
            cmd.Parameters["@weightperOne"].Value = product.WeightperOne;
            cmd.Parameters.Add("@downfromTotal", SqlDbType.Float);
            cmd.Parameters["@downfromTotal"].Value = product.DownfromTotal;

            return cmd;
        }

        SqlCommand createSelectCommandProducts(SqlConnection con)//get all product
        {

            string commandStr = "SELECT * FROM Product";

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }

        SqlCommand createSelectCommandProductsonOrder(SqlConnection con)//get all product to all orders
        {
            string commandStr = "select Product.barcod,ProductOnOrder.orderNum, Product.productName from ProductOnOrder inner join  Product on ProductOnOrder.barcodNum=Product.barcod";

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;
        }


        SqlCommand createSelectCommandproductNAME(SqlConnection con, string name)//get info to this product
        {

            string commandStr = "SELECT * FROM Product WHERE productName LIKE @name";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.AddWithValue("@name", "%" + name + "%");
            return cmd;
        }

        SqlCommand createSelectCommandproductID(SqlConnection con, int id)//get the product to this order
        {

            string commandStr = "SELECT * FROM Product INNER JOIN ProductOnOrder ON Product.barcod = ProductOnOrder.barcodNum WHERE ProductOnOrder.orderNum LIKE @orderNum";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.AddWithValue("@orderNum", "%" + id + "%");
            return cmd;
        }


        SqlCommand CreateUpdateCommandStatusProduct(SqlConnection con, Products product)//update product - app
        {
            string commandStr = "UPDATE ProductOnOrder SET status = 0,total='"+product.WeightAll+"' WHERE ProductOnOrder.orderNum ='"+product.Quantity+"' AND ProductOnOrder.barcodNum='"+product.Barcod+"'";
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