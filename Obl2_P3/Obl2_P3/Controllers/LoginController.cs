using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Clases;
using Dominio.Repositorios;
using Obl2_P3.Models;

namespace Obl2_P3.Controllers
{
    public class LoginController : Controller
    {
        #region RepoInstances
        RepoUsuario ru = new RepoUsuario();
        RepoPostulante rp = new RepoPostulante();
        #endregion

        #region Logic

        #region GET

        //GET: Login/Index
        public ActionResult Index()
        {
            return View();
        }

        //GET: Login/Logout
        public ActionResult Logout()
        {
            Session.Remove("userLog");
            Session.Remove("userRole");
            return RedirectToAction("Index", "Login", new VMUsuario());
        }

        #endregion

        #region POST

        //POST: Login/Index
        [HttpPost]
        public ActionResult Index(VMUsuario user)
        {
            if (ModelState.IsValid && user.validarModel())
            {
                var uAux = ru.findByCi(user.cedula);
                var pAux = rp.findByCi(user.cedula);

              
                if (pAux != null && uAux != null)
                {
                    uAux = null;
                }

                if (uAux == null)
                {
                    Session["userLog"] = pAux;
                    Session["userRole"] = pAux.getRole();
                    if (pAux.adjudicatario)
                    {
                        Session["adj"] = "si";
                    }

                    return RedirectToAction("Index", "Home");

                }else if (pAux == null)
                {
                    Session["userLog"] = uAux;
                    Session["userRole"] = uAux.getRole();

                    return RedirectToAction("Index", "Home");
                }

                else return View(user);
            }
            else return View(user);
        }

        #endregion

        #endregion
    }
}