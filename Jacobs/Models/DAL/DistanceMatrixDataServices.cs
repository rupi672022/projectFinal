using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace GoogleApi.Test.Maps.DistanceMatrix
{
    public class DistanceMatrixDataServices
    {
        //string[] addressArr;

        public int Insert(DistanceMatrix list) 
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertCommand(list, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                // this code needs to write the error to a log file
                throw new Exception("Failed to insert a company", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


            // num effected
            return numEffected;


        }
        public List<DistanceMatrixDataServices> Read(string Area)//get the all the employee 
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandFindingFath(con, Area);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<DistanceMatrixDataServices> DistanceMatrixlist = new List<DistanceMatrixDataServices>();
                while (dataReader.Read())//if user on table
                {
                    DistanceMatrixDataServices distanceMatrixTests = new DistanceMatrixDataServices();
                    //distanceMatrixTests.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    //findingPath.CompanyName = (string)dataReader["companyName"];
                    //findingPath.Address = (string)(dataReader["address"]);
                    //findingPath.DateArrivel = (string)(dataReader["dateArrivel"]);
                    //findingPath.DistributaionArea = (string)(dataReader["distributaionArea"]);
                    //findingPath.Lat = (double)dataReader["lat"];
                    //findingPath.Lng = (double)dataReader["lng"];




                    DistanceMatrixlist.Add(distanceMatrixTests);
                }
                dataReader.Close();

                return DistanceMatrixlist;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of FindingFath", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }



        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }

        SqlCommand CreateInsertCommand(DistanceMatrix list, SqlConnection con)//insert new company
        {
            string commandStr = "INSERT INTO Company ([companyName],[address],[openHour],[closeHour],[nameContact],[phoneContact],[lat],[lng],[distributaionArea]) VALUES (@companyName,@address,@openHour,@closeHour,@nameContact,@phoneContact,@lat,@lng,@distributaionArea)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@companyName", SqlDbType.Char);
            cmd.Parameters["@companyName"].Value = company.CompanyName;

            cmd.Parameters.Add("@address", SqlDbType.Char);
            cmd.Parameters["@address"].Value = company.Address;

            cmd.Parameters.Add("@openHour", SqlDbType.Char);
            cmd.Parameters["@openHour"].Value = company.OpenHour;


            return cmd;
        }


        private SqlCommand createSelectCommandFindingFath(SqlConnection con, string Area)
        {

            string commandStr = "select Company.companyNum,Company.companyName,Company.address,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.lat,Company.lng from Company INNER JOIN CompanyOnOrder ON Company.companyNum=CompanyOnOrder.companyNum WHERE Company.distributaionArea LIKE @Area ";

            SqlCommand cmd = createCommand(con, commandStr);


           // cmd.Parameters.AddWithValue("@date", "%" + date + "%");

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
