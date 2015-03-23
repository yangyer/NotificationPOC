using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolarWindDemo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NotifyClients(string dateTime, string id)
        {
            Responder.Instance.NotifyClients("Alert[" + dateTime + "] ID: " + id);

            return View("Index");
        }
    }
}