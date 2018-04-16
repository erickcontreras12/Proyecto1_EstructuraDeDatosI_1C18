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
using TDA.Clases;
using TDA.Interfaces;

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
    
        /// <summary>
        /// Muestra la vista de carga
        /// </summary>
        /// <returns></returns>
        public ActionResult Carga()
        {
            if (!db.adminadentro)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        private void enorden(NodoIndividual<Contenido> actual)
        {
            db.filmes.Insertados.Add(actual.valor);
        }
        private void enorden1(NodoIndividual<Usuario> actual)
        {
            db.registrados.Insertados.Add(actual.valor);
        }

        //Creaciones de los arboles ordenados por cada tipo
        public ActionResult CrearDocus()
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

            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Documental")
                {
                    db.Docu_Nombre.Insertar(item);
                  
                }
            }
         foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Documental")
                {
                 
                    db.Docu_Genero.Insertar(item);
                 
                }
            }
            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Documental")
                {
                   
                    db.Docu_Anio.Insertar(item);
                }

            }
            

            return RedirectToAction("Carga");
        }

        public ActionResult CrearPelis()
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

            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo=="Pelicula")
                {
                    db.Peliculas_Nombre.Insertar(item);

                }
                
            }
            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Pelicula")
                {
                   
                    db.Peliculas_Genero.Insertar(item);
                   
                }
            }
            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Pelicula")
                {
                    
                    db.Peliculas_Anio.Insertar(item);
                }
            }

            return RedirectToAction("Carga");
        }

        public ActionResult CrearSeries()
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

            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Serie")
                {
                    db.Series_Nombre.Insertar(item);
                    
                }
            }
           foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Serie")
                {
                    
                    db.Series_Genero.Insertar(item);
                    
                }

            }
            foreach (Contenido item in db.filmes.Insertados)
            {
                if (item.Tipo == "Serie")
                {                    
                    db.Series_Anio.Insertar(item);
                }
            }
            
            return RedirectToAction("Carga");
        }
        /// <summary>
        /// Crea la watchlist del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult miwatch()
        {
            //Crear cada archivo en la carpeta especificada
            string ruta = Server.MapPath("~/WatchListUsuarios/");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            var x = JsonConvert.SerializeObject(db.publico.WatchList.Insertados.Distinct().ToList());
            StreamWriter watch = new StreamWriter(ruta + "\\" +db.publico.Username+ "_Watchlist.json");
            watch.Write(x);
            watch.Close();

            return RedirectToAction("WatchList", "Login");

        }

        public ActionResult usu()
        {
            string ruta = Server.MapPath("~/usuariosGenerales/");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            var x = JsonConvert.SerializeObject(db.auxregistrados.Distinct().ToList());
            StreamWriter u = new StreamWriter(ruta + "Usuarios.json");
            u.Write(x);
            u.Close();

            return RedirectToAction("Users", "Login");

        }


        /// <summary>
        /// Carga principal de las peliculas,series,documentales
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
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
                                                      
                                Insertar(y);
                                                     
                            break;
                           
                        }

                    }
                    db.filmes.Insertados.Clear();
                    db.filmes.recorrer(enorden);
                    ViewBag.Message = "Cargado Exitosamente";

                }
                catch(Exception e)
                {

                    ViewBag.Message1 = "Dato erroneo.";
                }
            }
                return View();
            

        }
        /// <summary>
        /// Carga del Archivo json de usuarios
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult cargauser(HttpPostedFileBase postedFile)
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
                            db.registrados.FuncionObtenerLlavePrincipal = ObtenerUser;
                            db.registrados.FuncionObtenerLlave = ObtenerNombre;
                            db.registrados.FuncionCompararLlavePrincipal = CompararUser;
                            db.registrados.FuncionCompararLlave = CompararNombre;

                            Usuario y = JsonConvert.DeserializeObject<Usuario>(jsonOperaciones.ToString());
                            y.WatchList = new ArbolB<Contenido, string, string>(4);
                            db.auxregistrados.Add(y);
                            db.registrados.Insertar(y);

                            break;

                        }

                    }
                    db.registrados.Insertados.Clear();
                    db.registrados.recorrer(enorden1);
                    ViewBag.Message = "Cargado Exitosamente";

                }
                catch (Exception e)
                {

                    ViewBag.Message1 = "Dato erroneo.";
                }
            }
            return RedirectToAction("Users", "Login");

        }

        public static string ObtenerUser(Usuario dato)
        {
            return dato.Username;
        }

        public static string ObtenerNombre(Usuario dato)
        {
            return dato.Nombre;
        }

        public static int CompararUser(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        public static int CompararNombre(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        public void Insertar(Contenido contenido)
        {
            db.filmes.FuncionObtenerLlavePrincipal = ObtenerNombreC;
            db.filmes.FuncionObtenerLlave = ObtenerGenero;
            db.filmes.FuncionCompararLlavePrincipal = CompararNombreC;
            db.filmes.FuncionCompararLlave = CompararGenero;

            if (contenido.Tipo == "Documental")
            {                
               
                db.filmes.Insertar(contenido);
                
            }
            else if (contenido.Tipo == "Serie")
            {
              
                db.filmes.Insertar(contenido);
               
            }
            else if (contenido.Tipo == "Pelicula")
            {
  
                db.filmes.Insertar(contenido);
             
            }

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
