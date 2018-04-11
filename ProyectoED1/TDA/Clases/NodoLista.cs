using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;

namespace TDA.Clases
{
    public class NodoLista<T> : Nodo<T>
    {
        public NodoIndividual<T>[] valores;
        public NodoLista<T> Padre;

        public NodoLista(int k)
        {
            this.valores = new NodoIndividual<T>[k - 1];
        }

    }
}
