using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Dollarcity
{
    public partial class FrmProducto : Form
    {
        Producto productoEditar = null;
        string nombreImagen = "";

        public FrmProducto()
        {
            InitializeComponent();
        }

        public FrmProducto(Producto producto)
        {
            InitializeComponent();

            productoEditar = producto;

            cmbCategoria.Items.Add("Jardineria");
            cmbCategoria.Items.Add("Cocina");
            cmbCategoria.Items.Add("Hogar");
            cmbCategoria.Items.Add("Cuidado Personal");
            cmbCategoria.Items.Add("Manualidades");
            cmbCategoria.Items.Add("Juguetes");

            txtNombre.Text = producto.Nombre;

            txtPrecio.Text = producto.Precio.ToString();

            txtDescripcion.Text = producto.Descripcion;

            cmbCategoria.Text = producto.Categoria;

            chkStock.Checked = producto.Stock;

            nombreImagen = producto.Imagen;

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
                    picProducto.Image =
                        Image.FromStream(fs);
                }
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();

            abrir.Filter = "Imagenes|*.jpg;*.png";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                picProducto.Image =
                    Image.FromFile(abrir.FileName);

                nombreImagen =
                    Path.GetFileName(abrir.FileName);

                string destino = Path.Combine(
                    Application.StartupPath,
                    "ImagenesProductos",
                    nombreImagen
                );

                File.Copy(
                    abrir.FileName,
                    destino,
                true
                );
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDACIONES
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(cmbCategoria.Text))
                {
                    MessageBox.Show(
                        "Complete todos los campos",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // VALIDAR PRECIO
                decimal precio;

                if (!decimal.TryParse(txtPrecio.Text, out precio))
                {
                    MessageBox.Show(
                        "Ingrese un precio válido"
                    );

                    return;
                }

                // VALIDAR IMAGEN
                if (string.IsNullOrEmpty(nombreImagen))
                {
                    MessageBox.Show(
                        "Seleccione una imagen"
                    );

                    return;
                }

                // AGREGAR
                if (productoEditar == null)
                {
                    Producto nuevo = new Producto();

                    nuevo.Id = Datos.ListaProductos.Count + 1;

                    nuevo.Nombre = txtNombre.Text;

                    nuevo.Precio = precio;

                    nuevo.Descripcion = txtDescripcion.Text;

                    nuevo.Categoria = cmbCategoria.Text;

                    nuevo.Stock = chkStock.Checked;

                    nuevo.Imagen = nombreImagen;

                    Datos.ListaProductos.Add(nuevo);
                }
                else
                {
                    // EDITAR
                    productoEditar.Nombre = txtNombre.Text;

                    productoEditar.Precio = precio;

                    productoEditar.Descripcion = txtDescripcion.Text;

                    productoEditar.Categoria = cmbCategoria.Text;

                    productoEditar.Stock = chkStock.Checked;

                    productoEditar.Imagen = nombreImagen;
                }

                MessageBox.Show(
                    "Producto guardado correctamente"
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question );

            if ( resultado == DialogResult.Yes )
            this.Close();
        }
    }
}
