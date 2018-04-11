using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.Interfaces
{
    public delegate int CompararNodoDlg<T>(T actual, T nuevo);
    public delegate int CompararPrincipalNodoDlg<T>(T actual, T nuevo);
    public delegate void RecorridoDlg<T>(Nodo<T> actual);
    public delegate K ObtenerKey<T, K>(T dato);
    public delegate J ObtenerKeyPrincipal<T, J>(T dato);
}
