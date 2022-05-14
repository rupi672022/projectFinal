﻿using GoogleApi.Test.Maps.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jacobs.Controllers
{
    public class DistanceMatrixController : ApiController
    {
        // GET api/<controller>
        public void Get()
        {
            DistanceMatrixTests dm = new DistanceMatrixTests();
            dm.Setup();
            dm.test();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<DistanceMatrixTests> Get(string Area)
        {
            DistanceMatrixTests distanceMatrixTests = new DistanceMatrixTests();
            return distanceMatrixTests.Read(Area);
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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