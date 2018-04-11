using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;

namespace TDA.Clases
{
    public class NodoIndividual<T>: Nodo<T>
    {
        public T valor;
        public NodoLista<T> derecho, izquierdo;

        public NodoIndividual()
        {
            this.valor = default(T);
            this.derecho = null;
            this.izquierdo = null;
        }
    }
}
