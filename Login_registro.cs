using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Dollarcity
{
    public partial class Login_registro : Form
    {
        public Login_registro()
        {
            InitializeComponent();
        }

        private void btn_registrarse2_Click(object sender, EventArgs e)
        {
            string conexion = "Server=.;Database=BD_Dollarcity;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                try
                {
                    conn.Open();

                    // 1. VALIDAR CAMPOS VACÍOS
                    if (string.IsNullOrEmpty(txtNombre.Text) ||
                        string.IsNullOrEmpty(txtApellido.Text) ||
                        string.IsNullOrEmpty(txtCorreo.Text) ||
                        string.IsNullOrEmpty(txtContraseña.Text))
                    {
                        MessageBox.Show("Complete todos los campos");
                        return;
                    }

                    // 2. VERIFICAR SI EL CORREO YA EXISTE
                    string verificar = "SELECT COUNT(*) FROM Usuarios WHERE Correo=@correo";

                    SqlCommand cmdVerificar = new SqlCommand(verificar, conn);
                    cmdVerificar.Parameters.AddWithValue("@correo", txtCorreo.Text);

                    int existe = (int)cmdVerificar.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Este correo ya está registrado");
                        return;
                    }

                    // 3. INSERTAR USUARIO
                    string query = @"INSERT INTO Usuarios 
                            (Nombre, Apellido, Correo, Contrasena, Rol) 
                            VALUES 
                            (@nombre, @apellido, @correo, @contrasena, @rol)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@contrasena", txtContraseña.Text);
                    cmd.Parameters.AddWithValue("@rol", "Cliente");

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario registrado correctamente");

                    // 🟢 4. IR AL LOGIN
                    Login login = new Login();
                    login.Show();

                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Login frmMenu = new Login();
            frmMenu.Show();
            this.Close();
        }
    }
}
