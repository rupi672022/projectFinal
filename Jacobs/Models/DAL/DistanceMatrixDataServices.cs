using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Threading;
using Jacobs.Models.DAL;
using Jacobs.Models;
namespace GoogleApi.Test.Maps.DistanceMatrix
{
    public class DistanceMatrixDataServices
    {
        
        
        public int Insert(DistanceMatrix distanceMatrix, List<DistanceMatrix> final)
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                
                foreach(var i in final)
                {
                    DistanceMatrix postDm = new DistanceMatrix();
                    postDm.IdFrom = i.IdFrom;
                    postDm.IdTo = i.IdTo;
                    postDm.From = i.From;
                    postDm.To = i.To;
                    postDm.Distance = i.Distance;
                    postDm.Area = i.Area;
                   
                    SqlCommand insertCommand = CreateInsertCommand(postDm, con);
                        numEffected = insertCommand.ExecuteNonQuery();

                }
               
               

                //E Execute

            }

            catch (Exception ex)
            {

                // this code needs to write the error to a log file
                throw new Exception("Failed to insert a matrix", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


            // num effected
            return numEffected;


        }
        public List<DistanceMatrix> Read(string area)
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandDistanceMatrix(con,area);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<DistanceMatrix> DistanceMatrixlist = new List<DistanceMatrix>();
                while (dataReader.Read())//if user on table
                {  
                    DistanceMatrix distanceMatrixTests = new DistanceMatrix();
                    distanceMatrixTests.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    distanceMatrixTests.Address = (string)(dataReader["address"]);
                    DistanceMatrixlist.Add(distanceMatrixTests);
                }
                dataReader.Close();

                return DistanceMatrixlist;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of company", ex);
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

        SqlCommand CreateInsertCommand(DistanceMatrix distanceMatrix, SqlConnection con)//insert new company
        {
           string commandStr = "INSERT INTO DistanceMatrix ([from],[to],[distance],[companyNumFrom],[companyNumTo],[area]) VALUES (@from,@to,@distance,@companyNumFrom,@companyNumTo,@area)";
            
            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@from", SqlDbType.Char);
            cmd.Parameters["@from"].Value = distanceMatrix.From;

            cmd.Parameters.Add("@to", SqlDbType.Char);
            cmd.Parameters["@to"].Value = distanceMatrix.To;

            cmd.Parameters.Add("@distance", SqlDbType.Int);
            cmd.Parameters["@distance"].Value = distanceMatrix.Distance;

            cmd.Parameters.Add("@companyNumFrom", SqlDbType.Int);
            cmd.Parameters["@companyNumFrom"].Value = distanceMatrix.IdFrom;

            cmd.Parameters.Add("@companyNumTo", SqlDbType.Int);
            cmd.Parameters["@companyNumTo"].Value = distanceMatrix.IdTo;

            cmd.Parameters.Add("@area", SqlDbType.Char);
            cmd.Parameters["@area"].Value = distanceMatrix.Area;
            return cmd;
        }


        private SqlCommand createSelectCommandDistanceMatrix(SqlConnection con,string area)
        {

            string commandStr = "SELECT * FROM Company WHERE Company.distributaionArea LIKE @area";

            SqlCommand cmd = createCommand(con, commandStr);
           cmd.Parameters.AddWithValue("@area", "%" + area + "%");


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
