using ProyectoED1.DBContext;
using ProyectoED1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoED1.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        public ActionResult Index()
        {
            //Simulacion de cierre de sesion
            db.adminadentro = false;
            db.publico = new Usuario();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}