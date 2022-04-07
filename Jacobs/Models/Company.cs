using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;

namespace Jacobs.Models
{
    public class Company
    {
        int companyNum;
        string companyName;
        string address;
        string openHour;
        string closeHour;
        string nameContact;
        string phoneContact;
        string distributaionArea;
        double lng;
        double lat;
        



        public Company() { }

        public Company(string nameContact, string phoneContact)
        {
            this.nameContact = nameContact;
            this.phoneContact = phoneContact;
        }

   
        public Company(string companyName, string address,string openHour,string closeHour, string nameContact, string phoneContact,string distributaionArea, double lat, double lng)
        {
            this.companyName = companyName;
            this.address = address;
            this.openHour = openHour;
            this.closeHour = closeHour;
            this.nameContact = nameContact;
            this.phoneContact = phoneContact;
            this.distributaionArea = distributaionArea;
            this.lat = lat;
            this.lng = lng;
          
        }

        public Company(int companyNum, string companyName, string address, string openHour, string closeHour, string nameContact, string phoneContact)
        {
            this.companyNum = companyNum;
            this.companyName = companyName;
            this.address = address;
            this.openHour = openHour;
            this.closeHour = closeHour;
            this.nameContact = nameContact;
            this.phoneContact = phoneContact;
        }

        public int CompanyNum { get => companyNum; set => companyNum = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string Address { get => address; set => address = value; }
        public string OpenHour { get => openHour; set => openHour = value; }
        public string CloseHour { get => closeHour; set => closeHour = value; }
        public string NameContact { get => nameContact; set => nameContact = value; }
        public string PhoneContact { get => phoneContact; set => phoneContact = value; }
        public double Lng { get => lng; set => lng = value; }
        public double Lat { get => lat; set => lat = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }

        public int Insert()//insert new company
        {
            CompanyDataServices ds = new CompanyDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public List<Company> Read()//get the all company
        {
            CompanyDataServices ds = new CompanyDataServices();
            List<Company> Companieslist = ds.Read();
            return Companieslist;
        }
     



        public List<Company> Read(string name)//get the info about this company
        {
            CompanyDataServices ds = new CompanyDataServices();
            List<Company> Companieslist = ds.Read(name);
            return Companieslist;
        }

        public bool Update()//update this company
        {
            CompanyDataServices ds = new CompanyDataServices();
            return ds.Update(this);

        }

    }

}




