using GoogleApi.Test.Maps.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jacobs.Models;

namespace Jacobs.Controllers
{
    public class DistanceMatrixController : ApiController
    {
        // GET api/<controller>
        public List<DistanceMatrix> Get(string area)
        {

            List<DistanceMatrix> list = new List<DistanceMatrix>();
            List<string> addressList = new List<string>();

            DistanceMatrix dm = new DistanceMatrix();

            list=dm.Read(area);
            int count = 0;
            foreach (DistanceMatrix i in list)
            {
                if(count<10)
                {
                    addressList.Add(i.Address);

                }
                count++;

            }
            dm.Setup();

            return dm.test(addressList);

            //return dm.Read();
        }

        // GET api/<controller>/5
        //public List<DistanceMatrix> Get(string addressMatrix)
        //{
        //    DistanceMatrix distanceMatrixTests = new DistanceMatrix();
        //    return distanceMatrixTests.Read(addressMatrix);
        //}

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] DistanceMatrix distanceMatrix)
        {
            int id = distanceMatrix.Insert();
            CompaniesController comp = new CompaniesController();
            //comp.insertToDb();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}