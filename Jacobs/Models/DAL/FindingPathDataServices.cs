using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Threading;



namespace Jacobs.Models.DAL
{
    public class FindingPathDataServices
    {
        //string[] addressArr;
        public List<FindingPaths> Read(string date)//get the all the employee 
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandFindingFath(con, date);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<FindingPaths> FindingPathslist = new List<FindingPaths>();
                while (dataReader.Read())//if user on table
                {
                    FindingPaths findingPath = new FindingPaths();
                    findingPath.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    findingPath.CompanyName = (string)dataReader["companyName"];
                    findingPath.Address = (string)(dataReader["address"]);
                    findingPath.DateArrivel=(string)(dataReader["dateArrivel"]);
                    findingPath.DistributaionArea=(string)(dataReader["distributaionArea"]);
                    findingPath.Lat = (double)dataReader["lat"];
                    findingPath.Lng = (double)dataReader["lng"];
                  



                    FindingPathslist.Add(findingPath);
                }
                dataReader.Close();

                return FindingPathslist;
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
        //public List<FindingPaths> readMatrix()//get the all the employee 
        //{

        //    SqlConnection con = null;

        //    try
        //    {
        //        // Connect
        //        con = ConnectForMatrix("ProjDB");

        //        // Create the insert command
        //        SqlCommand pushCommand = createSelectCommandFindingFath(con);

        //        // Execute the command
        //        SqlDataReader dataReader = pushCommand.ExecuteReader();

        //        List<FindingPaths> FindingPathslist = new List<FindingPaths>();
        //        while (dataReader.Read())//if user on table
        //        {
        //            FindingPaths findingPath = new FindingPaths();
        //            findingPath.CompanyNum = (int)dataReader["companyNum"];
        //            findingPath.Address = (string)(dataReader["address"]);
        //            findingPath.DateArrivel = (string)(dataReader["dateArrivel"]);
        //            findingPath.DistributaionArea = (string)(dataReader["distributaionArea"]);
        //            findingPath.Lat = (double)dataReader["lat"];
        //            findingPath.Lng = (double)dataReader["lng"];




        //            FindingPathslist.Add(findingPath);
        //        }
        //        dataReader.Close();
             


        //        return FindingPathslist;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write the error to log
        //        throw new Exception("failed in reading of FindingFath", ex);
        //    }
        //    finally
        //    {
        //        // Close the connection
        //        if (con != null)
        //            con.Close();
        //    }

        //}

        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }
        SqlConnection ConnectForMatrix(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }
        private SqlCommand createSelectCommandFindingFath(SqlConnection con,string date)
        {

            string commandStr = "select Company.companyNum,Company.companyName,Company.address,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.lat,Company.lng from Company INNER JOIN CompanyOnOrder ON Company.companyNum=CompanyOnOrder.companyNum WHERE CompanyOnOrder.dateArrivel LIKE @date ";

            SqlCommand cmd = createCommand(con, commandStr);


            cmd.Parameters.AddWithValue("@date", "%" + date + "%");

            return cmd;

        }
        //private SqlCommand createSelectCommandFindingFath(SqlConnection con)
        //{

        //    string commandStr = "select Company.companyNum,Company.address,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.lat,Company.lng from Company INNER JOIN CompanyOnOrder ON Company.companyNum=CompanyOnOrder.companyNum";

        //    SqlCommand cmd = pushCommand(con, commandStr);



        //    return cmd;

        //}
        //SqlCommand pushCommand(SqlConnection con, string CommandSTR)
        //{

        //    SqlCommand cmd = new SqlCommand();  // create the command object
        //    cmd.Connection = con;               // assign the connection to the command object
        //    cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandTimeout = 20; // seconds

        //    return cmd;
        //}

        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {

            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 20; // seconds

            return cmd;
        }

        //public void buildMatrix(List<FindingPaths> FindingPathslist)
        //{
        //    List<FindingPaths> allAddressfindingPaths = new List<FindingPaths>;
        //    int i ;
        //    addressArr = new string[FindingPathslist.Count+1];
        //    for ( i = 0; i < FindingPathslist.Count; i++)
        //    {
        //        addressArr[i] = FindingPathslist[i].Address;
        //    }
        //    addressArr[i] = "גשר העץ 27,עמק חפר";
        //    for (int j = 0; j < addressArr.Length; j++)
        //    {

        //    }


    //}



}
}