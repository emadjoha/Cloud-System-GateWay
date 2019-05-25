using DS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace DS_System.Controllers
{
    public class GateWayController : ApiController
    {
        HttpClient client = new HttpClient();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public JsonResult Get(String id)
        {
         
            client.BaseAddress = new Uri("http://localhost:8083/api/info/"+id);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                //var data = response.Content.ReadAsAsync<IEnumerable<String>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                System.Diagnostics.Debug.WriteLine("Data is : {0}", response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
           return new JsonResult() { Data = response.Content.ReadAsAsync<IEnumerable<InfoModel>>().Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GateWay/add/{name}/{size}/{uid}/{isFolder}/{current}")]
        public JsonResult add(string name,string size,string uid,int isFolder,int current)
        {
            client.BaseAddress = new Uri("http://localhost:8083/api/files/"+name+"/"+size+"/"+uid+"/"+isFolder+"/"+current);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                //var data = response.Content.ReadAsAsync<IEnumerable<String>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                System.Diagnostics.Debug.WriteLine("Data is : {0}", response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return new JsonResult() { Data = response.Content.ReadAsStringAsync().Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // POST api/<controller>
        public void Post(string value)
        {
           

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