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
    public class ProductsController : ApiController
    {
        // GET api/<controller>
        public List<Products> Get()//get all product
        {
            Products product = new Products();
            return product.Read();
        }

        [HttpGet]
        [Route("api/Product")]
        public List<Products> getproducts()//get all product
        {
            Products product = new Products();
            return product.getproducts();
        }
        // GET api/<controller>/5
        public List<Products> Get(string name)//get the info product
        {
            Products product = new Products();
            return product.Read(name);
        }

        public List<Products> Get(int id)//get the product from this order
        {
            Products product = new Products();
            return product.Read(id);
        }


        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Products product)//insert new product
        {
            int id = product.Insert();
            return Request.CreateResponse(HttpStatusCode.OK,"success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Products product)//update product - status :  - app
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