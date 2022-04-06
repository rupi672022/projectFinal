using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;

namespace Jacobs.Models
{
    public class Orders
    {
        int orderNum;
        string startDate;
        string dateArrival;
        int companynum;
        double weight;//משקל סה"כ למוצר
        int quantity;//אם לא צריך לשקול
        double total;
        int productnum;
        int employeeNum;
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

        public Orders(int orderNum, double weight, int quantity, double total, int productnum)
        {
            this.orderNum = orderNum;
            this.weight = weight;
            this.quantity = quantity;
            this.total = total;
            this.productnum = productnum;
        }

        public Orders(int orderNum, int employeeNum, string preprationDate,int status)
        {
            this.orderNum = orderNum;
            this.employeeNum = employeeNum;
            this.preprationDate = preprationDate;
            this.status = status;
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

        public int Insert()//insert new order
        {
            OrderDataServices ds = new OrderDataServices();
            int status = ds.Insert(this);
            return status;
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