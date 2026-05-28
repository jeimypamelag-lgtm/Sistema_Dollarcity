using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Dollarcity
{
    public class Producto //clase producto 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public bool Stock { get; set; }

        public string Categoria { get; set; }

        public string Imagen { get; set; }
    }

    //clase estatica para guardar productos
    public static class Datos
    {
        public static List<Producto> ListaProductos =
            new List<Producto>();
    }
}
