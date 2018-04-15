using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;
using TDA.Clases;
namespace TDA.Clases
{
    public class ArbolB<T, J, K>
    {
        


        public List<T> Buscar(Predicate<T> x)
        {

            List<T> buscados = Insertados.FindAll(x);

            return buscados.ToList();
        }

        public T Encontrar(Predicate<T> x)
        {

            T buscados = Insertados.Find(x);

            return buscados;
        }

        public void Eliminar(Predicate<T> x)
        {

            T elimado = Insertados.Find(x);
            Insertados.Remove(elimado);
            raiz = null;
            raiz = new NodoLista<T>(grado);
            List<T> aux = Insertados;
            Insertados = new List<T>();
            foreach (T item in aux)
            {
                Insertar(item);
            }

        }

        public void recorrer(RecorridoDlg<T> recorrido)
        {

            recorrer_interno(raiz,recorrido);
          
        }
        public void recorrer_interno(NodoLista<T> inicio, RecorridoDlg<T> recorrido)
        {
            if (inicio != null)
            {
                for (int j = 0; j <= inicio.valores.Length - 1; j++)
                {
                    NodoIndividual<T> aux = inicio.valores[j];
                    if (aux!=null)
                    {
                        recorrido(aux);
                        recorrer_Hijo(aux, recorrido);
                    }                    
                }
                               
            }
        }

        public void recorrer_Hijo(NodoIndividual<T> valor, RecorridoDlg<T> recorrido)
        {
            if (valor.derecho != null)
            {
                for (int j = 0; j <= valor.derecho.valores.Length - 1; j++)
                {
                    NodoIndividual<T> aux = valor.derecho.valores[j];
                    if (aux != null)
                    {
                        recorrido(aux);
                        recorrer_Hijo(aux, recorrido);
                    }
                }
            }

            if (valor.izquierdo != null)
            {
                for (int j = 0; j <= valor.izquierdo.valores.Length - 1; j++)
                {
                    NodoIndividual<T> aux = valor.izquierdo.valores[j];
                    if (aux != null)
                    {
                        recorrido(aux);
                        recorrer_Hijo(aux, recorrido);
                    }
                }
            }
        }
    }
}
