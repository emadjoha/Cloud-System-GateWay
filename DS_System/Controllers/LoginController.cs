using DS_System.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;

namespace DS_System.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:3000/api/auth");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<UserLoginModel>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {

                    System.Diagnostics.Debug.WriteLine("{0}", d.email);
                    Console.WriteLine("{0}", d.password);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

            return "TestController";
        }

        // POST api/<controller>
        
        public JsonResult Post([FromBody]UserLoginModel user)
        {
            HttpClient client = new HttpClient();
            UserLoginModel auth = new UserLoginModel { email = user.email, password = user.password };
            client.BaseAddress = new Uri("http://localhost:3000");
            var response = client.PostAsJsonAsync("/api/auth", auth).Result;

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("My name : {0}", response.IsSuccessStatusCode);
                System.Diagnostics.Debug.WriteLine("{0}", response.Headers.ToString());
                Console.Write("Success");

                return new JsonResult() { Data = response.Headers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult() { Data = "Invalid userName/Password", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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