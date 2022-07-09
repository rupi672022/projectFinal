using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Threading;
using GoogleApi.Test.Maps.DistanceMatrix;


namespace Jacobs.Models.DAL
{
    
    public class FindingPathDataServices
    {
        
        //string[] addressArr;
        public List<FindingPaths> Read(string date, int DriverName)//get the all the employee 
        {
            
            
            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandFindingFath(con, date, DriverName);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<FindingPaths> FindingPathslist = new List<FindingPaths>();
                while (dataReader.Read())//if user on table
                {
                    FindingPaths findingPath = new FindingPaths();
                   
                    findingPath.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    findingPath.CompanyName = (string)dataReader["companyName"];
                    findingPath.Address = (string)(dataReader["address"]);
                    findingPath.DateArrivel = (string)(dataReader["dateArrivel"]);
                    findingPath.DistributaionArea = (string)(dataReader["distributaionArea"]);
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
    
        public List<FindingPaths> ReadDistance(string date)
        {


            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandFindingFathDistance(con, date);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<FindingPaths> FindingPathslist = new List<FindingPaths>();
                while (dataReader.Read())//if user on table
                {

                    FindingPaths findingPath = new FindingPaths();
                    if (dataReader.IsDBNull(2))
                        findingPath.DistributaionArea = null;
                    else
                        findingPath.DistributaionArea = (string)dataReader["distributaionArea"];
                   
                        findingPath.FromCompany = (string)dataReader["from"];  
                        findingPath.ToCompany = (string)(dataReader["to"]);
                        findingPath.DistanceCompany = Convert.ToInt32(dataReader["distance"]);
                        findingPath.IdFromCompany = Convert.ToInt32(dataReader["companyNumFrom"]);
                        findingPath.IdToCompany = Convert.ToInt32(dataReader["companyNumTo"]);



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
        private SqlCommand createSelectCommandFindingFath(SqlConnection con,string date, int DriverName)
        {
            string commandStr = "select Company.companyNum,Company.companyName,Company.address,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.lat,Company.lng";
            commandStr += " from Company INNER JOIN CompanyOnOrder ON Company.companyNum = CompanyOnOrder.companyNum";
            commandStr += " right join EmployeeOnOrder On EmployeeOnOrder.orderNum = CompanyOnOrder.orderNum";
            commandStr+=" WHERE CompanyOnOrder.dateArrivel Like @date And EmployeeOnOrder.driverNum ="+ DriverName;
            
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@date", "%" + date + "%");

            return cmd;
            
        }

        SqlCommand createSelectCommandFindingFathDistance(SqlConnection con, string date)
        {
            string commandStr = "select DISTINCT Company.companyName,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.address, DistanceMatrix.[from],DistanceMatrix.[to],DistanceMatrix.distance,DistanceMatrix.companyNumFrom,DistanceMatrix.companyNumTo,DistanceMatrix.area";
            commandStr += " from CompanyOnOrder inner join Company on Company.companyNum = CompanyOnOrder.companyNum";
            commandStr += " left join DistanceMatrix on  DistanceMatrix.companyNumFrom = Company.companyNum";
            commandStr += " WHERE CompanyOnOrder.dateArrivel = '" + date + "'";
            commandStr += " union ";
            commandStr += " select DISTINCT Company.companyName,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.address,DistanceMatrix.[from],DistanceMatrix.[to],DistanceMatrix.distance,DistanceMatrix.companyNumFrom,DistanceMatrix.companyNumTo,DistanceMatrix.area";
            commandStr += " from CompanyOnOrder inner join Company on Company.companyNum = 1";
            commandStr += " left join DistanceMatrix on  DistanceMatrix.companyNumFrom = Company.companyNum";
            commandStr += " WHERE Company.distributaionArea = '' and CompanyOnOrder.dateArrivel = '" + date + "'";
           

            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@date", "%" + date + "%");

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