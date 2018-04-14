using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA.Clases;
namespace ProyectoED1.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
        public ArbolB<Contenido, string, string> WatchList = new ArbolB<Contenido, string, string>(3);
        
    }
}