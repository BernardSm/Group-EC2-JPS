using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EC2_NBC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EC2_NBC.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<mvcCustomerModel> cusList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Customers").Result;
            cusList = response.Content.ReadAsAsync<IEnumerable<mvcCustomerModel>>().Result;
            return View(cusList);
        }

        public ActionResult Create(int id = 0)
        {
            return View(new mvcCustomerModel());
        }

        [HttpPost]
        public ActionResult Create(mvcCustomerModel cus)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Customers", cus).Result;
            return RedirectToAction("Index");
        }
       
    }
}