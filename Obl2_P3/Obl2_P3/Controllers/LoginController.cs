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
            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };
            ViewBag.message = message;
            return View();
        }

        //GET: Login/Logout
        public ActionResult Logout()
        {
            Session.Remove("userLog");
            Session.Remove("userRole");
            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };
            ViewBag.message = message;
            return RedirectToAction("Index", "Login", new VMUsuario());
        }

        #endregion

        #region POST

        //POST: Login/Index
        [HttpPost]
        public ActionResult Index(VMUsuario user)
        {
            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Usuario incorrecto" };


            if (ModelState.IsValid && user.validarModel())
            {
                var uAux = ru.findByCi(user.cedula);
                var pAux = rp.findByCi(user.cedula);


                if (pAux != null && uAux != null)
                {
                    uAux = null;
                }

                if (pAux != null)
                {
                    Session["userLog"] = pAux;
                    Session["userRole"] = pAux.getRole();
                    Session["adj"] = "no";
                    if (pAux.adjudicatario)
                    {
                        Session["adj"] = "si";
                    }

                    return RedirectToAction("Index", "Home");

                }
                else if (uAux != null)
                {
                    Session["userLog"] = uAux;
                    Session["userRole"] = uAux.getRole();

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.message = message;
                    return View(user);
                }
            }
            else
            {
                ViewBag.message = message;
                return View(user);
            }
        }

        #endregion

        #endregion
    }
}