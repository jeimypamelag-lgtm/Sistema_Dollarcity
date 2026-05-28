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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            // 🔴 1. VALIDAR CAMPOS VACÍOS
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }

            string conexion = //"Server=.;Database=BD_Dollarcity;Trusted_Connection=True;"; //"Server=PAMELA_PORTILLO;Database=BD_Dollarcity;Integrated Security=true";
                @"Server=DESKTOP-CC30LNJ\SQLEXPRESS;Database=BD_Dollarcity;Trusted_Connection=True;"; // - dary (a mi con esta cadena me agarra la base xd)

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                try
                {
                    conn.Open();

                    // 2. VERIFICAR SI EL USUARIO EXISTE
                    string queryUsuario = "SELECT COUNT(*) FROM Usuarios WHERE Correo=@correo";
                    SqlCommand cmdUsuario = new SqlCommand(queryUsuario, conn);
                    cmdUsuario.Parameters.AddWithValue("@correo", correo);

                    int existe = (int)cmdUsuario.ExecuteScalar();

                    if (existe == 0)
                    {
                        MessageBox.Show("Este usuario no está registrado");
                        return;
                    }

                    // 3. VERIFICAR CONTRASEÑA
                    string queryLogin = "SELECT Nombre, Rol FROM Usuarios WHERE Correo=@correo AND Contrasena=@contrasena";
                    SqlCommand cmdLogin = new SqlCommand(queryLogin, conn);
                    cmdLogin.Parameters.AddWithValue("@correo", correo);
                    cmdLogin.Parameters.AddWithValue("@contrasena", contrasena);

                    SqlDataReader reader = cmdLogin.ExecuteReader();

                    if (reader.Read())
                    {
                        string nombre = reader["Nombre"].ToString();
                        string rol = reader["Rol"].ToString();

                        MessageBox.Show("Bienvenido " + nombre);

                        Inicio principal = new Inicio(rol);
                        principal.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnRegistrar1_Click(object sender, EventArgs e)
        {
            Login_registro ventana = new Login_registro();

            // 2. Lo mostramos
            ventana.Show();
            this.Hide();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
