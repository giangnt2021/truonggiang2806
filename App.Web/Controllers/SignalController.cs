using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    public class SignalController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
