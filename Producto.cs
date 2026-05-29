using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Dollarcity
{
    /// <summary>
    /// Clase que representa un producto en el sistema Dollarcity.
    /// Incluye propiedades para gestión de stock.
    /// </summary>
    public class Producto
    {
        /// <summary>Identificador único del producto</summary>
        public int Id { get; set; }

        /// <summary>Nombre del producto</summary>
        public string Nombre { get; set; }

        /// <summary>Precio unitario del producto</summary>
        public decimal Precio { get; set; }

        /// <summary>Descripción detallada del producto</summary>
        public string Descripcion { get; set; }

        /// <summary>Indica si el producto está disponible en stock</summary>
        public bool Stock { get; set; }

        /// <summary>Categoría a la que pertenece el producto</summary>
        public string Categoria { get; set; }

        /// <summary>Nombre del archivo de imagen del producto</summary>
        public string Imagen { get; set; }

        /// <summary>Cantidad disponible en stock (NUEVA PROPIEDAD)</summary>
        public int CantidadStock { get; set; } = 0;

        /// <summary>
        /// Obtiene el estado del stock del producto.
        /// </summary>
        /// <returns>Estado actual del stock ("Disponible", "Por Agotarse" o "Agotado")</returns>
        public string ObtenerEstadoStock()
        {
            if (CantidadStock <= 0)
                return "Agotado";
            else if (CantidadStock == 1)
                return "Por Agotarse";
            else
                return "Disponible";
        }

        /// <summary>
        /// Verifica si el producto puede ser agregado al carrito.
        /// </summary>
        /// <returns>True si hay stock disponible, False en caso contrario</returns>
        public bool PuedeAgregarAlCarrito()
        {
            return CantidadStock > 0;
        }

        /// <summary>
        /// Descuenta la cantidad especificada del stock disponible.
        /// </summary>
        /// <param name="cantidad">Cantidad a descontar del stock</param>
        /// <returns>True si la operación fue exitosa, False si no hay suficiente stock</returns>
        public bool DescontarStock(int cantidad)
        {
            if (cantidad > CantidadStock)
                return false;

            CantidadStock -= cantidad;
            return true;
        }
    }

    /// <summary>
    /// Clase estática para gestionar la lista global de productos.
    /// Actúa como almacenamiento centralizado de datos.
    /// </summary>
    public static class Datos
    {
        /// <summary>Lista global de productos del sistema</summary>
        public static List<Producto> ListaProductos =
            new List<Producto>();

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a buscar</param>
        /// <returns>El producto encontrado o null si no existe</returns>
        public static Producto ObtenerProductoPorId(int id)
        {
            return ListaProductos.FirstOrDefault(p => p.Id == id);
        }
    }
}
