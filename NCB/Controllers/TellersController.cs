using NCB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace NCB.Controllers
{
    public class TellersController : Controller
    {
        // GET: Tellers
        public ActionResult Index()
        {
            IEnumerable<mvcTellerModel> tellList;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Tellers").Result;
            tellList = response.Content.ReadAsAsync<IEnumerable<mvcTellerModel>>().Result;
            return View(tellList);
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcTellerModel());
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Tellers/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcTellerModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(mvcTellerModel tell)
        {
            if (tell.Id == 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Tellers", tell).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Tellers/" + tell.Id, tell).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Tellers/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}