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
            Dictionary<int,string> addressList = new Dictionary<int,string>();

            DistanceMatrix dm = new DistanceMatrix();

            list=dm.Read(area);
            int count = 0;
            foreach (DistanceMatrix i in list)
            {
                if(count<9)
                {
                    addressList.Add(i.CompanyNum,i.Address);
                }
                count++;

            }
            dm.Setup();

            return dm.test(addressList,area);

            //return dm.Read();
        }

        // GET api/<controller>/5
       

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] DistanceMatrix distanceMatrix, List<DistanceMatrix> final)
        {
            int id = distanceMatrix.Insert(final);
           //num הוא אפס צריך לבדוק מה עושים ש
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