using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Dollarcity
{
    public partial class FrmCarrito : Form
    {
        public FrmCarrito()
        {
            InitializeComponent();
            this.Text = "Carrito de Compras";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(700, 600);
            this.BackColor = Color.WhiteSmoke;
        }

        private void FrmCarrito_Load(object sender, EventArgs e)
        {
            ConfigurarControles();
            MostrarCarrito();
        }

        private void ConfigurarControles()
        {
            // Panel principal
            Panel pnlMain = new Panel();
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.BackColor = Color.WhiteSmoke;
            this.Controls.Add(pnlMain);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "🛒 CARRITO DE COMPRAS";
            lblTitulo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.DarkBlue;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 15);
            pnlMain.Controls.Add(lblTitulo);

            // FlowLayoutPanel para items
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(20, 60);
            flowLayoutPanel1.Size = new Size(660, 350);
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(flowLayoutPanel1);

            // Label Total
            Label lblTotal = new Label();
            lblTotal.Name = "lblTotal";
            lblTotal.Text = "Total: $0.00";
            lblTotal.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTotal.ForeColor = Color.Green;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(20, 420);
            pnlMain.Controls.Add(lblTotal);

            // Botón Comprar
            Button btnComprar = new Button();
            btnComprar.Name = "btnComprar";
            btnComprar.Text = "✓ COMPRAR";
            btnComprar.Font = new Font("Arial", 11, FontStyle.Bold);
            btnComprar.BackColor = Color.Green;
            btnComprar.ForeColor = Color.White;
            btnComprar.Size = new Size(120, 40);
            btnComprar.Location = new Point(400, 470);
            btnComprar.Click += btnComprar_Click;
            pnlMain.Controls.Add(btnComprar);

            // Botón Limpiar
            Button btnLimpiar = new Button();
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Text = "🗑 LIMPIAR";
            btnLimpiar.Font = new Font("Arial", 11, FontStyle.Bold);
            btnLimpiar.BackColor = Color.Red;
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Size = new Size(120, 40);
            btnLimpiar.Location = new Point(540, 470);
            btnLimpiar.Click += btnLimpiarCarrito_Click;
            pnlMain.Controls.Add(btnLimpiar);

            // Botón Cerrar
            Button btnCerrar = new Button();
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Text = "← VOLVER";
            btnCerrar.Font = new Font("Arial", 11, FontStyle.Bold);
            btnCerrar.BackColor = Color.Gray;
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Size = new Size(120, 40);
            btnCerrar.Location = new Point(20, 470);
            btnCerrar.Click += (s, e) => this.Close();
            pnlMain.Controls.Add(btnCerrar);
        }

        private void MostrarCarrito()
        {
            FlowLayoutPanel flowLayoutPanel1 = (FlowLayoutPanel)this.Controls[0].Controls["flowLayoutPanel1"];
            flowLayoutPanel1.Controls.Clear();

            if (Carrito.Items.Count == 0)
            {
                Label lblVacio = new Label();
                lblVacio.Text = "El carrito está vacío 😢";
                lblVacio.AutoSize = true;
                lblVacio.Font = new Font("Arial", 14, FontStyle.Bold);
                lblVacio.ForeColor = Color.Gray;
                flowLayoutPanel1.Controls.Add(lblVacio);
                return;
            }

            // Mostrar cada item del carrito
            foreach (var item in Carrito.Items)
            {
                Panel panelItem = new Panel();
                panelItem.Width = 620;
                panelItem.Height = 90;
                panelItem.BorderStyle = BorderStyle.FixedSingle;
                panelItem.BackColor = Color.LightGray;
                panelItem.Margin = new Padding(5);

                // Nombre
                Label lblNombre = new Label();
                lblNombre.Text = item.Nombre;
                lblNombre.Top = 10;
                lblNombre.Left = 10;
                lblNombre.Width = 200;
                lblNombre.Font = new Font("Arial", 11, FontStyle.Bold);

                // Precio unitario
                Label lblPrecio = new Label();
                lblPrecio.Text = $"${item.Precio:F2}";
                lblPrecio.Top = 10;
                lblPrecio.Left = 220;
                lblPrecio.Width = 100;

                // Cantidad
                Label lblCantidad = new Label();
                lblCantidad.Text = $"Cantidad: {item.Cantidad}";
                lblCantidad.Top = 35;
                lblCantidad.Left = 10;
                lblCantidad.Width = 200;

                // Subtotal
                Label lblSubtotal = new Label();
                lblSubtotal.Text = $"Subtotal: ${item.Subtotal:F2}";
                lblSubtotal.Top = 35;
                lblSubtotal.Left = 220;
                lblSubtotal.Width = 150;
                lblSubtotal.Font = new Font("Arial", 11, FontStyle.Bold);
                lblSubtotal.ForeColor = Color.DarkGreen;

                // Botón Eliminar
                Button btnEliminar = new Button();
                btnEliminar.Text = "❌ Eliminar";
                btnEliminar.Width = 100;
                btnEliminar.Top = 35;
                btnEliminar.Left = 500;
                btnEliminar.BackColor = Color.IndianRed;
                btnEliminar.ForeColor = Color.White;
                btnEliminar.Font = new Font("Arial", 9, FontStyle.Bold);
                btnEliminar.Click += (s, e) =>
                {
                    Carrito.EliminarProducto(item.Id);
                    MostrarCarrito();
                    ActualizarTotal();
                };

                panelItem.Controls.Add(lblNombre);
                panelItem.Controls.Add(lblPrecio);
                panelItem.Controls.Add(lblCantidad);
                panelItem.Controls.Add(lblSubtotal);
                panelItem.Controls.Add(btnEliminar);

                flowLayoutPanel1.Controls.Add(panelItem);
            }

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total = Carrito.ObtenerTotal();
            Label lblTotal = (Label)this.Controls[0].Controls["lblTotal"];
            lblTotal.Text = $"Total: ${total:F2}";
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (Carrito.Items.Count == 0)
            {
                MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = Carrito.ObtenerTotal();
            DialogResult resultado = MessageBox.Show(
                $"¿Deseas confirmar tu compra por ${total:F2}?",
                "Confirmar Compra",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show(
                    $"✓ ¡Compra realizada exitosamente por ${total:F2}!",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Carrito.LimpiarCarrito();
                MostrarCarrito();
            }
        }

        private void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            if (Carrito.Items.Count == 0)
            {
                MessageBox.Show("El carrito ya está vacío", "Información");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Deseas vaciar el carrito?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                Carrito.LimpiarCarrito();
                MostrarCarrito();
            }
        }
    }
}
