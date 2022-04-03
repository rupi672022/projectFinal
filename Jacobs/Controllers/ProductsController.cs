using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Models;

namespace System.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/<controller>
        public List<Products> Get()
        {
            Products product = new Products();
            return product.Read();
        }

        // GET api/<controller>/5
        public List<Products> Get(string name)
        {
            Products product = new Products();
            return product.Read(name);
        }

        public List<Products> Get(int id)
        {
            Products product = new Products();
            return product.Read(id);
        }


        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Products product)
        {
            int id = product.Insert();
            return Request.CreateResponse(HttpStatusCode.OK,"success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Products product)
        {
            product.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}