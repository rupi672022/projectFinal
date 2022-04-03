using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Models.DAL;

namespace System.Models
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
        int status = 1;

        public Products() { }

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

        public int Insert()
        {
            ProductDataServices ds = new ProductDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public List<Products> Read()
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read();
            return productslist;
        }

        public List<Products> Read(string name)
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read(name);
            return productslist;
        }

        public List<Products> Read(int id)
        {
            ProductDataServices ds = new ProductDataServices();
            List<Products> productslist = ds.Read(id);
            return productslist;
        }

        public bool Update()//insert the article to the table
        {
            ProductDataServices ds = new ProductDataServices();
            return ds.Update(this);

        }


    }
}