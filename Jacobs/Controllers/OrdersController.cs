using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jacobs.Models;

namespace Jacobs.Controllers
{
    public class OrdersController : ApiController
    {
        // GET api/<controller>
        public List<Orders> Get()//get the product on order
        {
            Orders order = new Orders();
            return order.Read();
        }

        public List<Orders> Get(int idOrder)//get the product on order
        {
            Orders order = new Orders();
            return order.Read(idOrder);
        }

        [HttpGet]
        [Route("api/Orders/{idOrder}")]
        public List<Orders> getImage(int idOrder)//get image on this order - APP
        {
            Orders order = new Orders();
            return order.ReadgetImage(idOrder);
        }

        public List<Orders> Get(string preparationDate,int id)//get the order with this date to make - APP
        {
            Orders order = new Orders();
            return order.Read(preparationDate,id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Orders order) //insert new order
        {
            int id = order.Insert();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Orders order)//update the order - status : 0 + image 
        {
            order.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPut]
        [Route("api/Orders/{collectOrder}")]
        public HttpResponseMessage PutEmploye([FromBody] Orders order)//update the order - status : 0 + image 
        {
            order.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // DELETE api/<controller>/5
        public List<Orders> Delete(int id, int Norder)//delete product from order 
        {
            Orders order = new Orders();
            return order.Delete(id, Norder);
        }
    }
}