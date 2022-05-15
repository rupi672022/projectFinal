using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;

namespace Jacobs.Models
{
    public class Products
    {
        int barcod;
        string nameProduct;
        string type;
        int quantity;
        double weightAll;
        double weightperOne;
        double downfromTotal;
        int orderNum;
        int status = 1;

        public Products() { }

        public Products(int barcod, string nameProduct,int orderNum)
        {
            this.barcod = barcod;
            this.nameProduct = nameProduct;
            this.orderNum = orderNum;
        }

        public Products(int barcod, string nameProduct, string type, int status)
        {
            this.barcod = barcod;
            this.nameProduct = nameProduct;
            this.type = type;
            this.status = status;
        }

        public Products(int barcod, string nameProduct, string type,int quantity, double weightAll, double weightperOne, double downfromTotal)
        {
            this.barcod = barcod;
            this.nameProduct = nameProduct;
            this.type = type;
            this.quantity = quantity;
            this.weightAll = weightAll;
            this.weightperOne = weightperOne;
            this.downfromTotal=downfromTotal;
        }

        public int Barcod { get => barcod; set => barcod = value; }
        public string NameProduct { get => nameProduct; set => nameProduct = value; }
        public string Type { get => type; set => type = value; }
        public double WeightperOne { get => weightperOne; set => weightperOne = value; }
        public double DownfromTotal { get => downfromTotal; set => downfromTotal = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double WeightAll { get => weightAll; set => weightAll = value; }
        public int Status { get => status; set => status = value; }
        public int OrderNum { get => orderNum; set => orderNum = value; }

        public int Insert()//insert new product
        {
            ProductDataServices ds = new ProductDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public List<Products> Read()//get all the product
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read();
            return productslist;
        }

        public List<Products> getproducts()
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.getproducts();
            return productslist;
        }

        public List<Products> Read(string name)//get the info to this product
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read(name);
            return productslist;
        }

        public List<Products> Read(int id)//get the product to this order
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read(id);
            return productslist;
        }

        public bool Update()//update prosuct - status - app
        {
            ProductDataServices ds = new ProductDataServices();
            return ds.Update(this);

        }


    }
}