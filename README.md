# Proyecto Estructura de Datos I - 1er. Ciclo 2018
El proyecto del curso de Estructura de Datos I consistió en una aplicación que funcionara como
un servicio de transmisión de contenido (documentales, series, peliculas) vía Internet. Por lo que se desarrolló
una aplicación haciendo uso de C# con un estilo de Modelo Vista Controlador (MVC) que almacenara los datos de cada
contenido digital en una estructura de datos no lineal específica: arbol B, por ser un arbol balanceado de búsqueda
lo cual permitiera el fácil acceso a un dato específico.

-----------------------------------------------------
## Arbol B
La única parte que necesita detallarse en este documento es el uso de los nodos respectivos del proyecto, dado que cada
método dentro de la clase ArbolB está bien explicado en su respectiva documentación interna.
Para hacer uso del arbol se utilizaron dos tipos de nodos: NodoIndividual y NodoLista.

### Nodos

* NodoIndividual es un nodo que contiene el valor de un tipo de genérico y dos apuntadores hacia sus hijos, derecho e izquierdo,
esto porque cada valor específico de la lista de valores que posee un nodo en un árbol B puede tener sus propios hijo derecho e
izquierdo.

        public class NodoIndividual<T>: Nodo<T>
          {
             public T valor;
             public NodoLista<T> derecho, izquierdo;
          }
          
        
* NodoLista no es más que el nodo que utilizará la implementación del árbol, y solo contiene un arreglo de nodos que varía en
tamaño dependiendo del grado que se desee utilizar y de un apuntador hacia un nodo padre, el nodo padre se implementa en este
tipo de nodo porque todos los valores que se encuentren acá provienen de un nodo padre igual.

        public class NodoLista<T> : Nodo<T>
          {
             public NodoIndividual<T>[] valores;
             public NodoLista<T> Padre;
          }



-----------------------------------------------------
## Aplicación

