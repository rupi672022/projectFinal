using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jacobs.Models;
using GoogleApi.Test.Maps.DistanceMatrix;

namespace Jacobs.Controllers
{
    public class FindingPathsController : ApiController
    {

        // GET api/<controller>
        
        public List<FindingPaths> Get(string date, int DriverName)
        {
            FindingPaths findingPath = new FindingPaths();
            OrdersController oC = new OrdersController();
            //new distance matrix

            List<Orders> final = oC.GetBox(date);
           
            return findingPath.Read(date, DriverName);
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