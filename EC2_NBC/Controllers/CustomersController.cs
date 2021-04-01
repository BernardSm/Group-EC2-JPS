using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EC2_NBC.Helper;
using EC2_NBC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EC2_NBC.Controllers
{
    public class CustomersController : Controller
    {
        NCBApi _api = new NCBApi();
        public async Task<IActionResult> Index()
        {
            IList<Customer> customers = new List<Customer>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("api/Customers");
            if(response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customer>>(results);
            }
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            HttpClient client = _api.Initial();

            //Http Post
            var postTask = client.PostAsJsonAsync<Customer>("api/Customers", customer);
            postTask.Wait();

            var result = postTask.Result;
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            var customer = new Customer();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync($"api/Customers/{Id}");
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customer>(result);
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var customer = new Customer();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"api/Customers/{Id}");

            return RedirectToAction("Index");
        }
    }
}