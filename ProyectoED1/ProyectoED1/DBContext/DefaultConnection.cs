using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoED1.Models;
using TDA.Clases;

namespace ProyectoED1.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();
        public List<Usuario> auxregistrados = new List<Usuario>();
        
        public int IDActual { get; set; }
        public Usuario publico = new Usuario();
       public List<Contenido> auxfilmes = new List<Contenido>();

        //Arboles que se crean
        public ArbolB<Contenido, string, string> filmes = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Usuario, string, string> registrados = new ArbolB<Usuario, string, string>(4);
        public ArbolB<Contenido, string, string> Series_Nombre = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Series_Genero = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Series_Anio = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Peliculas_Nombre = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Peliculas_Genero = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Peliculas_Anio = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Docu_Nombre = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Docu_Genero = new ArbolB<Contenido, string, string>(4);
        public ArbolB<Contenido, string, string> Docu_Anio = new ArbolB<Contenido, string, string>(4);


        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}