using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamBuilding.Models;

namespace TeamBuilding.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// POST: Login
        /// </summary>
        public ActionResult ThemeCreate(UserViewModels userViewModels)
        {
            string name = userViewModels.Name;
            if (name == "admin")
            {
                return RedirectToAction("Result");
            } else
            {
                // To register the name for DB


                // To get the all names for DB


                return View();
            }
        }

        /// <summary>
        /// GET: Login/Result
        /// </summary>
        public ActionResult Result()
        {
            // To get all votes from DB


            return View();
        }
    }
}