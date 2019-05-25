using DS_System.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace DS_System.Controllers
{
    public class RegisterController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [System.Web.Http.Route("api/Register/add")]
        [System.Web.Http.HttpPost]
        public string add([FromBody]string id)
        {
            return "id"+id;
        }



        // POST api/<controller>
        public JsonResult Post([FromBody]UserRegisterModel user)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000");
            var response = client.PostAsJsonAsync("/api/users",user).Result;

            if (response.IsSuccessStatusCode)
            {
                return new JsonResult() { Data = response.Headers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult() { Data = "user no register", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}