﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jacobs.Models;

namespace Jacobs.Controllers
{
    public class FindingPathsController : ApiController
    {
        // GET api/<controller>
        public List<FindingPaths> Get(string date)
        {
            FindingPaths findingPath = new FindingPaths();
            return findingPath.Read(date);
        }

        // GET api/<controller>/5
        //public List<FindingFaths> Get(string name)
        //{
        //    FindingFaths findingFath = new FindingFaths();
        //    return findingFath.Read(name);
        //}


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