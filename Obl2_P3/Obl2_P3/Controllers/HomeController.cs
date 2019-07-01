using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obl2_P3.Utilities;

namespace Obl2_P3.Controllers
{
    public class HomeController : Controller
    {
        #region Logic

        #region GET

        public ActionResult Index()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View();
        }

        public ActionResult About()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View();
        }

        #endregion

        #endregion
    }
}