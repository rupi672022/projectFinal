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
        public void Get()
        {
            List<DistanceMatrix> list = new List<DistanceMatrix>();
            DistanceMatrix dm = new DistanceMatrix();
            list=dm.Read();
            dm.Setup();
          //  dm.test(list);
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