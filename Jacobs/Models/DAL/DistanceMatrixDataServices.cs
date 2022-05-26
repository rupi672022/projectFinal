﻿using System;
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

        public int Insert(DistanceMatrix distanceMatrix)
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertCommand(distanceMatrix,con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

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
            string commandStr = "INSERT INTO DistanceMatrix ([from],[to],[distance]) VALUES (@from,@to,@distance)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@from", SqlDbType.Int);
            cmd.Parameters["@from"].Value = distanceMatrix.From;

            cmd.Parameters.Add("@to", SqlDbType.Int);
            cmd.Parameters["@to"].Value = distanceMatrix.To;

            cmd.Parameters.Add("@distance", SqlDbType.Int);
            cmd.Parameters["@distance"].Value = distanceMatrix.Distance;


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
