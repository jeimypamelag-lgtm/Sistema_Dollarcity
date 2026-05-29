using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Dollarcity
{
    /// <summary>
    /// Clase que representa un item en el carrito de compras.
    /// Contiene información del producto y cantidad seleccionada.
    /// </summary>
    public class ItemCarrito
    {
        /// <summary>ID del producto en el carrito</summary>
        public int Id { get; set; }

        /// <summary>Nombre del producto</summary>
        public string Nombre { get; set; }

        /// <summary>Precio unitario del producto</summary>
        public decimal Precio { get; set; }

        /// <summary>Cantidad del producto en el carrito</summary>
        public int Cantidad { get; set; }

        /// <summary>Calcula el subtotal del item (Precio × Cantidad)</summary>
        public decimal Subtotal
        {
            get { return Precio * Cantidad; }
        }

        /// <summary>Nombre del archivo de imagen del producto</summary>
        public string Imagen { get; set; }
    }

    /// <summary>
    /// Clase estática para gestionar el carrito global.
    /// Maneja operaciones de agregar, eliminar y validar productos.
    /// </summary>
    public static class Carrito
    {
        /// <summary>Lista global de items en el carrito</summary>
        public static List<ItemCarrito> Items = new List<ItemCarrito>();

        /// <summary>
        /// Agrega un producto al carrito con validación de stock.
        /// Si el producto ya existe, incrementa la cantidad.
        /// </summary>
        /// <param name="producto">Producto a agregar</param>
        /// <param name="cantidad">Cantidad a agregar (por defecto 1)</param>
        /// <returns>True si se agregó exitosamente, False si no hay stock</returns>
        public static bool AgregarProducto(Producto producto, int cantidad = 1)
        {
            // VALIDACIÓN: Verificar que haya stock disponible
            if (!producto.PuedeAgregarAlCarrito())
            {
                return false; // No hay stock disponible
            }

            // VALIDACIÓN: Verificar que la cantidad solicitada no exceda el stock
            if (cantidad > producto.CantidadStock)
            {
                return false; // Cantidad solicitada supera el stock disponible
            }

            var item = Items.FirstOrDefault(i => i.Id == producto.Id);

            if (item != null)
            {
                // Si el producto ya existe, validar que la nueva cantidad no supere el stock
                if ((item.Cantidad + cantidad) > producto.CantidadStock)
                {
                    return false; // Cantidad total supera el stock
                }
                // Si el producto ya existe, aumentar cantidad
                item.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, agregarlo nuevo
                Items.Add(new ItemCarrito
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = cantidad,
                    Imagen = producto.Imagen
                });
            }

            return true; // Producto agregado exitosamente
        }

        /// <summary>
        /// Elimina un producto del carrito por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar</param>
        public static void EliminarProducto(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        /// <summary>
        /// Calcula el total del carrito.
        /// </summary>
        /// <returns>Total acumulado de todos los items</returns>
        public static decimal ObtenerTotal()
        {
            return Items.Sum(i => i.Subtotal);
        }

        /// <summary>
        /// Limpia todos los items del carrito.
        /// </summary>
        public static void LimpiarCarrito()
        {
            Items.Clear();
        }

        /// <summary>
        /// Procesa la compra: descuenta el stock de los productos vendidos.
        /// </summary>
        /// <returns>True si la compra se procesó exitosamente, False si hay error de stock</returns>
        public static bool ProcesarCompra()
        {
            // Primero validar que hay stock para todos los items
            foreach (var item in Items)
            {
                var producto = Datos.ObtenerProductoPorId(item.Id);
                if (producto == null || !producto.DescontarStock(item.Cantidad))
                {
                    return false; // No hay suficiente stock
                }
            }

            // Si todo es válido, limpiar el carrito
            LimpiarCarrito();
            return true;
        }
    }
}
