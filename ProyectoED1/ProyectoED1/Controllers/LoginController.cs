using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContext;
using ProyectoED1.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProyectoED1.Controllers
{
    public class LoginController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {

            db.registrados.Insertados.Sort(delegate (Usuario x, Usuario y)
            {           
            return x.Username.CompareTo(y.Username);
            });

            return View(db.registrados.Insertados.ToList());
        }

        public ActionResult Administrador()
        {
            return View();
        }

        public ActionResult Logeado()
        {
            return View();
        }

        public ActionResult Logear(string user, string contra)
        {
            if (user == "admin" && contra == "admin")
            {
                return RedirectToAction("Administrador");
            }
            else
            {
                Usuario logeado = db.auxregistrados.Find(x => x.Username == user);
                if (logeado == null)
                {
                    ViewBag.Message = "Usuario o Contrasenia invalido";
                    return View("Index");
                }
                db.publico = logeado;
                return RedirectToAction("Logeado");

            }
        }

        public ActionResult Cerrar()
        {
            db.publico = null;
            return View("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult registro([Bind(Include = "Nombre,Apellido,Edad,Username,Password")] Usuario user, string Username)
        {
            try
            {
                Usuario logeado = db.auxregistrados.Find(x => x.Username == Username);
                
                if (logeado != null)
                {
                    ViewBag.Message = "Usuario existente";
                    return View();
                }
                else {
                    db.registrados.FuncionObtenerLlavePrincipal = ObtenerUser;
                    db.registrados.FuncionObtenerLlave = ObtenerNombre;
                    db.registrados.FuncionCompararLlavePrincipal = CompararUser;
                    db.registrados.FuncionCompararLlave = CompararNombre;
                    // TODO: Add insert logic here
                    db.auxregistrados.Add(user);
                    db.registrados.Insertar(user);
                    string output = JsonConvert.SerializeObject(user);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CrearContenido()
        {
            return View();
        }

        public ActionResult Catalogo(string id)
        {
            List<Contenido> buscados;
            if (id==null)
            {
                return View(db.filmes.ToList());
            }
            else
            {
               buscados = db.filmes.FindAll(x => x.Nombre == id);
                if (buscados.Count() == 0)
                {
                    buscados = db.filmes.FindAll(x => x.Anio_Lanzamiento == id);

                    if (buscados.Count==0)
                    {
                        buscados = db.filmes.FindAll(x => x.Genero == id);
                    }
                }
                return View(buscados.ToList());
            }
            
        }

        public ActionResult WatchList()
        {
            db.publico.WatchList.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Nombre.CompareTo(y.Nombre);
            });

            return View(db.publico.WatchList.Insertados.ToList());
        }

        public ActionResult agregar(string id)
        {
            Contenido aux = db.filmes.Find(x => x.Nombre == id);
            db.publico.WatchList.FuncionObtenerLlavePrincipal = ObtenerNombreC;
            db.publico.WatchList.FuncionObtenerLlave = ObtenerGenero;
            db.publico.WatchList.FuncionCompararLlavePrincipal = CompararNombreC;
            db.publico.WatchList.FuncionCompararLlave = CompararGenero;
            db.publico.WatchList.Insertar(aux);

            return RedirectToAction("Catalogo");
        }

        public ActionResult Archivo()
        {
            db.Peliculas_Nombre.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Nombre.CompareTo(y.Nombre);
            });

            db.Peliculas_Genero.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Genero.CompareTo(y.Genero);
            });

            db.Peliculas_Anio.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Anio_Lanzamiento.CompareTo(y.Anio_Lanzamiento);
            });

            db.Series_Nombre.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Nombre.CompareTo(y.Nombre);
            });

            db.Series_Genero.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Genero.CompareTo(y.Genero);
            });

            db.Series_Anio.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Anio_Lanzamiento.CompareTo(y.Anio_Lanzamiento);
            });

            db.Docu_Nombre.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Nombre.CompareTo(y.Nombre);
            });

            db.Docu_Genero.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Genero.CompareTo(y.Genero);
            });

            db.Docu_Anio.Insertados.Sort(delegate (Contenido x, Contenido y)
            {
                return x.Anio_Lanzamiento.CompareTo(y.Anio_Lanzamiento);
            });

            var s = JsonConvert.SerializeObject(db.Peliculas_Nombre.Insertados);
            var a = JsonConvert.SerializeObject(db.Peliculas_Genero.Insertados);
            var b = JsonConvert.SerializeObject(db.Peliculas_Anio.Insertados);
            var c = JsonConvert.SerializeObject(db.Series_Nombre.Insertados);
            var d = JsonConvert.SerializeObject(db.Series_Genero.Insertados);
            var e = JsonConvert.SerializeObject(db.Series_Anio.Insertados);
            var f = JsonConvert.SerializeObject(db.Docu_Nombre.Insertados);
            var g = JsonConvert.SerializeObject(db.Docu_Genero.Insertados);
            var h = JsonConvert.SerializeObject(db.Docu_Anio.Insertados);

            List<Usuario> aux = db.registrados.Insertados;
            foreach (var item in aux)
            {
                item.WatchList = null;
            }
            var i = JsonConvert.SerializeObject(aux);

            string ruta = Server.MapPath("~/ArchivosJsonCreados/");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            StreamWriter pelis_Nombre= new StreamWriter(ruta + "\\PeliculasPorNombre.json");
            pelis_Nombre.Write(s);
            pelis_Nombre.Close();

            StreamWriter pelis_anio = new StreamWriter(ruta + "\\PeliculasPorAnio.json");
            pelis_anio.Write(b);
            pelis_anio.Close();

            StreamWriter pelis_genero = new StreamWriter(ruta + "\\PeliculasPorGenero.json");
            pelis_genero.Write(a);
            pelis_genero.Close();

            StreamWriter series_nombre = new StreamWriter(ruta + "\\SeriesPorNombre.json");
            series_nombre.Write(c);
            series_nombre.Close();

            StreamWriter series_anio = new StreamWriter(ruta + "\\SeriesPorAnio.json");
            series_anio.Write(d);
            series_anio.Close();

            StreamWriter series_genero = new StreamWriter(ruta + "\\SeriesPorGenero.json");
            series_genero.Write(e);
            series_genero.Close();

            StreamWriter docu_nombre = new StreamWriter(ruta + "\\DocumentalesPorNombre.json");
            docu_nombre.Write(f);
            docu_nombre.Close();

            StreamWriter docu_genero = new StreamWriter(ruta + "\\DocumentalesPorGenero.json");
            docu_genero.Write(g);
            docu_genero.Close();

            StreamWriter docu_anio = new StreamWriter(ruta + "\\DocumentalesPorAnio.json");
            docu_anio.Write(h);
            docu_anio.Close();

            StreamWriter usuarios = new StreamWriter(ruta + "\\Usuarios.json");
            usuarios.Write(i);
            usuarios.Close();
       

            if (db.registrados!=null)
            {
                foreach (var item in db.registrados.Insertados)
                {
                    var x = JsonConvert.SerializeObject(item.WatchList.Insertados);
                    StreamWriter watch = new StreamWriter(ruta + "\\" + item.Nombre + "_WatchList.json");
                    watch.Write(x);
                    watch.Close();
                }               
            }          

            return RedirectToAction("Administrador");
        }

        [HttpPost]
        public ActionResult CrearContenido([Bind(Include = "Tipo,Nombre,Anio_Lanzamiento,Genero")] Contenido film)
        {
            db.filmes.Add(film);

            if (film.Tipo == "Documental")
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

                db.Docu_Nombre.Insertar(film);
                db.Docu_Genero.Insertar(film);
                db.Docu_Anio.Insertar(film);

            }
            else if (film.Tipo=="Serie")
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

                db.Series_Nombre.Insertar(film);
                db.Series_Genero.Insertar(film);
                db.Series_Anio.Insertar(film);
            }
            else if(film.Tipo=="Pelicula")
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

                db.Peliculas_Nombre.Insertar(film);
                db.Peliculas_Genero.Insertar(film);
                db.Peliculas_Anio.Insertar(film);
            }
            
            return View("CrearContenido");
        }


        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Nombre,Apellido,Edad,Username,Password")] Usuario user)
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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

        //Todos los metodos siguiebtes son para la funcion comparar y obtener de los delegates de cada arbolito
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

    }
}
