using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Dollarcity
{
    public class ItemCarrito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal 
        { 
            get { return Precio * Cantidad; } 
        }
        public string Imagen { get; set; }
    }

    // Clase estática para manejar el carrito global
    public static class Carrito
    {
        public static List<ItemCarrito> Items = new List<ItemCarrito>();

        // Método para agregar productos al carrito
        public static void AgregarProducto(Producto producto, int cantidad = 1)
        {
            var item = Items.FirstOrDefault(i => i.Id == producto.Id);

            if (item != null)
            {
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
        }

        // Método para eliminar productos del carrito
        public static void EliminarProducto(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        // Calcular total
        public static decimal ObtenerTotal()
        {
            return Items.Sum(i => i.Subtotal);
        }

        // Limpiar carrito
        public static void LimpiarCarrito()
        {
            Items.Clear();
        }
    }
}
