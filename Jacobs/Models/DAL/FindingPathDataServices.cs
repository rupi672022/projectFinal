﻿using System;
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
                    findingPath.DateArrivel = (string)(dataReader["dateArrivel"]);
                    findingPath.DistributaionArea = (string)(dataReader["distributaionArea"]);
                    findingPath.Lat = (double)dataReader["lat"];
                    findingPath.Lng = (double)dataReader["lng"];


                    //findingPath.Lat = (double)dataReader["lat"];
                    //findingPath.Lng = (double)dataReader["lng"];




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
                    findingPath.DistributaionArea = (string)dataReader["distributaionArea"];
                    if (dataReader.IsDBNull(4))
                        findingPath.FromCompany = null;
                    else
                        findingPath.FromCompany = (string)dataReader["from"];

                    if (dataReader.IsDBNull(5))
                        findingPath.ToCompany = null;
                    else
                        findingPath.ToCompany = (string)(dataReader["to"]);

                    if (dataReader.IsDBNull(6))
                        findingPath.DistanceCompany = 0;
                    else
                        findingPath.DistanceCompany = Convert.ToInt32(dataReader["distance"]);

                    if (dataReader.IsDBNull(7))
                        findingPath.IdFromCompany = 0;
                    else
                        findingPath.IdFromCompany = Convert.ToInt32(dataReader["companyNumFrom"]);


                    if (dataReader.IsDBNull(8))
                        findingPath.IdToCompany = 0;
                    else
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
        private SqlCommand createSelectCommandFindingFath(SqlConnection con,string date)
        {
            string commandStr = "select Company.companyNum,Company.companyName,Company.address,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.lat,Company.lng from Company INNER JOIN CompanyOnOrder ON Company.companyNum=CompanyOnOrder.companyNum WHERE CompanyOnOrder.dateArrivel LIKE @date ";
            
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@date", "%" + date + "%");

            return cmd;
            
        }

        SqlCommand createSelectCommandFindingFathDistance(SqlConnection con, string date)
        {
            string commandStr = "select Company.companyName,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.address, DistanceMatrix.[from],DistanceMatrix.[to],DistanceMatrix.distance,DistanceMatrix.companyNumFrom,DistanceMatrix.companyNumTo,DistanceMatrix.area";
            commandStr += " from CompanyOnOrder inner join Company on Company.companyNum = CompanyOnOrder.companyNum";
            commandStr += " left join DistanceMatrix on  DistanceMatrix.companyNumFrom = Company.companyNum";
            commandStr += " WHERE CompanyOnOrder.dateArrivel like @date ";
            commandStr += " union ";
            commandStr += " select Company.companyName,CompanyOnOrder.dateArrivel,Company.distributaionArea,Company.address,DistanceMatrix.[from],DistanceMatrix.[to],DistanceMatrix.distance,DistanceMatrix.companyNumFrom,DistanceMatrix.companyNumTo,DistanceMatrix.area";
            commandStr += " from CompanyOnOrder inner join Company on Company.companyNum = 1";
            commandStr += " left join DistanceMatrix on  DistanceMatrix.companyNumFrom = Company.companyNum";
            commandStr += " WHERE Company.distributaionArea = '' and CompanyOnOrder.dateArrivel like @date";

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