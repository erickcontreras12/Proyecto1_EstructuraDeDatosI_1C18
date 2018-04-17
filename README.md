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

### Login
El login tiene un diseño amigable para que sea vistoso para todo tipo de usuarios, esta es la pestaña principal de la app por lo que
es necesario loggearse para tener acceso al resto de funciones de la misma. Si algún usuario de la app intentará cambiar directamente
a alguna de las otras pestañas haciendo uso de la URL el programa cuenta con las validaciones necesarias para que este no pueda
cambiar sin antes ingresar su usuario y contraseña. Esto a excepción del catálogo de contenido para el internauta pueda
vizualizar el contenido antes de crear una cuenta en el sitio.

![](https://image.ibb.co/gdy9u7/Login.jpg)

### Registro
Para registrarse se le piden al usuario ciertos datos personales para la configuración de su cuenta. Siendo los más importantes
y obligatorios el usuario y contraseña ya que estos son los que el usuario utilizará para ingresar a su perfil.

![](https://image.ibb.co/m4MUu7/Registro.jpg)

Al estar registrando una cuenta nueva, la app es capaz de detectar dos tipos de excepciones y en ambos casos retorna la alerta correspondiente al usuario:
1. El nombre de usuario ya existe.
2. Las contraseñas no coinciden.
![](https://image.ibb.co/hd001n/Alerta_Coincidencia.jpg)

### Catálogo
Como ya se había mencionado el catálogo es la única vista global, es decir que puede ser accedida por el administrador, un usuario 
corriente o bien sin necesidad de estar logeado. Para cada una de estas tres formas en que el catálogo puede ser
visualizado cambia en lo siguiente:
* Cuando lo ve el administrador: puede ver el contenido disponible y eliminar cualquiera de los datos que desee, además si el admin elimina algo que este contenido en la watch list de cualquier usuario este se elimina de forma inmediata para evitar incovenientes con los usuarios ya que al eliminar el contenido deja de estar disponible.
* Cuando lo ve un usuario que inicio sesión: puede ver el contenido disponible y agregar el contenido que desee a su watch list personal.
* Cuando lo ve un usuario sin iniciar sesión: solo puede observar el contenido disponible.

![](https://image.ibb.co/jkOW7S/Catalogo.jpg)

### Administrador
Al iniciar sesión el administrador este tienen acceso al mayor número de funciones de la aplicación ya que es quien puede agregar y 
eliminar el contenido que se encuentra almacenado en los arboles utilizados para contener la información. El administrador también
es capaz de ver la lista de usuarios registrados y generar un archivo Json para mantenerlos en su PC y que no se pierdan los 
registros.

Así como con la lista de usuarios el administrador es capaz de generar archivos Json de todos los árboles que implementa el programa 
para guardar la información de cada dato ingresado.

![](https://image.ibb.co/dxTW7S/Principal_Admin.jpg)

### Carga de datos por archivo
Cuando el administrador sube nuevo contenido a partir de un archivo Json se ingresan todos los datos a un árbol general de contenido
y para que este tenga más control sobre cada uno de los tipos de contenido puede generar árboles por cada tipo de contenido digital:
documentales, películas y series. Además cuando crea los árboles de manera individual por cada tipo, se crean 3 tipos distintos de 
árboles con distintos criterios de ordenamiento: año, género y nombre. En el caso de año y género dado que pueden repetirse las llaves
de cada objeto acude a un segundo criterio (nombre), solo si las llaves son iguales. 

![](https://image.ibb.co/enTW7S/Carga_Json.jpg)

### Creación de contenido manual
La carga de archivo es utilizada cuando el administrador desea subir contenido de manera másiva, así que también se incluye una pestaña 
para que puedan ingresarse datos de manera manual, es decir uno por uno.

![](https://image.ibb.co/j5SDMn/Crear_Pelicula.jpg)

### Lista para "ver más tarde"
Para un usuario normal que inicia sesión y en el catálogo encuentra contenido que desea ver pero en el momento no tiene el tiempo 
suficiente así que lo agrega su watch list para no olvidar el nombre de la producción audiovisual. Cada usuario tiene acceso a una 
pestaña que le muestra el contenido de su lista.

![](https://image.ibb.co/hEetMn/Watch_List.jpg)






Autores:
Fabian Alvarez & Erick Contreras, 2018.











