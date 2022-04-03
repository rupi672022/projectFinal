using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Models.DAL;

namespace System.Models
{
    public class Employees
    {
        int employeNum;
        string name;
        string phone;
        string role; //driver or collector
        string distributaionArea; //only for driver

        public Employees() { }
        public Employees(int employeNum, string name, string phone, string role, string distributaionArea)
        {
            this.employeNum = employeNum;
            this.name = name;
            this.phone = phone;
            this.role = role;
            this.distributaionArea = distributaionArea;
        }

        public int EmployeNum { get => employeNum; set => employeNum = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Role { get => role; set => role = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }

        public List<Employees> Read()
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            List<Employees> listEmployes = ds.Read();
            return listEmployes;
        }

        public List<Employees> Read(int employeNum)
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            List<Employees> listEmployes = ds.Read(employeNum);
            return listEmployes;
        }


        public int Insert()//insert the article to the table
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public bool Update()//insert the article to the table
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            return ds.Update(this);

        }

        public List<Employees> Delete(int id)
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            return ds.Delete(id);
        }
    }
}