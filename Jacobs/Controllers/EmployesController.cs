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
    public class EmployesController : ApiController
    {
        // GET api/<controller>

        public List<Employees> Get()//get all the employes that Active
        {
            Employees employe = new Employees();
            return employe.Read();
        }

        public List<Employees> Get(int employeNum)//get the the employe with this number employe
        {
            Employees employe = new Employees();

            if (employeNum > 10000)//give employe
            {
                return employe.Read(employeNum);
            }
            else//get the employe to the order
            {
                return employe.ReadEmployeDriver();
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Employees employe)  //insert new employe
        {
            int id = employe.Insert();
            return Request.CreateResponse(HttpStatusCode.OK,"success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Employees employe)//update the info on employe
        {
            employe.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        // DELETE api/<controller>/5
        public List<Employees> Delete(int id)//delete this employe - just to update to 0
        {
            Employees employe = new Employees();
            return employe.Delete(id);
        }
    }
}