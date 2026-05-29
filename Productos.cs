using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sistema_Dollarcity
{
    public partial class Productos : Form
    {

        string categoriaActual;
        string rolUsuario;

        public Productos(string categoria, string rol)
        {
            InitializeComponent();

            categoriaActual = categoria;
            rolUsuario = rol;
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            lblCategoria.Text = categoriaActual;

            MostrarProductos();

            if (rolUsuario != "ADMIN")
            {
                btnAgregarP.Visible = false;
            }
        }

        private void MostrarProductos()
        {
            flowLayoutPanel1.Controls.Clear();

            var productosFiltrados = Datos.ListaProductos
                .Where(p => p.Categoria == categoriaActual)
                .ToList();

            foreach (Producto producto in productosFiltrados)
            {
                Panel tarjeta = CrearTarjeta(producto);

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }

        private Panel CrearTarjeta(Producto producto)
        {
            Panel panel = new Panel();

            panel.Width = 220;
            panel.Height = 380;

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.White;

            PictureBox pic = new PictureBox();

            pic.Width = 180;
            pic.Height = 120;

            pic.Top = 10;
            pic.Left = 20;

            pic.SizeMode = PictureBoxSizeMode.Zoom;

            string ruta = Path.Combine(
                Application.StartupPath,
                "ImagenesProductos",
                producto.Imagen
            );

            if (File.Exists(ruta))
            {
                using (FileStream fs = new FileStream(
                    ruta,
                    FileMode.Open,
                    FileAccess.Read))
                {
                    pic.Image = Image.FromStream(fs);
                }
            }
            else
            {
                MessageBox.Show(ruta);
            }

                Label lblNombre = new Label();

            lblNombre.Text = producto.Nombre;

            lblNombre.Top = 140;
            lblNombre.Left = 10;

            lblNombre.Width = 180;

            Label lblPrecio = new Label();

            lblPrecio.Text = "$" + producto.Precio;

            lblPrecio.Top = 170;
            lblPrecio.Left = 10;

            Label lblDescripcion = new Label();

            lblDescripcion.Text = producto.Descripcion;

            lblDescripcion.Top = 200;
            lblDescripcion.Left = 10;

            lblDescripcion.Width = 180;

            lblDescripcion.Height = 40;

            Label lblStock = new Label();

            lblStock.Text = producto.Stock ?
                "En Stock" :
                "Agotado";

            lblStock.Top = 250;
            lblStock.Left = 10;

            Button btnCarrito = new Button();

            btnCarrito.Text = "🛒 Agregar";
            btnCarrito.Top = 290;
            btnCarrito.Left = 10;
            btnCarrito.BackColor = Color.LightGreen;
            btnCarrito.Font = new Font("Arial", 9, FontStyle.Bold);
            btnCarrito.Click += (s, e) =>
            {
                Carrito.AgregarProducto(producto, 1);
                MessageBox.Show(
                    $"✓ {producto.Nombre} agregado al carrito",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            panel.Controls.Add(pic);
            panel.Controls.Add(lblNombre);
            panel.Controls.Add(lblPrecio);
            panel.Controls.Add(lblDescripcion);
            panel.Controls.Add(lblStock);
            panel.Controls.Add(btnCarrito);

            // ADMIN
            if (rolUsuario == "ADMIN")
            {
                Button btnEditar = new Button();

                btnEditar.Text = "Editar";

                btnEditar.Top = 290;
                btnEditar.Left = 100;

                btnEditar.Click += (s, e) =>
                {
                    FrmProducto frm =
                        new FrmProducto(producto);

                    frm.ShowDialog();

                    MostrarProductos();
                };

                Button btnEliminar = new Button();

                btnEliminar.Text = "Eliminar";

                btnEliminar.Top = 330;
                btnEliminar.Left = 10;

                btnEliminar.Click += (s, e) =>
                {
                    Datos.ListaProductos.Remove(producto);

                    MostrarProductos();
                };

                panel.Controls.Add(btnEditar);
                panel.Controls.Add(btnEliminar);
            }

            return panel;
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            FrmProducto frm = new FrmProducto();

            frm.ShowDialog();

            MostrarProductos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inicio frm = new Inicio(rolUsuario);

            frm.Show();

            this.Close();
        }
    }
}
