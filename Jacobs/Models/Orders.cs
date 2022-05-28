using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;
using GoogleApi.Test.Maps.DistanceMatrix;

namespace Jacobs.Models
{
    public class Orders
    {
        int orderNum;
        string startDate;
        string dateArrival;
        int companynum;
        string companyName;
        string openHour;
        string distributaionArea;
        double weight;//משקל סה"כ למוצר
        int quantity;//אם לא צריך לשקול
        double total;
        int productnum;
        string productName;
        int employeeNum;
        int driverNum;
        string preprationDate;
        string image;
        int status = 1;


        public Orders() { }

        public Orders(int orderNum)
        {
            this.orderNum = orderNum;
        }

        public Orders(int orderNum, string startDate, string dateArrival, int companynum)
        {
            this.orderNum = orderNum;
            this.startDate = startDate;
            this.dateArrival = dateArrival;
            this.companynum = companynum;
        }

        public Orders(int orderNum, double weight, int quantity, double total, int productnum, string productName)
        {
            this.orderNum = orderNum;
            this.weight = weight;
            this.quantity = quantity;
            this.total = total;
            this.productnum = productnum;
            this.productName = productName;
        }

        public Orders(int orderNum, int employeeNum, string preprationDate,int status,int driverNum)
        {
            this.orderNum = orderNum;
            this.employeeNum = employeeNum;
            this.driverNum = driverNum;
            this.preprationDate = preprationDate;
            this.status = status;
        }

        public Orders(int orderNum, string dateArrival, int companynum, string companyName, string openHour, string distributaionArea)
        {
            this.orderNum = orderNum;
            this.dateArrival = dateArrival;
            this.companynum = companynum;
            this.companyName = companyName;
            this.openHour = openHour;
            this.distributaionArea = distributaionArea;
        }

        public int OrderNum { get => orderNum; set => orderNum = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string DateArrival { get => dateArrival; set => dateArrival = value; }
        public int Companynum { get => companynum; set => companynum = value; }
        public double Weight { get => weight; set => weight = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Productnum { get => productnum; set => productnum = value; }
        public string PreprationDate { get => preprationDate; set => preprationDate = value; }
        public int EmployeeNum { get => employeeNum; set => employeeNum = value; }
        public double Total { get => total; set => total = value; }
        public int Status { get => status; set => status = value; }
        public string Image { get => image; set => image = value; }
        public string OpenHour { get => openHour; set => openHour = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int DriverNum { get => driverNum; set => driverNum = value; }

        public int Insert()//insert new order
        {
            OrderDataServices ds = new OrderDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public List<Orders> Read()//get the all  orders
        {
            OrderDataServices ds = new OrderDataServices();
            List<Orders> listOrder = ds.Read();
            return listOrder;
        }

        public List<Orders> Read(int idOrder)//get the product on order
        {
            OrderDataServices ds = new OrderDataServices();
            List<Orders> listOrder = ds.Read(idOrder);
            return listOrder;
        }

        public List<Orders> ReadgetImage(int idOrder)//get image on this order - APP
        {
            OrderDataServices ds = new OrderDataServices();
            List<Orders> listOrder = ds.ReadgetImage(idOrder);
            return listOrder;
        }

        public List<Orders> Read(string preparationDate,int id)//get order with the date  - APP
        {
            OrderDataServices ds = new OrderDataServices();
            List<Orders> listOrder = ds.Read(preparationDate,id);
            return listOrder;
        }


        public bool Update()//update the order - status + image
        {
            OrderDataServices ds = new OrderDataServices();
            return ds.Update(this);

        }

        public List<Orders> Delete(int id, int Norder)//delete product frop order
        {
            OrderDataServices ds = new OrderDataServices();
            return ds.Delete(id, Norder);
        }
    }

}