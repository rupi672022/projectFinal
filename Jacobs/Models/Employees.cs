using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;

namespace Jacobs.Models
{
    public class Employees
    {
        
        int employeNum;
        string name;
        string phone;
        string role; //driver or collector
        string distributaionArea; //only for driver
        int orderNum;

        public Employees() { }
        public Employees(int employeNum, string name, string phone, string role, string distributaionArea,int orderNum)
        {
            this.employeNum = employeNum;
            this.name = name;
            this.phone = phone;
            this.role = role;
            this.distributaionArea = distributaionArea;
            this.orderNum = orderNum;
        }

        public int EmployeNum { get => employeNum; set => employeNum = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Role { get => role; set => role = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }
        public int OrderNum { get => orderNum; set => orderNum = value; }

        public List<Employees> Read()//read all the employe that Active
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            List<Employees> listEmployes = ds.Read();
            return listEmployes;
        }

        public List<Employees> Read(int employeNum)//read only this employe 
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            List<Employees> listEmployes = ds.Read(employeNum);
            return listEmployes;
        }


        public int Insert()//insert new employe
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public bool Update()//update the employe
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            return ds.Update(this);

        }

        public List<Employees> Delete(int id)//delete this employe - only update to 0
        {
            EmployeeDataServices ds = new EmployeeDataServices();
            return ds.Delete(id);
        }
    }
}