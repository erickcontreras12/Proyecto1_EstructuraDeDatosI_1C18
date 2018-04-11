using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;

namespace TDA.Clases
{
    public class ArbolB<T, J, K>
    {
        public NodoLista<T> raiz;
        CompararNodoDlg<K> _fnCompararLave;
        ObtenerKey<T, K> _fnObtenerLlave;
        CompararPrincipalNodoDlg<J> _fnCompararLLavePrincipal;
        ObtenerKeyPrincipal<T, J> _fnObtenerLLavePrincipal;
        private int grado;
        private bool yaInsertado = false;

        public ArbolB(int grado)
        {
            this.grado = grado;
            raiz = null;
            raiz = new NodoLista<T>(grado);
        }

        /// <summary>
        /// Funcion para obtener y devolver el valor de criterio
        /// principal para proceder a ordenar el arbol
        /// </summary>
        public CompararPrincipalNodoDlg<J> FuncionCompararLlavePrincipal
        {
            get
            {
                return _fnCompararLLavePrincipal;
            }
            set
            {
                _fnCompararLLavePrincipal = value;
            }
        }

        /// <summary>
        /// Funcion para obtener y devolver el atributo de criterio
        /// principal por el que se ordena el arbol
        /// </summary>
        public ObtenerKeyPrincipal<T, J> FuncionObtenerLlavePrincipal
        {
            get
            {
                return _fnObtenerLLavePrincipal;
            }
            set
            {
                _fnObtenerLLavePrincipal = value;
            }
        }

        /// <summary>
        /// Funcion para obtener y devolver el valor de criterio
        /// secundario para proceder a ordenar el arbol
        /// </summary>
        public CompararNodoDlg<K> FuncionCompararLlave
        {
            get
            {
                return _fnCompararLave;
            }
            set
            {
                _fnCompararLave = value;
            }
        }

        /// <summary>
        /// Funcion para obtener y devolver el atributo de criterio 
        /// secundario por el que se ordena el arbol
        /// </summary>
        public ObtenerKey<T, K> FuncionObtenerLlave
        {
            get
            {
                return _fnObtenerLlave;
            }
            set
            {
                _fnObtenerLlave = value;
            }
        }

        /// <summary>
        /// Insertar general, inserta en la raiz de ser posible sino 
        /// busca insertar en el nodo hoja que corresponda
        /// </summary>
        /// <param name="dato">Valor que estara almacenado dentro del nodo</param>
        public void Insertar(T dato)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (dato == null)
                throw new ArgumentNullException("El dato ingresado está vacio");

            if (!validarHijos(raiz))
            {

                if (validarEspacio(raiz))
                {
                    insertarEnArreglo(raiz.valores, dato);

                    int ind = buscarEnLista(raiz, dato);
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(raiz.valores[ind].valor), FuncionObtenerLlavePrincipal(dato)) == 0
                        && FuncionCompararLlave(FuncionObtenerLlave(raiz.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                    {
                        yaInsertado = true;
                    }
                }
                else //Si ya esta lleno procede a hacer el primer split de todos en la raiz
                {
                    separarNodos(raiz, dato);
                }
            }
            else
            {
                insertarInterno(raiz, dato);
            }


            if (yaInsertado)
            {
                //insertar a la lisa gg
                yaInsertado = false;
            }
        }


        private void insertarInterno(NodoLista<T> aux, T dato)
        {
            K llaveInsertar = FuncionObtenerLlave(dato);
            J llavePrincipalInsertar = FuncionObtenerLlavePrincipal(dato);

            for (int i = 0; i < aux.valores.Length; i++)
            {

                if (aux.valores[i] != null)
                {
                    //si la funcion principal es igual, pasa al segundo criterio
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) == 0)
                    {
                        if (i == 0 && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[0].valor), llaveInsertar) < 0)
                        {
                            if (aux.valores[i].izquierdo != null)
                            {
                                if (!validarHijos(aux.valores[i].izquierdo))
                                {
                                    if (validarEspacio(aux.valores[i].izquierdo))
                                    {
                                        insertarEnArreglo(aux.valores[i].izquierdo.valores, dato);

                                        int ind = buscarEnLista(aux.valores[i].izquierdo, dato);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].izquierdo.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].izquierdo, dato);
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].izquierdo, dato);
                                }
                            }
                        }//Caso sea mayor a la ultima posicion
                        else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                            && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveInsertar) > 0)
                        {
                            if (aux.valores[i].derecho != null)
                            {
                                if (!validarHijos(aux.valores[i].derecho))
                                {
                                    if (validarEspacio(aux.valores[i].derecho))
                                    {
                                        insertarEnArreglo(aux.valores[i].derecho.valores, dato);

                                        int ind = buscarEnLista(aux.valores[i].derecho, dato);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].derecho, dato);
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].derecho, dato);
                                }
                            }
                        }//Caso este contenido entre dos valores
                        else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                            (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveInsertar) > 0
                            && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i + 1].valor), llaveInsertar) < 0))
                        {
                            if (aux.valores[i].derecho != null)
                            {
                                if (!validarHijos(aux.valores[i].derecho))
                                {
                                    if (validarEspacio(aux.valores[i].derecho))
                                    {
                                        insertarEnArreglo(aux.valores[i].derecho.valores, dato);

                                        int ind = buscarEnLista(aux.valores[i].derecho, dato);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].derecho, dato);
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].derecho, dato);
                                }
                            }
                        }
                    }//Caso sea menor a la primera posicion con llave principal
                    else if (i == 0 && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[0].valor), llavePrincipalInsertar) < 0)
                    {
                        if (aux.valores[i].izquierdo != null)
                        {
                            if (!validarHijos(aux.valores[i].izquierdo))
                            {
                                if (validarEspacio(aux.valores[i].izquierdo))
                                {
                                    insertarEnArreglo(aux.valores[i].izquierdo.valores, dato);

                                    int ind = buscarEnLista(aux.valores[i].izquierdo, dato);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].izquierdo.valores[ind].valor), FuncionObtenerLlavePrincipal(dato)) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].izquierdo.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].izquierdo, dato);
                                }
                            }
                            else
                            {
                                insertarInterno(aux.valores[i].izquierdo, dato);
                            }
                        }
                    }//Caso sea mayor a la ultima posicion con llave principal
                    else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                        && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) > 0)
                    {
                        if (aux.valores[i].derecho != null)
                        {
                            if (!validarHijos(aux.valores[i].derecho))
                            {
                                if (validarEspacio(aux.valores[i].derecho))
                                {
                                    insertarEnArreglo(aux.valores[i].derecho.valores, dato);

                                    int ind = buscarEnLista(aux.valores[i].derecho, dato);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].izquierdo.valores[ind].valor), FuncionObtenerLlavePrincipal(dato)) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].derecho, dato);
                                }
                            }
                            else
                            {
                                insertarInterno(aux.valores[i].derecho, dato);
                            }
                        }
                    }//Caso este contenido entre dos valores con llave principal
                    else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                        (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) > 0
                        && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i + 1].valor), llavePrincipalInsertar) < 0))
                    {
                        if (aux.valores[i].derecho != null)
                        {
                            if (!validarHijos(aux.valores[i].derecho))
                            {
                                if (validarEspacio(aux.valores[i].derecho))
                                {
                                    insertarEnArreglo(aux.valores[i].derecho.valores, dato);

                                    int ind = buscarEnLista(aux.valores[i].derecho, dato);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].izquierdo.valores[ind].valor), FuncionObtenerLlavePrincipal(dato)) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].derecho, dato);
                                }
                            }
                            else
                            {
                                insertarInterno(aux.valores[i].derecho, dato);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ingresa un valor en una lista o arreglo y lo ordena
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        /// <param name="dato">Valor</param>
        private void insertarEnArreglo(NodoIndividual<T>[] aux, T dato)
        {
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i] == null)
                {
                    NodoIndividual<T> nuevo = new NodoIndividual<T>();
                    nuevo.valor = dato;
                    aux[i] = nuevo;
                    ordenarNodo(aux);
                    break;
                }
            }
        }

        private void asignarHijos(Nodo<T>[] aux)
        {

        }


        /// <summary>
        /// Validar si el nodo lista tiene hijos en alguna de sus posiciones
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        /// <returns>True si tiene hijos</returns>
        private bool validarHijos(NodoLista<T> aux)
        {
            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] != null)
                {
                    if (aux.valores[i].izquierdo != null || aux.valores[i].derecho != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Validar si hay espacio en el nodo lista
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        /// <returns>True si hay espacio</returns>
        private bool validarEspacio(NodoLista<T> aux)
        {
            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] == null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ordena los valores del nodo lista en forma ascendente 
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        public void ordenarNodo(NodoIndividual<T>[] aux)
        {
            for (int i = 0; i < aux.Length; i++)
            {
                if (i != aux.Length - 1)
                {
                    if (aux[i + 1] != null)
                    {
                        if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux[i].valor), FuncionObtenerLlavePrincipal(aux[i + 1].valor)) == 0)
                        {
                            if (this.FuncionCompararLlave(FuncionObtenerLlave(aux[i].valor), FuncionObtenerLlave(aux[i + 1].valor)) > 0)
                            {
                                NodoIndividual<T> tempo = aux[i];
                                aux[i] = aux[i + 1];
                                aux[i + 1] = tempo;
                            }
                        }
                        else if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux[i].valor), FuncionObtenerLlavePrincipal(aux[i + 1].valor)) > 0)
                        {
                            NodoIndividual<T> tempo = aux[i];
                            aux[i] = aux[i + 1];
                            aux[i + 1] = tempo;
                        }
                    }
                }
            }
        }


        private int buscarEnLista(NodoLista<T> aux, T dato)
        {
            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] != null)
                {
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(dato), FuncionObtenerLlavePrincipal(aux.valores[i].valor)) == 0
                        && FuncionCompararLlave(FuncionObtenerLlave(dato), FuncionObtenerLlave(aux.valores[i].valor)) == 0)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }


        /// <summary>
        /// Cuando el nodo lista se encuentra lleno se crea un nuevo nodo temporal 
        /// con un espacio extra para ordenarse y observar cual es el valor que debe
        /// "subir" al nodo lista Padre hasta que este insertado en una posicion
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        /// <param name="valor">Valor a ingresar</param>
        public void separarNodos(NodoLista<T> aux, T valor)
        {
            //Crea la lista temporal y ingresa los datos del nodo lista actual
            NodoLista<T> temp = new NodoLista<T>(grado + 1);
            for (int i = 0; i < temp.valores.Length; i++)
            {
                if (i <= aux.valores.Length - 1)
                {
                    temp.valores[i] = aux.valores[i] as NodoIndividual<T>;
                }
            }

            /*Ya que la lista temporal tiene un espacio adicional al del nodo lista actual
             * (el ultimo espacio) ahi se ingresa el valor que viene y se ordena para
             * escoger que valor "sube" para ser insertado en un espacio real de 
             * algun nodo del arbol
             */
            NodoIndividual<T> nodoAux = new NodoIndividual<T>();
            nodoAux.valor = valor;
            temp.valores[temp.valores.Length - 1] = nodoAux;
            ordenarNodo(temp.valores);

            NodoIndividual<T> subir;
            NodoLista<T> hijoIzq = new NodoLista<T>(grado);
            NodoLista<T> hijoDer = new NodoLista<T>(grado);
            double indice = (grado / 2) - 1;
            if (grado % 2 != 0)
            {
                indice = indice + 0.5;
            }
            subir = temp.valores[Convert.ToInt32(indice)];
            //Todos los hermanos que tiene a la izquiera
            for (int i = 0; i < temp.valores.Length; i++)
            {
                if (i < indice)
                {
                    insertarEnArreglo(hijoIzq.valores, temp.valores[i].valor);
                }
                else if (i > indice)
                {
                    insertarEnArreglo(hijoDer.valores, temp.valores[i].valor);
                }
            }

            subir.izquierdo = hijoIzq;
            subir.derecho = hijoDer;



            if (aux.Padre != null)
            {
                if (validarEspacio(aux.Padre as NodoLista<T>))
                {
                    //Falta asignar los nuevos hijos de los hermanos
                    NodoLista<T> padreAux = aux.Padre as NodoLista<T>;
                    insertarEnArreglo(padreAux.valores, subir.valor);
                    int ind = buscarEnLista(padreAux, subir.valor);
                    if (ind == 0)
                    {
                        if (padreAux.valores[0] == subir)
                        {
                            padreAux.valores[0] = subir;
                        }
                    }
                    else
                    {
                        padreAux.valores[ind] = subir;
                    }
                    aux.Padre = padreAux;
                }
                else
                {
                    separarNodos((aux.Padre as NodoLista<T>), subir.valor);
                }
            }
            else
            {
                NodoLista<T> raizAux = new NodoLista<T>(grado);
                insertarEnArreglo(raizAux.valores, subir.valor);
                int ind = buscarEnLista(raizAux, subir.valor);
                if (ind == 0)
                {
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(raizAux.valores[0].valor), FuncionObtenerLlavePrincipal(subir.valor)) == 0
                        && FuncionCompararLlave(FuncionObtenerLlave(raizAux.valores[0].valor), FuncionObtenerLlave(subir.valor)) == 0)
                    {
                        raizAux.valores[ind] = subir;
                    }
                }
                raiz = raizAux;
                raiz.Padre = null;
                hijoDer.Padre = raiz;
                hijoIzq.Padre = raiz;


            }
        }
    }
}
