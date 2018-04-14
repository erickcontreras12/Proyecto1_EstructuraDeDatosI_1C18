using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContext;
using ProyectoED1.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ProyectoED1.Controllers
{
    public class CargaController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;

        // GET: Carga
        public ActionResult Index()
        {
            return View();
        }
    

        public ActionResult Carga()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase postedFile)
        {

            if (postedFile != null)
            {

                string filepath = string.Empty;

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);

                try
                {

                    JArray json = JArray.Parse(csvData);

                    foreach (JObject jsonOperaciones in json.Children<JObject>())
                    {

                        foreach (JProperty property in jsonOperaciones.Properties())
                        {
                          
                            Contenido y = JsonConvert.DeserializeObject<Contenido>(jsonOperaciones.ToString());
                            
                            if (y.Tipo == "Documental")
                            {
                                db.Docu_Nombre.FuncionObtenerLlavePrincipal = ObtenerNombreC;
                                db.Docu_Nombre.FuncionObtenerLlave = ObtenerGenero;
                                db.Docu_Nombre.FuncionCompararLlavePrincipal = CompararNombreC;
                                db.Docu_Nombre.FuncionCompararLlave = CompararGenero;

                                db.Docu_Genero.FuncionObtenerLlavePrincipal = ObtenerGenero;
                                db.Docu_Genero.FuncionObtenerLlave = ObtenerNombreC;
                                db.Docu_Genero.FuncionCompararLlavePrincipal = CompararGenero;
                                db.Docu_Genero.FuncionCompararLlave = CompararNombreC;

                                db.Docu_Anio.FuncionObtenerLlavePrincipal = ObtenerAnio;
                                db.Docu_Anio.FuncionObtenerLlave = ObtenerNombreC;
                                db.Docu_Anio.FuncionCompararLlavePrincipal = CompararAnio;
                                db.Docu_Anio.FuncionCompararLlave = CompararNombreC;

                                db.filmes.Add(y);
                                db.Docu_Nombre.Insertar(y);
                                db.Docu_Genero.Insertar(y);
                                db.Docu_Anio.Insertar(y);
                                break;
                            }
                            else if (y.Tipo == "Serie")
                            {
                                db.Series_Nombre.FuncionObtenerLlavePrincipal = ObtenerNombreC;
                                db.Series_Nombre.FuncionObtenerLlave = ObtenerGenero;
                                db.Series_Nombre.FuncionCompararLlavePrincipal = CompararNombreC;
                                db.Series_Nombre.FuncionCompararLlave = CompararGenero;

                                db.Series_Genero.FuncionObtenerLlavePrincipal = ObtenerGenero;
                                db.Series_Genero.FuncionObtenerLlave = ObtenerNombreC;
                                db.Series_Genero.FuncionCompararLlavePrincipal = CompararGenero;
                                db.Series_Genero.FuncionCompararLlave = CompararNombreC;

                                db.Series_Anio.FuncionObtenerLlavePrincipal = ObtenerAnio;
                                db.Series_Anio.FuncionObtenerLlave = ObtenerNombreC;
                                db.Series_Anio.FuncionCompararLlavePrincipal = CompararAnio;
                                db.Series_Anio.FuncionCompararLlave = CompararNombreC;

                                db.filmes.Add(y);
                                db.Series_Nombre.Insertar(y);
                                db.Series_Genero.Insertar(y);
                                db.Series_Anio.Insertar(y);
                                break;
                            }
                            else if (y.Tipo == "Pelicula")
                            {
                                db.Peliculas_Nombre.FuncionObtenerLlavePrincipal = ObtenerNombreC;
                                db.Peliculas_Nombre.FuncionObtenerLlave = ObtenerGenero;
                                db.Peliculas_Nombre.FuncionCompararLlavePrincipal = CompararNombreC;
                                db.Peliculas_Nombre.FuncionCompararLlave = CompararGenero;

                                db.Peliculas_Genero.FuncionObtenerLlavePrincipal = ObtenerGenero;
                                db.Peliculas_Genero.FuncionObtenerLlave = ObtenerNombreC;
                                db.Peliculas_Genero.FuncionCompararLlavePrincipal = CompararGenero;
                                db.Peliculas_Genero.FuncionCompararLlave = CompararNombreC;

                                db.Peliculas_Anio.FuncionObtenerLlavePrincipal = ObtenerAnio;
                                db.Peliculas_Anio.FuncionObtenerLlave = ObtenerNombreC;
                                db.Peliculas_Anio.FuncionCompararLlavePrincipal = CompararAnio;
                                db.Peliculas_Anio.FuncionCompararLlave = CompararNombreC;

                                db.filmes.Add(y);
                                db.Peliculas_Nombre.Insertar(y);
                                db.Peliculas_Genero.Insertar(y);
                                db.Peliculas_Anio.Insertar(y);
                                break;
                            }

                        }

                    }
                    ViewBag.Message = "Cargado Exitosamente";

                }
                catch
                {
                    ViewBag.Message1 = "Dato erroneo.";
                }
            }
                return View();
            

        }


        public static int CompararUser(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        public static int CompararNombre(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        public static string ObtenerNombreC(Contenido dato)
        {
            return dato.Nombre;
        }

        public static string ObtenerAnio(Contenido dato)
        {
            return dato.Anio_Lanzamiento;
        }
        public static string ObtenerGenero(Contenido dato)
        {
            return dato.Genero;
        }

        public static int CompararNombreC(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }
        public static int CompararAnio(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }
        public static int CompararGenero(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        // GET: Carga/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carga/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carga/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carga/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carga/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carga/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carga/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
