using NCB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace NCB.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            IEnumerable<mvcCustomerModel> cusList;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Customers").Result;
            cusList = response.Content.ReadAsAsync<IEnumerable<mvcCustomerModel>>().Result;
            return View(cusList);
        }

       
        public ActionResult AddorEdit(int id = 0)
        {
            if(id==0)
                return View(new mvcCustomerModel());
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Customers/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcCustomerModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(mvcCustomerModel cus)
        {
            if(cus.UserID == 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Customers", cus).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Customers/"+cus.UserID, cus).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
           
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Customers/"+id.ToString() ).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}