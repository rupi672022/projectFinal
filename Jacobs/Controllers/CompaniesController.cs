using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Models;

namespace System.Controllers
{
    public class CompaniesController : ApiController
    {
        // GET api/<controller>
        public List<Company> Get()
        {
            Company company = new Company();
            return company.Read();
        }

        // GET api/<controller>/5
        public List<Company> Get(string name)
        {
            Company company = new Company();
            return company.Read(name);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Company company) //insert user to table
        {
            int id = company.Insert();
            return Request.CreateResponse(HttpStatusCode.OK,"success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Company company)
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