using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Jacobs.Models.DAL
{
    public class EmployeeDataServices
    {

        public int Insert(Employees employe) //insert new employe
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertCommand(employe, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                // this code needs to write the error to a log file
                throw new Exception("Failed to insert a employe", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


            // num effected
            return numEffected;


        }

        public List<Employees> Read(int employeNum)//get this employe 
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommand(con, employeNum);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Employees> listEmployes = new List<Employees>();
                while (dataReader.Read())
                {
                    Employees employe = new Employees();
                    employe.EmployeNum = Convert.ToInt32(dataReader["employeNum"]);
                    employe.Name = (string)dataReader["name"];
                    employe.Phone = (string)dataReader["phone"];
                    employe.Role = (string)dataReader["role"];

                    if (dataReader.IsDBNull(4))
                        employe.DistributaionArea = null;
                    else
                        employe.DistributaionArea= (string)dataReader["distributaionArea"];

                    listEmployes.Add(employe);
                }
                dataReader.Close();

                return listEmployes;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of employe", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }


        public List<Employees> Read()//get the all the employee that Active
        {

            SqlConnection con = null;

            try
            {
                // Connect
                con = Connect("ProjDB");

                // Create the insert command
                SqlCommand selectCommand = createSelectCommandEmploye(con);

                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader();

                List<Employees> listEmployes = new List<Employees>();
                while (dataReader.Read())//if user on table
                {
                    Employees employe = new Employees();
                    employe.EmployeNum = Convert.ToInt32(dataReader["employeNum"]);
                    employe.Name = (string)dataReader["name"];
                    employe.Phone = (string)dataReader["phone"];
                    employe.Role = (string)dataReader["role"];

                    if (dataReader.IsDBNull(4))
                        employe.DistributaionArea = null;
                    else
                        employe.DistributaionArea = (string)dataReader["distributaionArea"];

                    listEmployes.Add(employe);
                }
                dataReader.Close();

                return listEmployes;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading of employe", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        public bool Update(Employees employe) //update the employe
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database
                con = Connect("ProjDB");

                SqlCommand updatecommand = createUpdateCommand(con, employe);


                int numEffectedUser = updatecommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a employe", ex);
            }

            finally
            {
                // close the db connection
                con.Close();
            }

            return true;
        }

        public List<Employees> Delete(int id) //delete this employe - update this employe to 0
        {

            SqlConnection con = null;


            try
            {
                //C - Connect to the Database 
                con = Connect("ProjDB");

                SqlCommand deletecommand = createDeleteCommandStatus(con, id);


                // Execute the command
                SqlDataReader dataReader = deletecommand.ExecuteReader();

                List<Employees> listEmployes = new List<Employees>();
                dataReader.Close();

                return listEmployes;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update a employe", ex);
            }

            finally
            {
                // close the db connection
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

        SqlCommand CreateInsertCommand(Employees employe, SqlConnection con)//inset to table new employe
        {
            string commandStr = "INSERT INTO Employes ([employeNum],[name],[phone],[role],[distributaionArea]) VALUES (@employeNum,@name,@phone,@role,@diArea)";

            SqlCommand cmd = createCommand(con, commandStr);

            cmd.Parameters.Add("@employeNum", SqlDbType.Int);
            cmd.Parameters["@employeNum"].Value = employe.EmployeNum;
            cmd.Parameters.Add("@name", SqlDbType.Char);
            cmd.Parameters["@name"].Value = employe.Name;
            cmd.Parameters.Add("@phone", SqlDbType.Char);
            cmd.Parameters["@phone"].Value = employe.Phone;
            cmd.Parameters.Add("@role", SqlDbType.Char);
            cmd.Parameters["@role"].Value = employe.Role;
            cmd.Parameters.Add("@diArea", SqlDbType.Char);
            if (employe.DistributaionArea != null)
            {
                cmd.Parameters["@diArea"].Value = employe.DistributaionArea;
            }
            else
                cmd.Parameters["@diArea"].Value = DBNull.Value;

            return cmd;
        }

        private SqlCommand createSelectCommand(SqlConnection con,int employeNum)//get this employe
        {

            string commandStr = "SELECT * FROM Employes WHERE employeNum ="+employeNum;

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }

        private SqlCommand createSelectCommandEmploye(SqlConnection con)//get all the employe that Active
        {

            string commandStr = "SELECT * FROM Employes WHERE Employes.status=1";

            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;

        }


        SqlCommand createUpdateCommand(SqlConnection con, Employees employe)//update this employe
        {
            string commandStr = "UPDATE Employes SET phone='"+employe.Phone+"', role='"+employe.Role+ "', distributaionArea='"+employe.DistributaionArea+"' WHERE Employes.employeNum='" + employe.EmployeNum + "' ";
            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;
        }

        SqlCommand createDeleteCommandStatus(SqlConnection con, int id)//delete this employe - change the status to 0 
        {
            string commandStr = "UPDATE Employes SET status=0 WHERE Employes.employeNum='" + id + "' ";
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