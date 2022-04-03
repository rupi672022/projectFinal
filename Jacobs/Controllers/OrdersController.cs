using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Models;

namespace System.Controllers
{
    public class OrdersController : ApiController
    {
        // GET api/<controller>

        public List<Orders> Get(int idOrder)
        {
            Orders order = new Orders();
            return order.Read(idOrder);
        }

        [HttpGet]
        [Route("api/Orders/{idOrder}")]
        public List<Orders> getImage(int idOrder)
        {
            Orders order = new Orders();
            return order.ReadgetImage(idOrder);
        }

        public List<Orders> Get(string preparationDate,int id)
        {
            Orders order = new Orders();
            return order.Read(preparationDate,id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Orders order) //insert user to table
        {
            int id = order.Insert();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Orders order)
        {
            order.Update();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        // DELETE api/<controller>/5
        public List<Orders> Delete(int id, int Norder)
        {
            Orders order = new Orders();
            return order.Delete(id, Norder);
        }
    }
}