using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleApi.Test.Maps.DistanceMatrix;
using Jacobs.Models;

namespace Jacobs.Controllers
{
    public class CompaniesController : ApiController
    {
        // GET api/<controller>
        public List<Company> Get()//get all the company
        {
            Company company = new Company();
            return company.Read();
        }

        // GET api/<controller>/5
        public List<Company> Get(string name)//get the info about this company
        {
            Company company = new Company();
            return company.Read(name);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Company company) //insert new company
        {
            
        
        int id = company.Insert();

        DistanceMatrixController dM = new DistanceMatrixController();
            //new distance matrix
            List<DistanceMatrix> final=dM.Get(company.DistributaionArea);
            insertToDb(final,dM);
                    
            //call to distance matrix that create
            //send cooridnation
            //here to call to calcuate distancematrix and post to db
            return Request.CreateResponse(HttpStatusCode.OK,"success");
        }
        public void insertToDb(List<DistanceMatrix> final, DistanceMatrixController dM)
        {
            foreach (var i in final)
            {
                DistanceMatrix postDm = new DistanceMatrix();

                postDm.Id = i.Id;
                postDm.From = i.From;
                postDm.To = i.To;
                postDm.Distance = i.Distance;

                dM.Post(postDm);
            }
        }
        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Company company)//update contact in the company
        {
            company.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}