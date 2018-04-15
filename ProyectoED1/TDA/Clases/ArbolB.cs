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
        public NodoLista<T> raiz;
        CompararNodoDlg<K> _fnCompararLave;
        ObtenerKey<T, K> _fnObtenerLlave;
        CompararPrincipalNodoDlg<J> _fnCompararLLavePrincipal;
        ObtenerKeyPrincipal<T, J> _fnObtenerLLavePrincipal;
        private int grado;
        private bool yaInsertado = false;
        private bool encontrado = false;
        private NodoIndividual<T> nuevito;
        public List<T> Insertados = new List<T>();

        public ArbolB(int grado)
        {
            this.grado = grado;
            raiz = null;
            raiz = new NodoLista<T>(grado);
            nuevito = null;
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
            nuevito = new NodoIndividual<T>();
            nuevito.valor = dato;
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (dato == null)
                throw new ArgumentNullException("El dato ingresado está vacio");

            if (!validarHijos(raiz))
            {

                if (validarEspacio(raiz))
                {

                    insertarEnArreglo(raiz, dato);

                    int ind = buscarEnLista(raiz, dato);
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(raiz.valores[ind].valor), FuncionObtenerLlavePrincipal(dato)) == 0
                        && FuncionCompararLlave(FuncionObtenerLlave(raiz.valores[ind].valor), FuncionObtenerLlave(dato)) == 0)
                    {
                        yaInsertado = true;
                    }
                }
                else //Si ya esta lleno procede a hacer el primer split de todos en la raiz
                {
                    separarNodos(raiz, nuevito);
                }
            }
            else
            {
                insertarInterno(raiz, nuevito);
            }


            if (yaInsertado)
            {
                //insertar a la lisa gg
              //  Insertados.Add(dato);
                asignarPadres3(raiz);
                yaInsertado = false;
                nuevito = null;
            }
        }

        private void asignarPadres3(NodoLista<T> aux)
        {
            if (aux != null)
            {
                NodoLista<T> guardado = aux;
                for (int i = 0; i < aux.valores.Length; i++)
                {
                    if (aux.valores[i] != null)
                    {
                        if (aux.valores[i].izquierdo != null)
                        {
                            asignarPadres3(aux.valores[i].izquierdo);
                            aux.valores[i].izquierdo.Padre = aux;
                            aux.valores[i].derecho.Padre = aux;
                        }

                        if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null) && aux.valores[i].derecho != null)
                        {
                            asignarPadres3(aux.valores[i].derecho);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// Metodo de insercion recursivo para cuando algun valor del nodo lista
        /// ya tengo hijos
        /// </summary>
        /// <param name="aux">Nodo lista a evaluar o insertar</param>
        /// <param name="dato">Valor a insertar</param>
        private void insertarInterno(NodoLista<T> aux, NodoIndividual<T> dato)
        {
            K llaveInsertar = FuncionObtenerLlave(dato.valor);
            J llavePrincipalInsertar = FuncionObtenerLlavePrincipal(dato.valor);

            for (int i = 0; i < aux.valores.Length; i++)
            {

                if (aux.valores[i] != null)
                {
                    //si la funcion principal es igual, pasa al segundo criterio
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) == 0)
                    {
                        if (i == 0 && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[0].valor), llaveInsertar) > 0)
                        {
                            if (aux.valores[i].izquierdo != null)
                            {
                                if (!validarHijos(aux.valores[i].izquierdo))
                                {
                                    if (validarEspacio(aux.valores[i].izquierdo))
                                    {
                                        insertarEnArreglo(aux.valores[i].izquierdo, dato.valor);

                                        int ind = buscarEnLista(aux.valores[i].izquierdo, dato.valor);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].izquierdo.valores[ind].valor), llaveInsertar) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].izquierdo, dato);
                                        if (yaInsertado)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].izquierdo, dato);
                                }
                            }
                        }//Caso sea mayor a la ultima posicion
                        else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                            && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveInsertar) < 0)
                        {
                            if (aux.valores[i].derecho != null)
                            {
                                if (!validarHijos(aux.valores[i].derecho))
                                {
                                    if (validarEspacio(aux.valores[i].derecho))
                                    {
                                        insertarEnArreglo(aux.valores[i].derecho, dato.valor);

                                        int ind = buscarEnLista(aux.valores[i].derecho, dato.valor);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), llaveInsertar) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].derecho, dato);
                                        if (yaInsertado)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].derecho, dato);
                                }
                            }
                        }//Caso este contenido entre dos valores
                        else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                            (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveInsertar) < 0
                            && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i + 1].valor), llaveInsertar) > 0))
                        {
                            if (aux.valores[i].derecho != null)
                            {
                                if (!validarHijos(aux.valores[i].derecho))
                                {
                                    if (validarEspacio(aux.valores[i].derecho))
                                    {
                                        insertarEnArreglo(aux.valores[i].derecho, dato.valor);

                                        int ind = buscarEnLista(aux.valores[i].derecho, dato.valor);
                                        if (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), llaveInsertar) == 0)
                                        {
                                            yaInsertado = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        separarNodos(aux.valores[i].derecho, dato);
                                        if (yaInsertado)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    insertarInterno(aux.valores[i].derecho, dato);
                                }
                            }
                        }
                    }//Caso sea menor a la primera posicion con llave principal
                    else if (i == 0 && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[0].valor), llavePrincipalInsertar) > 0)
                    {
                        if (aux.valores[i].izquierdo != null)
                        {
                            if (!validarHijos(aux.valores[i].izquierdo))
                            {
                                if (validarEspacio(aux.valores[i].izquierdo))
                                {
                                    insertarEnArreglo(aux.valores[i].izquierdo, dato.valor);

                                    int ind = buscarEnLista(aux.valores[i].izquierdo, dato.valor);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].izquierdo.valores[ind].valor), llavePrincipalInsertar) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].izquierdo.valores[ind].valor), llaveInsertar) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].izquierdo, dato);
                                    if (yaInsertado)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                insertarInterno(aux.valores[i].izquierdo, dato);
                            }
                        }
                    }//Caso sea mayor a la ultima posicion llena con llave principal
                    else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                        && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) < 0)
                    {
                        if (aux.valores[i].derecho != null)
                        {
                            if (!validarHijos(aux.valores[i].derecho))
                            {
                                if (validarEspacio(aux.valores[i].derecho))
                                {
                                    insertarEnArreglo(aux.valores[i].derecho, dato.valor);

                                    int ind = buscarEnLista(aux.valores[i].derecho, dato.valor);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].derecho.valores[ind].valor), llavePrincipalInsertar) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), llaveInsertar) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].derecho, dato);
                                    if (yaInsertado)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                insertarInterno(aux.valores[i].derecho, dato);
                            }
                        }
                    }//Caso este contenido entre dos valores con llave principal
                    else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                        (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llavePrincipalInsertar) < 0
                        && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i + 1].valor), llavePrincipalInsertar) > 0))
                    {
                        if (aux.valores[i].derecho != null)
                        {
                            if (!validarHijos(aux.valores[i].derecho))
                            {
                                if (validarEspacio(aux.valores[i].derecho))
                                {
                                    insertarEnArreglo(aux.valores[i].derecho, dato.valor);

                                    int ind = buscarEnLista(aux.valores[i].derecho, dato.valor);
                                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].derecho.valores[ind].valor), llavePrincipalInsertar) == 0
                                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].derecho.valores[ind].valor), llaveInsertar) == 0)
                                    {
                                        yaInsertado = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    separarNodos(aux.valores[i].derecho, dato);
                                    if (yaInsertado)
                                    {
                                        break;
                                    }
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
        private void insertarEnArreglo(NodoLista<T> aux, T dato)
        {
            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] == null)
                {
                    NodoIndividual<T> nuevo = new NodoIndividual<T>();
                    nuevo.valor = dato;
                    //nuevo.padre = aux.Padre;
                    aux.valores[i] = nuevo;
                    ordenarNodo(aux.valores);
                    break;
                }
            }
        }

        /// <summary>
        /// Inserta un nodo que ya existe en un nodo lista distinto
        /// </summary>
        /// <param name="aux">Nodo lista en el que inserta</param>
        /// <param name="nodo">Nodo a insertar</param>
        private void insertarExistenteEnArreglo(NodoLista<T> aux, NodoIndividual<T> nodo)
        {
            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] == null)
                {
                    aux.valores[i] = nodo;
                    ordenarNodo(aux.valores);
                    break;
                }
            }
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

        private void asignarPadres(NodoLista<T> aux)
        {
            if (aux != null)
            {
                NodoLista<T> guardado = aux;
                for (int i = 0; i < aux.valores.Length; i++)
                {
                    if (aux.valores[i] != null)
                    {
                        if (i == 0 && aux.valores[i].izquierdo != null)
                        {
                            asignarPadres(aux.valores[i].izquierdo);
                        }
                        else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null) && aux.valores[i].derecho != null)
                        {
                            asignarPadres(aux.valores[i].derecho);
                        }
                        else if (aux.valores[i].izquierdo != null && aux.valores[i].derecho != null)
                        {
                            aux.valores[i].izquierdo.Padre = aux;
                            aux.valores[i].derecho.Padre = aux;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

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

                            if (i != 0 && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux[i].valor), FuncionObtenerLlavePrincipal(aux[i - 1].valor)) == 0
                                && this.FuncionCompararLlave(FuncionObtenerLlave(aux[i].valor), FuncionObtenerLlave(aux[i - 1].valor)) < 0)
                            {
                                ordenarNodo(aux);
                            }
                        }
                        else if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux[i].valor), FuncionObtenerLlavePrincipal(aux[i + 1].valor)) > 0)
                        {
                            NodoIndividual<T> tempo = aux[i];
                            aux[i] = aux[i + 1];
                            aux[i + 1] = tempo;

                            if (i != 0 && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux[i].valor), FuncionObtenerLlavePrincipal(aux[i - 1].valor)) < 0)
                            {
                                ordenarNodo(aux);
                            }
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

        private void asignarHijos(NodoIndividual<T>[] aux, int ind)
        {
            if (ind == 0 && aux[ind + 1] != null)
            {
                aux[ind + 1].izquierdo = aux[ind].derecho;
            }
            else if (ind == aux.Length - 1 || (ind < aux.Length - 1 && aux[ind + 1] == null))
            {
                aux[ind - 1].derecho = aux[ind].izquierdo;
            }
            else if (aux[ind - 1] != null && aux[ind + 1] != null)
            {
                aux[ind + 1].izquierdo = aux[ind].derecho;
                aux[ind - 1].derecho = aux[ind].izquierdo;
            }
        }

        public void buscarEnArbol(NodoLista<T> aux, NodoIndividual<T> dato)
        {
            K llaveBuscar = FuncionObtenerLlave(dato.valor);
            J llaveBuscarPrincipal = FuncionObtenerLlavePrincipal(dato.valor);

            for (int i = 0; i < aux.valores.Length; i++)
            {
                if (aux.valores[i] != null)
                {
                    if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llaveBuscarPrincipal) == 0
                        && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveBuscar) == 0)
                    {
                        int ind = buscarEnLista(aux, dato.valor);
                        if (ind == 0)
                        {
                            if (aux.valores[0] == dato)
                            {
                                asignarHijos(aux.valores, ind);
                            }
                        }
                        else
                        {
                            asignarHijos(aux.valores, ind);
                        }
                        break;
                    }
                }
            }

            if (!encontrado)
            {
                for (int i = 0; i < aux.valores.Length; i++)
                {

                    if (aux.valores[i] != null)
                    {
                        //si la funcion principal es igual, pasa al segundo criterio
                        if (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llaveBuscarPrincipal) == 0)
                        {
                            //Caso sea menor a la primera posicion
                            if (i == 0 && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[0].valor), llaveBuscar) > 0)
                            {
                                buscarEnArbol(aux.valores[i].izquierdo, dato);
                                break;
                            }
                            //Caso sea mayor a la ultima posicion
                            else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                                && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveBuscar) < 0)
                            {
                                buscarEnArbol(aux.valores[i].derecho, dato);
                                break;
                            }
                            //Caso este contenido entre dos valores
                            else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                                (FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i].valor), llaveBuscar) < 0
                                && FuncionCompararLlave(FuncionObtenerLlave(aux.valores[i + 1].valor), llaveBuscar) > 0))
                            {
                                buscarEnArbol(aux.valores[i].derecho, dato);
                                break;
                            }
                        }//Caso sea menor a la primera posicion con llave principal
                        else if (i == 0 && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[0].valor), llaveBuscarPrincipal) > 0)
                        {
                            buscarEnArbol(aux.valores[i].izquierdo, dato);
                            break;

                        }//Caso sea mayor a la ultima posicion llena con llave principal
                        else if ((i == aux.valores.Length - 1 || aux.valores[i + 1] == null)
                            && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llaveBuscarPrincipal) < 0)
                        {
                            buscarEnArbol(aux.valores[i].derecho, dato);
                            break;
                        }//Caso este contenido entre dos valores con llave principal
                        else if (i < aux.valores.Length - 1 && aux.valores[i + 1] != null &&
                            (FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i].valor), llaveBuscarPrincipal) < 0
                            && FuncionCompararLlavePrincipal(FuncionObtenerLlavePrincipal(aux.valores[i + 1].valor), llaveBuscarPrincipal) > 0))
                        {
                            buscarEnArbol(aux.valores[i].derecho, dato);
                            break;
                        }
                    }
                }
            }

            
        }

        /// <summary>
        /// Cuando el nodo lista se encuentra lleno se crea un nuevo nodo temporal 
        /// con un espacio extra para ordenarse y observar cual es el valor que debe
        /// "subir" al nodo lista Padre hasta que este insertado en una posicion
        /// </summary>
        /// <param name="aux">Arreglo o lista</param>
        /// <param name="valor">Valor a ingresar</param>
        public void separarNodos(NodoLista<T> aux, NodoIndividual<T> valor)
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
            nodoAux = valor;
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
            //Todos los hermanos que tiene a la izquiera seran parte de su hijo izquierdo
            //Y todos los hermanos a la derecha seran parte de su hijo derecho
            for (int i = 0; i < temp.valores.Length; i++)
            {
                if (i < indice)
                {
                    insertarExistenteEnArreglo(hijoIzq, temp.valores[i]);
                }
                else if (i > indice)
                {
                    insertarExistenteEnArreglo(hijoDer, temp.valores[i]);
                }
            }

            subir.izquierdo = hijoIzq;
            subir.derecho = hijoDer;



            if (aux.Padre != null)
            {
                if (validarEspacio(aux.Padre as NodoLista<T>))
                {
                    //Falta asignar los nuevos hijos de los hermanos
                    //if (aux.Padre.valores[0].padre == null)
                    //{
                    //    subir.padre = null;
                    //}
                    //else
                    //{
                    //    subir.padre = aux.Padre.valores[0].padre;
                    //}
                    insertarExistenteEnArreglo(aux.Padre, subir);
                    hijoDer.Padre = aux.Padre;
                    hijoIzq.Padre = aux.Padre;
                    int ind = buscarEnLista(aux.Padre, subir.valor);
                    if (ind == 0)
                    {
                        if (aux.Padre.valores[0] == subir)
                        {
                            asignarHijos(aux.Padre.valores, ind);
                        }
                    }
                    else
                    {
                        asignarHijos(aux.Padre.valores, ind);
                    }
                    yaInsertado = true;
                }
                else
                {
                    separarNodos((aux.Padre as NodoLista<T>), subir);
                    buscarEnArbol(raiz, subir);
                    encontrado = false;
                }
            }
            else
            {
                NodoLista<T> raizAux = new NodoLista<T>(grado);
                //subir.padre = null;
                insertarExistenteEnArreglo(raizAux, subir);
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
                yaInsertado = true;
            }
        }


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
