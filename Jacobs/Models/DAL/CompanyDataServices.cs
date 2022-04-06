using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Jacobs.Models.DAL
{
    public class CompanyDataServices
    {
        public int Insert(Company company) //insert new company
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertCommand(company, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

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
        public List<Company> Read()//get the all company
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandCompany(con);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Company> listCompnay = new List<Company>();
                while (dataReader.Read())//if user on table
                {
                    Company company = new Company();
                    company.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    company.CompanyName = (string)dataReader["companyName"];
                    company.Address = (string)dataReader["address"];

                    if (dataReader.IsDBNull(3))
                        company.OpenHour = null;
                    else
                        company.OpenHour = (string)dataReader["openHour"];

                    if (dataReader.IsDBNull(4))
                        company.CloseHour = null;
                    else
                        company.CloseHour = (string)dataReader["closeHour"];

                    company.NameContact = (string)dataReader["nameContact"];
                    company.PhoneContact = (string)dataReader["phoneContact"];

                    if (dataReader.IsDBNull(7))
                        company.Lat = 0;
                    else
                        company.Lat = (double)dataReader["lat"];

                    if (dataReader.IsDBNull(8))
                        company.Lng = 0;
                    else
                        company.Lng = (double)dataReader["Lng"];

                    if (dataReader.IsDBNull(9))
                        company.DistributaionArea = null;
                    else
                        company.DistributaionArea = (string)dataReader["distributaionArea"];

                    listCompnay.Add(company);
                }
                dataReader.Close();

                return listCompnay;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of compay", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public List<Company> Read(string name)//get this company
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandCompanyNAME(con, name);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Company> listCompnay = new List<Company>();
                while (dataReader.Read())//if user on table
                {
                    Company company = new Company();
                    company.CompanyNum = Convert.ToInt32(dataReader["companyNum"]);
                    company.CompanyName = (string)dataReader["companyName"];
                    company.Address = (string)dataReader["address"];
                    company.DistributaionArea = (string)dataReader["distributaionArea"];
                    company.OpenHour = (string)dataReader["openHour"];
                    company.CloseHour = (string)dataReader["closeHour"];
                    company.NameContact = (string)dataReader["nameContact"];
                    company.PhoneContact = (string)dataReader["phoneContact"];

                    listCompnay.Add(company);
                }
                dataReader.Close();

                return listCompnay;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of compay", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public bool Update(Company company) //update the contact on this company
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database - קשר עם הפרויקט 
                con = Connect("ProjDB");

                SqlCommand updatecommand = createUpdateCommand(con, company);


                int numEffectedUser = updatecommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a company", ex);
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

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString); // מתחבר אל הנתונים
            con.Open(); // סטטוס - פתוח 
            return con;
        }

        SqlCommand CreateInsertCommand(Company company, SqlConnection con)//insert new company
        {
            string commandStr = "INSERT INTO Company ([companyName],[address],[openHour],[closeHour],[nameContact],[phoneContact],[lat],[lng],[distributaionArea]) VALUES (@companyName,@address,@openHour,@closeHour,@nameContact,@phoneContact,@lat,@lng,@distributaionArea)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@companyName", SqlDbType.Char);
            cmd.Parameters["@companyName"].Value = company.CompanyName;

            cmd.Parameters.Add("@address", SqlDbType.Char);
            cmd.Parameters["@address"].Value = company.Address;

            cmd.Parameters.Add("@openHour", SqlDbType.Char);
            cmd.Parameters["@openHour"].Value = company.OpenHour;

            cmd.Parameters.Add("@closeHour", SqlDbType.Char);
            cmd.Parameters["@closeHour"].Value = company.CloseHour;

            cmd.Parameters.Add("@nameContact", SqlDbType.Char);
            cmd.Parameters["@nameContact"].Value = company.NameContact;

            cmd.Parameters.Add("@phoneContact", SqlDbType.Char);
            cmd.Parameters["@phoneContact"].Value = company.PhoneContact;

            cmd.Parameters.Add("@lat", SqlDbType.Float);
            cmd.Parameters["@lat"].Value = company.Lat;

            cmd.Parameters.Add("@lng", SqlDbType.Float);
            cmd.Parameters["@lng"].Value = company.Lng;

            cmd.Parameters.Add("@distributaionArea", SqlDbType.Char);
            cmd.Parameters["@distributaionArea"].Value = company.DistributaionArea;

            return cmd;
        }

        private SqlCommand createSelectCommandCompany(SqlConnection con)//get all company
        {

            string commandStr = "SELECT * FROM Company";

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }


        private SqlCommand createSelectCommandCompanyNAME(SqlConnection con, string name)//get this company
        {

            string commandStr = "SELECT * FROM Company WHERE companyName LIKE @name";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.AddWithValue("@name", "%" + name + "%");
            return cmd;
        }

        SqlCommand createUpdateCommand(SqlConnection con, Company company)//update this company
        {
            string commandStr = "UPDATE Company SET nameContact='" + company.NameContact + "', phoneContact='" + company.PhoneContact + "' WHERE companyNum='" + company.CompanyNum + "' ";
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